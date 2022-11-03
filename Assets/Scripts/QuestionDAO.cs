using System.ComponentModel;
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
using TMPro;



[FirestoreData]

public struct QuestionModel
{
    [FirestoreProperty]
    public string Question {get; set;}

    [FirestoreProperty]
    public string[] Answers {get; set;}

    [FirestoreProperty]
    public int CorrectAnswer{get;set;}

    [FirestoreProperty]
    public string questionUID{get;set;}

}


public class QuestionDAO
{   

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

    // public var retrieveCounter(String world, String section, String difficulty){
    //     UnityEngine.Debug.Log("Retrieving Counter");
    //     var firestore = FirebaseFirestore.DefaultInstance;
    //     DocumentReference docRef = firestore.Document("QnA/"+world+"/Sections/"+section+"/difficulty/"+difficulty);
    //     docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
    //     {
    //     DocumentSnapshot snapshot = task.Result;
    //     if (snapshot.Exists) {
    //         Dictionary<string, object> city = snapshot.ToDictionary();
    //     foreach (KeyValuePair<string, object> pair in city) {
    //         var counter = pair.Value;
    //         UnityEngine.Debug.Log(counter.ToString());
    // }
        
        // CounterModel counterData=snapshot.ConvertTo<CounterModel>();
        // int counter = counterData.counterValue;
        // counter=counter+1;
        // UnityEngine.Debug.Log(counter.ToString());
    //     } else {
    //         UnityEngine.Debug.Log(String.Format("Retrieving Counter Failed"));
    //     }
    //     });

    //     return counter

    // }
    
    public static void updateCounter(String world, String section, String difficulty, int counterint){
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document("QnA/"+world+"/Sections/"+section+"/difficulty/"+difficulty).UpdateAsync("counter",counterint);
    }
    public static void setAnswers(QuestionModel questionData, String world, String section, String difficulty, String questionId){
        string question = questionId;
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
