using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using TMPro;
using UnityEngine.SceneManagement;

public static class QuestionChoice{
    static string difficulty = " ";
    
    public static void setDifficulty(string setDifficulty){
        difficulty = setDifficulty;
    }
    public static string getDifficulty(){
       return difficulty;
    }
}

public class QuestionBoardDAO : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] QuestionBoardContainerPrefab;
    public TMP_Dropdown worldDD;
    public TMP_Dropdown sectionDD;
    public TMP_Dropdown levelDD;
    public GameObject errorUI;
    public TMP_Text errorMessageToShow;
    public string errorMessage;

    private int height_offset = -275;
    private int margin = 555;
    private int limit = 20;
    public bool done = false;
    private string questionLevel="empty";

    
    
    void Start()
    {
        questionLevel = QuestionChoice.getDifficulty();
        
    }

    // Update is called once per frame
    void Update()
    {   
        var ansMap = new Dictionary<int,string>(){
            {1,"A"},
            {2,"B"},
            {3,"C"},
            {4,"D"}
        };
        
        if (done==true)
        {   
            Debug.Log("level: " + questionLevel);
            QuestionBoardContainerPrefab = new GameObject[limit];
            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            var colRef = db.Collection("/QnA/Testing/Sections/Functional Testing/difficulty/"+questionLevel+"/Questions");
            Query query = colRef.Limit(limit);
            
            
            
            // UnityEngine.Debug.Log(System.String.Format("World:{0} | Section:{1} | Level:{2}", questionWorld, questionSection, questionLevel));

            QuestionBoardContainerPrefab = new GameObject[limit];
            query.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            QuerySnapshot query = task.Result;
            int i=0;
            foreach (DocumentSnapshot documentSnapshot in query.Documents) {
                QuestionBoardContainerPrefab[i] = Instantiate(Resources.Load<GameObject>("QuestionBoardBox"));
                // GameObject newButton = Instantiate(Resources.Load<GameObject>("DeleteButton")) as GameObject;
                // newButton.transform.SetParent(QuestionBoardContainerPrefab[i].transform, false);
                // QuestionBoardContainerPrefab[i].FindGameObjectWithTag("DeleteButton").GetComponent<Button>().onClick.AddListener(delegate {testButton(i.ToString()); });

                // GameObject newButton = Instantiate(button) as GameObject;
                // newButton.transform.SetParent(QuestionBoardContainerPrefab[i].transform, false);
                TextMeshProUGUI textMesh = QuestionBoardContainerPrefab[i].GetComponentInChildren<TextMeshProUGUI>();
            
    
                QuestionModel questionData = documentSnapshot.ConvertTo<QuestionModel>();
                textMesh.text="Question ID:  " + questionData.questionUID+"\n"; 
                textMesh.text=textMesh.text+ "Question:  " + questionData.Question+"\n"; // \n to give more space for question
                textMesh.text=textMesh.text+ "Option A:  " + questionData.Answers[0]+"\n";
                textMesh.text=textMesh.text+ "Option B:  " + questionData.Answers[1]+"\n";
                textMesh.text=textMesh.text+ "Option C:  " + questionData.Answers[2]+"\n";
                textMesh.text=textMesh.text+ "Option D:  " + questionData.Answers[3]+"\n";
                textMesh.text=textMesh.text+ "Correct Answer: "+ansMap[questionData.CorrectAnswer] +"\n";
                
                i=i+1;    
                };
                // height_offset = -200;
                // margin = 340;
                for(int x = 0; x < QuestionBoardContainerPrefab.Length; x++)
                {
                    var newAssignmentContainer = Instantiate(QuestionBoardContainerPrefab[x], new Vector3(0, height_offset, 0), Quaternion.identity);
                    // buttons[x].GetComponent<Button>().onClick.AddListener(delegate {testButton(x.ToString()); });
                    newAssignmentContainer.transform.SetParent(gameObject.transform.parent, false);
                    height_offset = height_offset - margin;
                    Debug.Log("height_offset: " + height_offset);
                }
            });
            done = false;
        }
        
    }

    public void fetchQuestions()
    {
        string worldSelection= worldDD.options[worldDD.value].text;
        string sectionSelection= sectionDD.options[sectionDD.value].text;
        string levelSelection= levelDD.options[levelDD.value].text;
        bool fetchData= true;
        if (worldSelection=="SELECT WORLD")
        {
            fetchData=false;
        }
        if (sectionSelection=="SELECT SECTION")
        {
            fetchData=false;
        }
        if (levelSelection=="SELECT LEVEL")
        {
            fetchData=false;
        }
        if (fetchData)
        {
            SceneManager.LoadScene("View Questions");
            QuestionChoice.setDifficulty(levelDD.options[levelDD.value].text);
            Debug.Log("level: " + questionLevel);
        }
        else
        {
            errorMessage = "Error! Make sure to fill in all the necessary information. Please try again.";
            errorUI.SetActive(true);
            errorMessageToShow.text = errorMessage; 
            Debug.Log(errorMessage);
        }
    }

    public void setDone(){
        done=true;
    }

    public void testButton(string idx){
        Debug.Log("Delete Button" + idx);

    }

}
