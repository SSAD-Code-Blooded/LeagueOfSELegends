using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Extensions;
using System;

[FirestoreData]
public struct CounterModel
{   [FirestoreProperty]
    public Dictionary<string,object> collection{get;set;}

    [FirestoreProperty]
    public int counterValue {get; set;}
}

public class AddQuestionManager : MonoBehaviour
{
    public TMP_InputField question,ansA,ansB,ansC,ansD;
    public TMP_Dropdown worldDD,sectionDD,levelDD,correctAns;
    public string questionUID;
    public int counter;
    public int counterint;
    public string questionIncremented;

    public GameObject errorUI;
    public TMP_Text errorMessageToShow;
    public string errorMessage;

    public QuestionModel createQuestionData()
    {
        string [] ansList = new string[]{ansA.text,ansB.text,ansC.text,ansD.text};
        var ansMap = new Dictionary<string,int>()
        {
            {"A",1},
            {"B",2},
            {"C",3},
            {"D",4}
        };
        
        var questionData = new QuestionModel
        {
            Question = question.text,
            Answers = ansList,
            CorrectAnswer = ansMap[correctAns.options[correctAns.value].text],
            questionUID=""
        };
        return questionData;
    }
    
    public void clickAddQuestionButton(){
        bool writeData= true;
        
        string worldSelection= worldDD.options[worldDD.value].text;
        string sectionSelection= sectionDD.options[sectionDD.value].text;
        string levelSelection= levelDD.options[levelDD.value].text;
        string correctAnswer= correctAns.options[correctAns.value].text;

        if (worldSelection=="SELECT WORLD")
        {
            writeData=false;
        }
        if (sectionSelection=="SELECT SECTION")
        {
            writeData=false;
        }
        if (levelSelection=="SELECT LEVEL")
        {
            writeData=false;
        }
        if (correctAnswer=="SELECT ANSWER")
        {
            writeData=false;
        }
        if (question.text=="")
        {
            writeData=false;
        }
        if (ansA.text=="")
        {
            writeData=false;
        }
        if (ansB.text=="")
        {
            writeData=false;
        }
        if (ansC.text=="")
        {
            writeData=false;
        }
        if (ansD.text=="")
        {
            writeData=false;
        }

        if (writeData)
        {
            QuestionModel questionData = createQuestionData();
            UnityEngine.Debug.Log("Retrieving Counter");
            var firestore = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = firestore.Document("QnA/"+worldSelection+"/Sections/"+sectionSelection+"/difficulty/"+levelSelection);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists) {
            Dictionary<string, object> data = snapshot.ToDictionary();
            var counter = data["counter"];
            string counterstring =counter.ToString();
            counterint=int.Parse(counterstring);
            counterint=counterint+1;
            QuestionDAO.updateCounter(worldSelection, sectionSelection,levelSelection,counterint);
            questionIncremented="Q"+counterint.ToString();
            questionData.questionUID=questionIncremented;
            UnityEngine.Debug.Log(questionIncremented);
            QuestionDAO.setAnswers(questionData, worldSelection, sectionSelection,levelSelection,questionIncremented);
            }
            else 
            {
                errorMessage = "Retrieving Counter Failed";
                errorUI.SetActive(true);
                errorMessageToShow.text = errorMessage; 
                UnityEngine.Debug.Log(String.Format(errorMessage));
            }
            });  
            UnityEngine.Debug.Log("Question set succesfully!");
            SceneManager.LoadScene("Teacher Menu");
        }
        else
        {
            errorMessage = "Error! Make sure to fill in all the necessary information. Please try again.";
            errorUI.SetActive(true);
            errorMessageToShow.text = errorMessage; 
            UnityEngine.Debug.Log(errorMessage);
        }
    }
}


//  public int retrieveCounter(String world, String section, String difficulty){
    //     UnityEngine.Debug.Log("Retrieving Counter");
    //     var firestore = FirebaseFirestore.DefaultInstance;
    //     DocumentReference docRef = firestore.Document("QnA/"+world+"/Sections/"+section+"/difficulty/"+difficulty);
    //     docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
    //     {
    //     DocumentSnapshot snapshot = task.Result;
    //     if (snapshot.Exists) {
    //     Dictionary<string, object> city = snapshot.ToDictionary();
    //     foreach (KeyValuePair<string, object> pair in city) {
    //         var counter = pair.Value;
    //         string counterstring =counter.ToString();
    //         int counterint=int.Parse(counterstring);
    //         counterint=counterint+1;
    //         UnityEngine.Debug.Log(counterint.ToString());}

    //     // CounterModel counterData=snapshot.ConvertTo<CounterModel>();
    //     // int counter = counterData.counterValue;
    //     // counter=counter+1;
    //     // UnityEngine.Debug.Log(counter.ToString());

    //     } else {
    //         UnityEngine.Debug.Log(String.Format("Retrieving Counter Failed"));
    //     }
    //     });

    //     return counter;
    //     }
