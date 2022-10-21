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

public struct UserData
{
    [FirestoreProperty]
    public string UserName {get; set;}

    [FirestoreProperty]
    public string EmailAddress {get; set;}

    [FirestoreProperty]
    public string MatriculationNo {get;set;}

    [FirestoreProperty]
    public string UserProgressLevel {get;set;}
}

public class LoginManager : MonoBehaviour
{   

    public TMP_InputField email, password,username,matricNumber;
    public GameObject errorUI;
    public TMP_Text errorMessageToShow;
    public string errorMessage;
    public Button signInButton,registerButton;

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started");
        
        FirebaseFirestoreSettings Settings=FirebaseFirestore.DefaultInstance.Settings;  
        Settings.PersistenceEnabled = false;

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task=>{
            Firebase.DependencyStatus DependencyStatus = task.Result;
            if(DependencyStatus ==  Firebase.DependencyStatus.Available){

            }else{
                Debug.LogError(
                    "Could not resolve all Firebase dependencies: " + DependencyStatus
                );
                
            }
        });

        registerButton.onClick.AddListener(()=>
        {
            var userData = new UserData{
                UserName = username.text,
                EmailAddress = email.text,
                MatriculationNo = matricNumber.text,
                UserProgressLevel = "Easy",
            };
            var firestore = FirebaseFirestore.DefaultInstance;
            firestore.Document("Users/"+email.text).SetAsync(userData);
        });
    }

    public void OnClickSignIn(){

        Debug.Log("Clicked SignIn");
        Login();
        async Task Login(){
            bool loginStatus=false;
            string errorMessage="";
            await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => {
                    if (task.IsCanceled) {
                        Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                        Debug.Log("SignIn cancelled");
    
                        return;
                    }
                    if (task.IsFaulted) {
                        Debug.Log("SignIn Failed");
                        Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception.InnerExceptions[0].Message);
                        errorMessage = task.Exception.InnerExceptions[0].Message;
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
        else{
            errorUI.SetActive(true);
            errorMessageToShow.text = errorMessage; 
        }
        return;
        }
    }
    public void OnClickSignUp(){
        Debug.Log("Clicked Signup");
        SignUp();
        async Task SignUp(){
            bool signupStatus=false;
            string errorMessage="";
            await FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => {
                    //Add checks for signup here
                    if(username.text==""){
                        Debug.Log("invalid Username");
                        errorMessage = "Invalid Username!";
                        return;
                    }

                    if(matricNumber.text.Length<9){
                        Debug.Log("invalid matric number");
                        errorMessage = "Matriculation Number should have 9 characters!";
                        return;
                    }
                    
                    if (task.IsCanceled) {
                        Debug.LogError("SignUpWithEmailAndPasswordAsync was canceled.");
                        Debug.Log("SignUp cancelled");
    
                        return;
                    }
                    if (task.IsFaulted) {
                        Debug.Log("SignUp Failed");
                        Debug.LogError("SignUpWithEmailAndPasswordAsync encountered an error: " + task.Exception.InnerExceptions[0].Message);
                        errorMessage = task.Exception.InnerExceptions[0].Message;
                        return;
                    }else{
                        signupStatus=true;
                    }
                    // Firebase user has been created.
                    Debug.Log("Signup Successful");
                    Firebase.Auth.FirebaseUser newUser = task.Result;
                    Debug.LogFormat("User signed up successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                
            }
        );
        if (signupStatus){
            SceneManager.LoadScene("3 Main Menu"); 
        }
        else{
            errorUI.SetActive(true);
            errorMessageToShow.text = errorMessage; 
        }
        return;
        }




        // bool signupStatus=true;
        // string errorMessage="";
        // FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => {
        //     if (task.IsCanceled) {
        //         Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
        //         signupStatus=false;
        //         errorMessage = task.Exception.InnerExceptions[0].Message;
        //     }
        //     if (task.IsFaulted) {
        //         Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
        //         signupStatus=false;
        //         errorMessage = task.Exception.InnerExceptions[0].Message;
        //     }
        //     if (signupStatus){
        //         // Firebase user has been created.
        //         Debug.Log("Signup Successful");
        //         Firebase.Auth.FirebaseUser newUser = task.Result;
        //         Debug.LogFormat("User signed up successfully: {0} ({1})",
        //         newUser.DisplayName, newUser.UserId);
        //     }else{
        //         Debug.LogError("error: "+errorMessage);
        //         errorUI.SetActive(true);
        //         errorMessageToShow.text = errorMessage;
        //     }


        //     }
        // );
        
        // return;
    }

    public void onClickBackToUserSelection(){
        SceneManager.LoadScene("1 User Selection");
    }

    public void onClickToSignupScene(){
        SceneManager.LoadScene("2 Student Register");
    }

    public void onClickToCloseErrorUI(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void onClickToStudentLoginScene(){
        SceneManager.LoadScene("1.1 Student Login");
    }
    
}
