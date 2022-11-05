using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Extensions;

///
///Allows Teacher to delete question from Firebase by specifiying - World, Section, Difficulty and Question ID 
///
public class deleteQuestionManager : MonoBehaviour
{   public TMP_Dropdown worldDD,sectionDD,levelDD; /**< User Interface Drop Down for World, Section and Level*/
    public TMP_InputField questionid; /**< User Interface input field for Question ID*/
    public  TMP_Text displayQuestion;   /**< User Interface Text field for displaying of Retrieved Question for Verification before Deleting*/

    public GameObject errorUI;  /**< User Interface of Error Message */
    public TMP_Text errorMessageToShow;/**< User Interface Text field for displaying of Error Message */
    public string errorMessage;/**< String value of Error Message */

    ///
    ///Takes in Section, World, Level and Question ID and calls the QuestionDAO to delete question
    ///
    public void clickDeleteQuestionButton(){
        UnityEngine.Debug.Log("FN CALLED");
        bool delData= true; 
        string worldSelection= worldDD.options[worldDD.value].text; 
        string sectionSelection= sectionDD.options[sectionDD.value].text; 
        string levelSelection= levelDD.options[levelDD.value].text; 
        string questionidSelection=questionid.text; 

        if (worldSelection=="SELECT WORLD"){
            delData=false; 
        }
        if (sectionSelection=="SELECT SECTION"){
            delData=false;
        }
        if (levelSelection=="SELECT LEVEL"){
            delData=false;
        }
        if (questionidSelection==""){
            delData=false;
        }
        if (delData)
        {            
            QuestionDAO.deleteQuestion(questionidSelection, worldSelection, sectionSelection,levelSelection);
            UnityEngine.Debug.Log("Question Deleted succesfully!");
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

    ///
    /// Retrieves and displays question that the Teacher intends to delete from the database for confirmation
    ///
    public void retrieveQuestion(){
        string worldSelection= worldDD.options[worldDD.value].text; 
        string sectionSelection= sectionDD.options[sectionDD.value].text; 
        string levelSelection= levelDD.options[levelDD.value].text;
        string questionidSelection=questionid.text;
        bool fetchData = true;

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
        if (questionidSelection == "")
        {
            fetchData = false;
        }
        
        if (fetchData)
        {
            var ansMap = new Dictionary<int,string>(){
                        {1,"A"},
                        {2,"B"},
                        {3,"C"},
                        {4,"D"}
                    };

            var firestore = FirebaseFirestore.DefaultInstance;
            DocumentReference docRef = firestore.Document("QnA/"+worldSelection+"/Sections/"+sectionSelection+"/difficulty/"+levelSelection+"/Questions/"+questionidSelection);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
            DocumentSnapshot snapshot = task.Result;
            QuestionModel questionData = snapshot.ConvertTo<QuestionModel>();
            displayQuestion.text="Question ID:  " + questionData.questionUID+"\n"; 
            displayQuestion.text=displayQuestion.text+"Question:  " + questionData.Question+"\n"; // \n to give more space for question
            displayQuestion.text=displayQuestion.text+ "Option A:  " + questionData.Answers[0]+"\n";
            displayQuestion.text=displayQuestion.text+ "Option B:  " + questionData.Answers[1]+"\n";
            displayQuestion.text=displayQuestion.text+ "Option C:  " + questionData.Answers[2]+"\n";
            displayQuestion.text=displayQuestion.text+ "Option D:  " + questionData.Answers[3]+"\n";
            displayQuestion.text=displayQuestion.text+ "Correct Answer: "+ansMap[questionData.CorrectAnswer] +"\n";
            });
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
