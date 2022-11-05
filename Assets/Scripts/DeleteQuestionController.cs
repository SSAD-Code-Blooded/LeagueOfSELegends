using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// This class is used to manage the loading of scenes from Delete Question Scene.
///
/// It contains function for us to load other scenes when we are in Delete Question Scene.
public class DeleteQuestionController : MonoBehaviour
{
    /// This method is called whenever users selects the back button.
    ///
    /// It loads Teacher Menu
    public void backButton()
    {
        SceneManager.LoadScene("Teacher Menu");
    }

    /// This method is called whenever users wants to leave Error Popup.
    ///
    /// It loads Delete Question scene.
    public void onClickToCloseErrorUI()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



