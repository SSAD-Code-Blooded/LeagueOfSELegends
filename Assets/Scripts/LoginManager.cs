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

/**< Stores User Data in struct data type */
public struct UserData
{
    [FirestoreProperty]
    public string UserName {get; set;} /**< String value of Username */

    [FirestoreProperty]
    public string EmailAddress {get; set;} /**< String value of Email Address */

    [FirestoreProperty]
    public string MatriculationNo {get;set;} /**< String value of Matriculation Number */

    [FirestoreProperty]
    public string UserProgressLevel {get;set;} /**< String value of User Progress Level*/

    [FirestoreProperty]
    public int ChallengeModeWins {get;set;} /**< Integer value of Number of Challenge mode wins*/

    [FirestoreProperty]
    public int StoryModeScore {get;set;}  /**< Integer value of Story Mode Score*/
}

///
///Manages and Verifies Login and Creation of Student into the Game
///
public class LoginManager : MonoBehaviour
{   

    public TMP_InputField email, password,username,matricNumber; /**< User Interface input field for email, password, username and matriculation number*/
    public GameObject errorUI; /**< User Interface of Error Message */
    public TMP_Text errorMessageToShow;/**< User Interface Text field for displaying of Error Message */
    public string errorMessage; /**< String value of Error Message */
    public Button signInButton,registerButton;/**< Button for Singning In and Registration */

    
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
        userDAO.setUserProgressLevel();

    }


    ///
    /// Verifies if entered credentials is a student account, Signs student in or returns error message accordingly
    ///
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

    ///
    /// Creates New User Student Account
    ///
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

                    var userData = new UserData{
                        UserName = username.text,
                        EmailAddress = email.text,
                        MatriculationNo = matricNumber.text,
                        UserProgressLevel = "Easy",
                        ChallengeModeWins = 0,
                        StoryModeScore =0
                    };
                    //setting up firestore document for user
                    var firestore = FirebaseFirestore.DefaultInstance;
                    firestore.Document("Users/"+email.text).SetAsync(userData);

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

    }

    ///
    /// Back Button to Enter Type of User Page
    ///
    public void onClickBackToUserSelection(){
        SceneManager.LoadScene("1 User Selection");
    }

    ///
    /// Opens Student Registration User Interface
    ///
    public void onClickToSignupScene(){
        SceneManager.LoadScene("2 Student Register");
    }
    
    ///
    /// Closes Login Error User Interface
    ///
    public void onClickToCloseErrorUI(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    ///Opens Student Login Scene
    public void onClickToStudentLoginScene(){
        SceneManager.LoadScene("1.1 Student Login");
    }
    
}
