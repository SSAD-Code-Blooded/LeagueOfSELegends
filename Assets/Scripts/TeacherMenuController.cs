using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This class is used to manage the loading of scenes from Teacher Menu.
///
/// It contains function for us to load other scenes when we are in Teacher Menu.
public class TeacherMenuController : MonoBehaviour
{
    /// This method is called whenever teacher user selects the Add Question button.
    ///
    /// It loads the Add Question scene.
    public void addQuestion()
    {
        SceneManager.LoadScene("Add Question");
    }
    
    /// This method is called whenever teacher user selects the View Questions button.
    ///
    /// It loads the View Questions scene.
    public void viewQuestion()
    {
        SceneManager.LoadScene("View Questions");
    }

    /// This method is called whenever teacher user selects the Delete Question button.
    ///
    /// It loads the Delete Question scene.
    public void deleteQuestion()
    {
        SceneManager.LoadScene("Delete Question");
    }

    /// This method is called whenever teacher user selects the back button.
    ///
    /// It loads the Teacher Login scene.
    public void backButton()
    {
        SceneManager.LoadScene("1.2 Teacher Login");
    }
}



