using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeManager : MonoBehaviour
{

    public void onClickBackToMainMenu()
    {
        SceneManager.LoadScene("3 Main Menu");
    }

    public void onClickBackToChallengeMenu()
    {
        SceneManager.LoadScene("4 Challenge Menu");
    }

    public void onClickToCreateRoomMenu()
    {
        SceneManager.LoadScene("4.1 Challenge Create Room Menu");
    }

    
    public void onClickToJoinMenu()
    {
            SceneManager.LoadScene("4.2 Challenge Join Room Menu");
    }





}
