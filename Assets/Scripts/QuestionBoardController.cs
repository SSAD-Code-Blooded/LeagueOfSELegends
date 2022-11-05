using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This class is used to manage the loading of scenes from View Question Scene.
///
/// It contains function for us to load other scenes when we are in View Question Scene.
public class QuestionBoardController : MonoBehaviour
{
    /// This method is called whenever users wants to leave View Question scene to Teacher Menu.
    ///
    /// It loads Main Menu scene.
    public void CloseButton()
    {
        SceneManager.LoadScene("Teacher Menu");
    }
    
    /// This method is called whenever users wants to leave Error Popup.
    ///
    /// It loads Teacher Menu scene.
    public void onClickToCloseErrorUI()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
