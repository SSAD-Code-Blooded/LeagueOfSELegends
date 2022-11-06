using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This class is used to manage the scene flow while in user selection scene.
///
/// It has functions to help load different scenes from user selection scene.
public class UserSelectionController : MonoBehaviour
{
    /// This method is called whenever users wants to login as a student.
    ///
    /// It loads student login scene.
    public void selectStudent()
    {
        SceneManager.LoadScene("1.1 Student Login");
    }

    /// This method is called whenever users wants to login as a teacher.
    ///
    /// It loads teacher login scene.
    public void selectTeacher()
    {
        SceneManager.LoadScene("1.2 Teacher Login");
    }

    /// This method is called whenever users wants to go back to game title scene.
    ///
    /// It closes game title scene.
    public void CloseButton()
    {
        SceneManager.LoadScene("0.1 Game Title");
    }
}