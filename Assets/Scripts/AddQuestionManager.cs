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
{   
    [FirestoreProperty]
    public int counterValue {get; set;} /**< Integer Counter Value used to set Question ID*/
} /**< Firestore Data referencing Firebase to refer to Database's counter values*/

///
///Allows Teacher to specifiying - World, Section, Difficulty and Question ID and Add question to Firebase by Calling Question Data Access Object
///
public class AddQuestionManager : MonoBehaviour
{
    public TMP_InputField question,ansA,ansB,ansC,ansD;  /**< User Interface input field for the 4 Options for Question*/
    public TMP_Dropdown worldDD,sectionDD,levelDD,correctAns; /**< User Interface Dropdown for World, Section, Level and Correct Answer*/
    public string questionUID; /**< User Interface input field for Question ID*/
    public int counter; /**< Integer Value for Counter*/
    public int counterint; /**< Final Integer Value for Counter after Processing*/
    public string questionIncremented; /**< String Value representing QuestionID */

    public GameObject errorUI;/**< User Interface of Error Message */
    public TMP_Text errorMessageToShow;/**< User Interface Text field for displaying of Error Message */
    public string errorMessage;/**< String value of Error Message */

    ///
    /// Creating Question Model of struct data type to add Question into Firebase,
    /// with each member of the struct representing a field in the Question Document in Database
    ///
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

    ///
    ///Adding of Question into Database by setting relevant fields and calling Question DAO (Data Access Object)
    ///
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
