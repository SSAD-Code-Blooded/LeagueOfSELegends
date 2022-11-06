using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTitleController : MonoBehaviour
{
    ///
    /// Upon starting the game, the user selection scene is loaded
    ///
    public void selectStart()
    {
        SceneManager.LoadScene("1 User Selection");
    }
    
    ///
    /// This method exits the game
    ///

    public void CloseButton()
    {
        Application.Quit();
        Debug.Log("quitting program now");
        
    }
}
