using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This class is used to manage the scene for Challenge's aspect.
///
/// It manages the loading of different scenes from Challenge scene.
public class ChallengeManager : MonoBehaviour
{

    /// This method is called whenever user wants to go back to MainMenu button in Challenge Menu Scene.
    ///
    /// It brings the user back to Main Menu Scene.
    public void onClickBackToMainMenu()
    {
        SceneManager.LoadScene("3 Main Menu");
    }

    /// This method is called whenever user clicks on back to Challenge Menu Scene.
    ///
    /// It brings user to Challenge Menu Scene.
    public void onClickBackToChallengeMenu()
    {
        SceneManager.LoadScene("4 Challenge Menu");
    }

}
