using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class AddQuestionManager : MonoBehaviour
{
    public TMP_InputField question,ansA,ansB,ansC,ansD;
    public TMP_Dropdown worldDD,sectionDD,levelDD,correctAns;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public QuestionModel createQuestionData(){
        string [] ansList = new string[]{ansA.text,ansB.text,ansC.text,ansD.text};
        var ansMap = new Dictionary<string,int>(){
            {"A",1},
            {"B",2},
            {"C",3},
            {"D",4}
        };
        
        var questionData = new QuestionModel{
            Question = question.text,
            Answers = ansList,
            CorrectAnswer = ansMap[correctAns.options[correctAns.value].text]
        };
        return questionData;
    }
    public void clickAddQuestionButton(){
        bool writeData= true;
        QuestionModel questionData = createQuestionData();
        string worldSelection= worldDD.options[worldDD.value].text;
        string sectionSelection= sectionDD.options[sectionDD.value].text;
        string levelSelection= levelDD.options[levelDD.value].text;
        //int counterValue=QuestionDAO.retrieveCounter(worldSelection, sectionSelection,levelSelection);
        //UnityEngine.Debug.Log(String.Format("Retrieved: ",counterValue));

        if (worldSelection=="SELECT WORLD"){
            writeData=false;
        }
        if (sectionSelection=="SELECT SECTION"){
            writeData=false;
        }
        if (levelSelection=="SELECT LEVEL"){
            writeData=false;
        }
        if (writeData){
            QuestionDAO.setAnswers(questionData, worldSelection, sectionSelection,levelSelection);
            UnityEngine.Debug.Log("Question set succesfully!");
            SceneManager.LoadScene("Teacher Menu");
        }
        else{
            UnityEngine.Debug.Log("Question not set!");
        }
    }
}
