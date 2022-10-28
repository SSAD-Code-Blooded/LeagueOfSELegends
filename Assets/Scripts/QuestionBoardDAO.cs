using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;

public class QuestionBoardDAO : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] QuestionBoardContainerPrefab;
    public TMP_Dropdown worldDD;
    public TMP_Dropdown sectionDD;
    public TMP_Dropdown levelDD;
    public static string questionWorld;
    public static string questionSection;
    public static string questionLevel;
    private int height_offset = -275;
    private int margin = 555;
    private int limit = 10;
    private bool done = true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (done)
        {
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            var colRef = db.Collection("Users");
            Query query = colRef.OrderByDescending("ChallengeModeWins").Limit(limit);
            
            // questionWorld = worldDD.options[worldDD.value].text;
            // questionSection = sectionDD.options[sectionDD.value].text;
            // questionLevel = levelDD.options[levelDD.value].text;
            // UnityEngine.Debug.Log(System.String.Format("World:{0} | Section:{1} | Level:{2}", questionWorld, questionSection, questionLevel));

            QuestionBoardContainerPrefab = new GameObject[10];
            query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            QuerySnapshot query = task.Result;
            int i=0;
            foreach (DocumentSnapshot documentSnapshot in query.Documents) {
                QuestionBoardContainerPrefab[i] = Instantiate(Resources.Load<GameObject>("QuestionBoardBox"));
                TextMeshProUGUI textMesh = QuestionBoardContainerPrefab[i].GetComponentInChildren<TextMeshProUGUI>();
                // textMesh.text="(RANK " + (i+1).ToString() + ") ";
                
                // if(i<=2){
                //     textMesh.color = new Color(0.9686275f,0.7960785f,0.09803922f,1);
                // }
    
                Dictionary<string, object> student = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in student) {   
                    textMesh.text="(ID) Question:  " + "\n\n\n\n"; // \n to give more space for question
                    textMesh.text=textMesh.text+ "Option A:  " + "             ";
                    textMesh.text=textMesh.text+ "Option B:  " + "\n";
                    textMesh.text=textMesh.text+ "Option C:  " + "             ";
                    textMesh.text=textMesh.text+ "Option D:  " + "\n";
                    textMesh.text=textMesh.text+ "Correct Answer: ___ " +"\n";
                
                UnityEngine.Debug.Log(System.String.Format("{0}: {1}", pair.Key, pair.Value));
                }
                
                i=i+1;    
                };
                // height_offset = -200;
                // margin = 340;
                for(int x = 0; x < QuestionBoardContainerPrefab.Length; x++)
                {
                    var newAssignmentContainer = Instantiate(QuestionBoardContainerPrefab[x], new Vector3(0, height_offset, 0), Quaternion.identity);
                    newAssignmentContainer.transform.SetParent(gameObject.transform.parent, false);
                    height_offset = height_offset - margin;
                    Debug.Log("height_offset: " + height_offset);
                }
            });
            
        }
        done = false;
    }

}
