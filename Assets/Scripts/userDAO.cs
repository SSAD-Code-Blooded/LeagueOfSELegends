using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
public static class userDAO 
{
    public static string userProfileLevel;

    
    
    public static DocumentReference getUserDoc(){
        FirebaseUser currentuser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        string userEmail=currentuser.Email;
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        DocumentReference docRef = db.Document("Users/"+userEmail);
        return docRef; 
    }

    public static void setUserProgressLevel(){
        DocumentReference docRef = getUserDoc();
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
        var snapshot = task.Result;
        if (snapshot.Exists)
        {
            var userData = snapshot.ConvertTo<UserData>();
            userProfileLevel = userData.UserProgressLevel;
        }
        else
        {
            UnityEngine.Debug.Log(System.String.Format("Document {0} does not exist!", snapshot.Id));
            return;
        }
        });
        return;
    }
    public static string getUserProgressLevel(){
        return userProfileLevel;
    }

    public static int getStoryModeScore(){
        int score=0;
        bool updated=false;
        DocumentReference docRef = getUserDoc();
        getScore();
        async Task getScore(){
            await docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    if(task.IsCanceled){
                        return;
                    }
                    if(task.IsFaulted){
                        return;
                    }
                    else{
                        DocumentSnapshot snapshot = task.Result;
                        var userData = snapshot.ConvertTo<UserData>();
                        score= userData.StoryModeScore;
                        UnityEngine.Debug.Log($"score retrieved: {score}");
                        updated=true;
                    }
                
                }
            );
        }
        while(true){
            if (updated){
                return score; 
            }
        }
        return score;  
    }

    public static void setUserProgressLevel(string userProgressLevel){
        DocumentReference docRef = getUserDoc();
        docRef.UpdateAsync("UserProgressLevel", userProgressLevel).ContinueWith(task => {
            UnityEngine.Debug.Log($"Updated userProgressLevel to {userProgressLevel}!");
        }); 
    }
    public static void setStoryModeScore(int storyModeScore){
        
        
        DocumentReference docRef = getUserDoc();
        UnityEngine.Debug.Log("writing to database..");

        Dictionary<string, object> updates = new Dictionary<string, object>
            {
                    { "StoryModeScore", storyModeScore }
            };
        docRef.UpdateAsync(updates).ContinueWith(task => {
            UnityEngine.Debug.Log($"Updated storyModeScore to {storyModeScore}!");
        });    
    }
    public static void setChallengeModeWins(string email,int score){ 
        
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Document("Users/"+email);

        docRef.UpdateAsync("ChallengeModeWins", score).ContinueWith(task => {
            UnityEngine.Debug.Log($"Updated ChallengeModeWins to {score}!");
        }); 
    }
}
