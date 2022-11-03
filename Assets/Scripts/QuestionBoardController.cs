using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionBoardController : MonoBehaviour
{
    public void CloseButton()
    {
        SceneManager.LoadScene("Teacher Menu");
    }
    
    public void onClickToCloseErrorUI()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
