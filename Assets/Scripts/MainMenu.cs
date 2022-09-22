using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void storyMode()
    {
        SceneManager.LoadScene("Storymode");
    }

    public void challenge()
    {
        SceneManager.LoadScene("Challenge");
    }

    public void displayProfile()
    {
        SceneManager.LoadScene("Profile");
    }
    
    public void displayLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void CloseButton()
    {
        SceneManager.LoadScene("1.1 Student Login");
    }



}
