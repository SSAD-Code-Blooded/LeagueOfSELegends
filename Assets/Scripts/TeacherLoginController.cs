using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class TeacherLoginController : MonoBehaviour
{ 
    public void CloseButton()
    {
        SceneManager.LoadScene("1 User Selection");
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignOut();
    }
}
