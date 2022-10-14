using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryModeController :MonoBehaviour
{
    public void selectSoftwareEngineering()
    {
        SceneManager.LoadScene("World");
    }
    
    public void onClickBackToMainMenu()
    {
        SceneManager.LoadScene("3 Main Menu");
    }
}
