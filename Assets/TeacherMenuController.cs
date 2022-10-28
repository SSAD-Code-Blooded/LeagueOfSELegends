using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeacherMenuController : MonoBehaviour
{
    public void addQuestion()
    {
        SceneManager.LoadScene("Add Question");
    }
    
    public void viewQuestion()
    {
        SceneManager.LoadScene("View Questions");
    }

    public void backButton()
    {
        SceneManager.LoadScene("1.2 Teacher Login");
    }
}



