using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardController : MonoBehaviour
{
    public void CloseButton()
    {
        SceneManager.LoadScene("3 Main Menu");
    }
}
