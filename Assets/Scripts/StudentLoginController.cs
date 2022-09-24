using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudentLoginController : MonoBehaviour
{
    public void closeButton()
    {
        SceneManager.LoadScene("1 User Selection");
    }

    public void registerAccount()
    {
        SceneManager.LoadScene("2 Student Register");
    }

        public void loginAccount()
    {
        // SceneManager.LoadScene("3 Main Menu");
    }
}
