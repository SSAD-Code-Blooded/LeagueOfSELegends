using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using System;



[FirestoreData]

public struct QuestionModel
{
    [FirestoreProperty]
    public string Question {get; set;}

    [FirestoreProperty]
    public string[] Answers {get; set;}

    [FirestoreProperty]
    public int CorrectAnswer{get;set;}
}

public class QuestionDAO : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        Dictionary<string,string> questionDict;
        // String question1=getQuestion("Planning","Requirement Analysis","Easy","Q1");
        // UnityEngine.Debug.Log(question1);
        // UnityEngine.Debug.Log(question1.CorrectAnswer);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public String getQuestion(String world, String section, String difficulty,String question){
    //     var firestore = FirebaseFirestore.DefaultInstance;
    //     var _listener=firestore.Document("QnA/"+world+"/Sections/"+section+"/Difficulty/"+difficulty+"/Questions/"+question).Listen(snapshot=>{
    //         // Assert.IsNull(task.Exception);
    //     if (snapshot.exist){
    //         var qnData = snapshot.ConvertTo<QuestionModel>();
    //     UnityEngine.Debug.Log(qnData.Question);
    //     var question = new Dictionary(){
    //         {"Question",qnData.Question},
    //         {"Answers",qnData.Answers},
    //         {"CorrectAnswer",qnData.CorrectAnswer},
    //     };
    //     return qnData.Question.ToString();
    //     }
    //     else{
    //         return " ";
    //     }
        

    // });
    // }
    
}
