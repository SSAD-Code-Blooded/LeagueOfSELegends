using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// This class is used to manage the loading of scenes from Story Mode Scene.
///
/// It contains function for us to load other scenes when we are in Story Mode Scene.
public class StoryModeController :MonoBehaviour
{
    /// This method is called whenever users selects the Software Engineering World.
    ///
    /// It loads World scene
    public void selectSoftwareEngineering()
    {
        SceneManager.LoadScene("World");
    }
    
    /// This method is called whenever users selects the back button.
    ///
    /// It loads Main Menu
    public void onClickBackToMainMenu()
    {
        SceneManager.LoadScene("3 Main Menu");
    }
}
