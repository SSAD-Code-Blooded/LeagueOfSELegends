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
    public string Question {get; set;} /**< String value of Question */

    [FirestoreProperty]
    public string[] Answers {get; set;}/**< Array of Strings, each element consisting a possible Answer to the Question */

    [FirestoreProperty]
    public int CorrectAnswer{get;set;} /**< Integer that indicates Correct Answer  */

    [FirestoreProperty]
    public string questionUID{get;set;}/**< String that indicates Unique Question ID  */

} /**< Firestore Data to reference each Question in a struct data type */

///
/// Data Access Object that performs Question Related Database Operations 
///
public class QuestionDAO
{    
    ///
    /// Function to Update Counter in Firebase
    ///
    public static void updateCounter(String world, String section, String difficulty, int counterint){
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document("QnA/"+world+"/Sections/"+section+"/difficulty/"+difficulty).UpdateAsync("counter",counterint);
    }

    ///
    /// Function to Create Question in Firebase
    ///
    public static void setAnswers(QuestionModel questionData, String world, String section, String difficulty, String questionId){
        string question = questionId;
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document("QnA/"+world+"/Sections/"+section+"/difficulty/"+difficulty+"/Questions/"+question).SetAsync(questionData);
        UnityEngine.Debug.Log("Question write succesfully!");
    }

    ///
    /// Function to Delete Question in Firebase
    ///
    public static void deleteQuestion(String questionid, String world, String section, String difficulty){
        var firestore = FirebaseFirestore.DefaultInstance;
        firestore.Document("QnA/"+world+"/Sections/"+section+"/difficulty/"+difficulty+"/Questions/"+questionid).DeleteAsync();
        UnityEngine.Debug.Log("Question deleted succesfully!");

    }


    
}
