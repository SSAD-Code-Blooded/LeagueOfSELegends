using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;

public static class QuestionService
{
    
    public static string calculateProgressLevel(int StoryModeScore)
    {
        if (StoryModeScore < 2){
            return "Easy";
        }
        else if (StoryModeScore<4){
            return "Medium";
        }
        else{
            return "Hard";
        }
    }

    public static int calculateScore(int currentScore,char result){
        
        if (result=='W'){
            int newScore = currentScore+1;
            UnityEngine.Debug.Log($"score change from {currentScore} to {newScore}!");
            return newScore;
        }
        else{
            int newScore = currentScore-1;
            UnityEngine.Debug.Log($"score change from {currentScore} to {newScore}!");
            return Mathf.Max(0,newScore);
        }
    }


    public static void storyModeUpdate(char result){  
        DocumentReference docRef = userDAO.getUserDoc();
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
        var snapshot = task.Result;
        if (snapshot.Exists)
        {
            var userData = snapshot.ConvertTo<UserData>();
            // UnityEngine.Debug.Log(userData.UserProgressLevel);
            int userScore=userData.StoryModeScore;
            int updatedScore = calculateScore(userScore,result);
            string newProgressLevel = calculateProgressLevel(updatedScore);
            userDAO.updateUserProgressLevel(newProgressLevel);
            userDAO.setStoryModeScore(updatedScore);
            userDAO.userProfileLevel = newProgressLevel;
        }
        else
        {
            UnityEngine.Debug.Log(System.String.Format("Document {0} does not exist!", snapshot.Id));
            return;
        }
        });
        return;
    }

    public static void challengeModeUpdate(string email){  
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Document("Users/"+email);

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
        var snapshot = task.Result;
        if (snapshot.Exists)
        {
            var userData = snapshot.ConvertTo<UserData>();
            // UnityEngine.Debug.Log(userData.UserProgressLevel);
            int userChallengeScore=userData.ChallengeModeWins;
            userDAO.setChallengeModeWins(email,userChallengeScore+1);
        }
        else
        {
            UnityEngine.Debug.Log(System.String.Format("Document {0} does not exist!", snapshot.Id));
            return;
        }
        });
        return;
    }


    
}
