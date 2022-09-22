using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudentRegisterController : MonoBehaviour
{
    public void closeButton()
    {
        SceneManager.LoadScene("1.1 Student Login");
    }

    public void registerAccount()
    {
        SceneManager.LoadScene("3 Main Menu");
    }


}
