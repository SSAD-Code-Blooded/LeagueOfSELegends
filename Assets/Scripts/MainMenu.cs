using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void storyMode()
    {
        SceneManager.LoadScene("Story Mode");
    }

    public void challenge()
    {
        SceneManager.LoadScene("4.1 Challenge Create Room Menu");
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
