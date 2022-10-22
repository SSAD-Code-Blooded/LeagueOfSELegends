using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;

public class LeaderboardDAO : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] LeaderboardContainerPrefab;
    private int height_offset = -180;
    private int margin = 340;
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
            Query query = colRef.OrderByDescending("ChallengeModeWins").Limit(10);
            
            LeaderboardContainerPrefab = new GameObject[10];
            query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            QuerySnapshot query = task.Result;
            int i=0;
            foreach (DocumentSnapshot documentSnapshot in query.Documents) {
                LeaderboardContainerPrefab[i] = Instantiate(Resources.Load<GameObject>("LeaderboardBox"));
                TextMeshProUGUI textMesh = LeaderboardContainerPrefab[i].GetComponentInChildren<TextMeshProUGUI>();
                // textMesh.text="(RANK " + (i+1).ToString() + ") ";
                
                // if(i<=2){
                //     textMesh.color = new Color(0.9686275f,0.7960785f,0.09803922f,1);
                // }
    
                Dictionary<string, object> student = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in student) {   
                    if (pair.Key.Equals("ChallengeModeWins")){
                        textMesh.text= "Number of Challenge Won:  " + pair.Value.ToString()+ " (RANK #" + (i+1).ToString() + ") \n";
                    }
                    else if (pair.Key.Equals("EmailAddress")){
                        textMesh.text=textMesh.text+ "Email Address:  " + pair.Value.ToString()+"\n";
                    }
                    else if (pair.Key.Equals("MatriculationNo")){
                        textMesh.text=textMesh.text+ "Matriculation No:  " + pair.Value.ToString()+"\n";
                    }
                    else if (pair.Key.Equals("UserName")){
                        textMesh.text=textMesh.text+ "Username:  " + pair.Value.ToString()+"\n";
                    }

                
                UnityEngine.Debug.Log(System.String.Format("{0}: {1}", pair.Key, pair.Value));
                }
                
                i=i+1;    
                };
                // height_offset = -200;
                // margin = 340;
                for(int x = 0; x < LeaderboardContainerPrefab.Length; x++)
                {
                    var newAssignmentContainer = Instantiate(LeaderboardContainerPrefab[x], new Vector3(0, height_offset, 0), Quaternion.identity);
                    newAssignmentContainer.transform.SetParent(gameObject.transform.parent, false);
                    height_offset = height_offset - margin;
                    Debug.Log("height_offset: " + height_offset);
                }
            });
            
        }
        done = false;
    }

}
