using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTitleController : MonoBehaviour
{
    public void selectStart()
    {
        SceneManager.LoadScene("1 User Selection");
    }
    public void CloseButton()
    {
        Application.Quit();
        Debug.Log("quitting program now");
        
    }
}
