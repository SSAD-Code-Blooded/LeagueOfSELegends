using System.Diagnostics.Tracing;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
// using System.Reflection.PortableExecutable;
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

public class QuestionDAO
{
    public int counterValue;
    // Start is called before the first frame update
    // void Start()
    // {   
    //     Dictionary<string,string> questionDict;
    //     // String question1=getQuestion("Planning","Requirement Analysis","Easy","Q1");
    //     // UnityEngine.Debug.Log(question1);
    //     // UnityEngine.Debug.Log(question1.CorrectAnswer);
        

    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public int retrieveCounter(String world, String section, String difficulty){
        UnityEngine.Debug.Log("Retrieving Counter");
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("QnA/"+world+"/Sections/"+section+"/difficulty").Document(difficulty);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
        DocumentSnapshot snapshot = task.Result;
        if (snapshot.Exists) {
        //counter=snapshot.GetValue();

        } else {
            UnityEngine.Debug.Log(String.Format("Retrieving Counter Failed"));
        }
        });

    return counterValue;


    }
    public static void setAnswers(QuestionModel questionData, String world, String section, String difficulty){
        string question = questionData.Question;
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document("QnA/"+world+"/Sections/"+section+"/difficulty/"+difficulty+"/Questions/"+question).SetAsync(questionData);
        UnityEngine.Debug.Log("Question write succesfully!");
    }

    public static void deleteQuestion(String questionid, String world, String section, String difficulty){
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document("QnA/"+world+"/Sections/"+section+"/difficulty/"+difficulty+"/Questions/"+questionid).DeleteAsync();
        UnityEngine.Debug.Log("Question deleted succesfully!");

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
