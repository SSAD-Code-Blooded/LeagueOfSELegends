using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

/// This class is used to manage the scene for Main Menu.
///
/// It manages the loading of different scenes from Main Menu scene.
public class MainMenu : MonoBehaviour
{
    /// This method is called whenever users wants to enter story mode.
    ///
    /// It loads Story Mode scene.
    public void storyMode()
    {
        SceneManager.LoadScene("Story Mode");
    }

    /// This method is called whenever users wants to enter challenge mode.
    ///
    /// It loads Challenge Mode scene.
    public void challenge()
    {
        SceneManager.LoadScene("4.1 Challenge Create Or Join Room Menu");
    }

    /// This method is called whenever users wants to see his / her profile.
    ///
    /// It loads profle scene.
    public void displayProfile()
    {
        SceneManager.LoadScene("Profile");
    }
    
    /// This method is called whenever users wants to see leaderboard.
    ///
    /// It loads leaderboard scene.
    public void displayLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    /// This method is called whenever users wants to log out.
    ///
    /// It loads brings user to student login scene.
    public void CloseButton()
    {   
        SceneManager.LoadScene("1.1 Student Login");
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignOut();
    }



}
