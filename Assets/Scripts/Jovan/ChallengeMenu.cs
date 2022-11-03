using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class ChallengeMenu : MonoBehaviourPunCallbacks
{
    
    private string user1Email = "jova0002@e.ntu.edu.sg";
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.GameVersion = "0.0.0";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("We are connected already.");
            Debug.Log("This is: " + user1Email);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void onBackButtonToMainMenu()
    { 
        PhotonNetwork.LeaveLobby();
        Debug.Log("Player has left lobby to main menu.");
        SceneManager.LoadScene(sceneName:"3 Menu");
    }

    public void onClickToCreateRoomMenu()
    {
        SceneManager.LoadScene(sceneName:"4.1 Challenge Create Room Menu");
    }

    public void onClickToJoinRoomMenu()
    {
        SceneManager.LoadScene(sceneName:"4.2 Challenge Join Room Menu");
    }

    public override void OnConnectedToMaster()
    {
        // means if the MasterClient loads Scene B, all other clients will load Scene B as well
        PhotonNetwork.AutomaticallySyncScene = true; 
        // PhotonNetwork.NickName = ProgramStateController.matricNo;
        PhotonNetwork.NickName = user1Email;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        if (!PhotonNetwork.InRoom)
        {
            Debug.Log("Notinroom");
        }
    }


}
