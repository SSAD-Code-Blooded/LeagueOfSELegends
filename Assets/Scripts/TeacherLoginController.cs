using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeacherLoginController : MonoBehaviour
{ 
    public void CloseButton()
    {
        SceneManager.LoadScene("1 User Selection");
    }
}
