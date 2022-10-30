using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase.Firestore;

[FirestoreData]

public struct TeacherData
{
    [FirestoreProperty]
    public string role {get; set;}

    [FirestoreProperty]
    public string EmailAddress {get; set;}
}

public class TeacherLoginManager : MonoBehaviour
{   

    public TMP_InputField email, password;
    public GameObject errorUI;
    public TMP_Text errorMessageToShow;
    public string errorMessage;
    public Button registerButton;

    
    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("Started");
        
        FirebaseFirestoreSettings Settings=FirebaseFirestore.DefaultInstance.Settings;  
        Settings.PersistenceEnabled = false;

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task=>{
            Firebase.DependencyStatus DependencyStatus = task.Result;
            if(DependencyStatus ==  Firebase.DependencyStatus.Available){

            }else{
                UnityEngine.Debug.LogError(
                    "Could not resolve all Firebase dependencies: " + DependencyStatus
                );
                
            }
        });
    }

    public void OnClickSignIn(){

        UnityEngine.Debug.Log("Clicked SignIn");
        Login();
        async Task Login(){
            bool loginStatus=false;
            string errorMessage="";
            await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => {
                    if (task.IsCanceled) {
                        UnityEngine.Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                        UnityEngine.Debug.Log("SignIn cancelled");
                        errorMessage = "Sign In Cancelled";
    
                        return;
                    }
                    if (task.IsFaulted) {
                        UnityEngine.Debug.Log("SignIn Failed");
                        UnityEngine.Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception.InnerExceptions[0].Message);
                        errorMessage = task.Exception.InnerExceptions[0].Message;
                        return;
                    }else{
                        if (email.text=="teacher@gmail.com"){
                            loginStatus=true;
                        }
                        else
                        {
                            errorMessage="Not a teacher account";
                        }
                        
                        
                    }
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    UnityEngine.Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                
            }
        );
        if (loginStatus){
            SceneManager.LoadScene("Teacher Menu"); 
        }
        else{
            errorUI.SetActive(true);
            errorMessageToShow.text = errorMessage; 
        }
        return;
        }
    }
    public void onClickToCloseErrorUI(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

