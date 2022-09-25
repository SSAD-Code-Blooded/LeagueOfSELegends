using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public class LoginManager : MonoBehaviour
{
 public TMP_InputField email, password;
    public Button signInButton,registerButton;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task=>{
            Firebase.DependencyStatus DependencyStatus = task.Result;
            if(DependencyStatus ==  Firebase.DependencyStatus.Available){

            }else{
                Debug.LogError(
                    "Could not resolve all Firebase dependencies: " + DependencyStatus
                );
            }
        });
    }

    public void OnClickSignIn(){

        Debug.Log("Clicked SignIn");
        Login();
        async Task Login(){
            bool loginStatus=false;
            await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => {
                    if (task.IsCanceled) {
                        Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                        Debug.Log("SignIn cancelled");

                        return;
                    }
                    if (task.IsFaulted) {
                        Debug.Log("SignIn Failed");
                        Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                        return;
                    }else{
                        loginStatus=true;
                    }
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    Debug.LogFormat("User signed in successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
                
            }
        );
        if (loginStatus){
        
            SceneManager.LoadScene("3 Main Menu"); 
        }
        return;
        }
        
        

    }
    public void OnClickSignUp(){
        Debug.Log("Clicked Signup");
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Debug.Log("Signup Successful");
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed up successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            }
        );
    }

    
}
