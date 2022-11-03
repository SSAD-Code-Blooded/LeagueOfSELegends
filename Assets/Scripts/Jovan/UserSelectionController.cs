using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserSelectionController : MonoBehaviour
{
    public void selectStudent()
    {
        SceneManager.LoadScene("1.1 Student Login");
    }
    
    public void selectTeacher()
    {
        SceneManager.LoadScene("1.2 Teacher Login");
    }

    public void CloseButton()
    {
        SceneManager.LoadScene("0.1 Game Title");
    }
}
