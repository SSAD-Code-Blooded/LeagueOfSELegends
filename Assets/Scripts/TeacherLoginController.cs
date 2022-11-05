using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

/// This class is used to manage the loading of scenes from Teacher Login Scene.
///
/// It contains function for teacher to logout.
public class TeacherLoginController : MonoBehaviour
{ 

    /// This method is called whenever teacher selects the logout button.
    ///
    /// It loads User Selection Scene and logs the user out.
    public void CloseButton()
    {
        SceneManager.LoadScene("1 User Selection");
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignOut();
    }
}
