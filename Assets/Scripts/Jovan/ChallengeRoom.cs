using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class ChallengeRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public GameObject waitingpanel;
    public TextMeshProUGUI roomN;
    public TMP_Dropdown worldDD;
    public TMP_Dropdown sectionDD;
    public TMP_Dropdown levelDD;
    public static string challengeWorld;
    public static string challengeSection;
    public static string challengeLevel;
    public static Vector3 position;
    public static int playerID;
    public static string player1email;
    public static string player2email;
    private bool p2notyet;
    public GameObject join_room_error_canvas;
    public TMP_Text join_room_error_text;

    private int MaxPlayersPerRoom = 2;
    private string user1Email;

    // Start is called before the first frame update
    void Start()
    {   
        FirebaseUser currentuser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        var userEmail=currentuser.Email;
        user1Email=userEmail;
        waitingpanel.SetActive(false);
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
        SceneManager.LoadScene(sceneName:"3 Main Menu");
    }

    public void onBackButtonToCreateRoomLobby() 
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Player has left room to create room lobby.");
    }

    public override void OnConnectedToMaster()
    {
        // means if the MasterClient loads Scene B, all other clients will load Scene B as well
        PhotonNetwork.AutomaticallySyncScene = true; 
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
    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() 
        { 
            IsVisible = true, IsOpen = true, MaxPlayers = 2 
        };

        if (PhotonNetwork.CreateRoom(user1Email, roomOptions, TypedLobby.Default))
        {
            Debug.Log("Create room successfully");
        }
        else
        {
            Debug.Log("Create room failed");
        }
        challengeWorld = worldDD.options[worldDD.value].text;
        challengeSection = sectionDD.options[sectionDD.value].text;
        challengeLevel = levelDD.options[levelDD.value].text;
        Debug.Log(challengeLevel + " " + challengeSection + " " + challengeWorld);
    }

    public void OnClick_EnterRoom()
    {
        if (PhotonNetwork.JoinRoom(roomNameInput.text))
        {
                Debug.Log("Player Joined in the Room " + roomNameInput.text);
        }
        else
        {   
            join_room_error_canvas.SetActive(true);
            join_room_error_text.text=$"Invalid Room ID! Please try again!";
            Debug.Log("Invalid Room ID! Please try again!");
        }
    } 


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(roomList);
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room);
        }
    }
    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        Debug.Log("Create Room Failed: " + codeAndMessage[1]);

    }

    private void OnCreatedRoom(short returnCode, string message)
    {
        Debug.Log("Room Created Successfully");
        PhotonNetwork.JoinRoom(user1Email);
    }

    public static void OnClickRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {
            Debug.Log("Player Joined in the Room" + roomName);
        }
        else
        {
            Debug.Log("Failed to join in the room, please fix the error!");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client joined a room");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount == 1)
        {
            ExitGames.Client.Photon.Hashtable challengeinfo = new ExitGames.Client.Photon.Hashtable();
            challengeinfo.Add("world", challengeWorld);
            challengeinfo.Add("section", challengeSection);
            challengeinfo.Add("level", challengeLevel);
            challengeinfo.Add("player1email", user1Email);
            player1email = user1Email;
            
            PhotonNetwork.CurrentRoom.SetCustomProperties(challengeinfo);
            playerID = 1;
        }
        else
        {
            challengeLevel = PhotonNetwork.CurrentRoom.CustomProperties["level"].ToString();
            challengeSection = PhotonNetwork.CurrentRoom.CustomProperties["section"].ToString();
            challengeWorld = PhotonNetwork.CurrentRoom.CustomProperties["world"].ToString();
            player1email = PhotonNetwork.CurrentRoom.CustomProperties["player1email"].ToString();

            ExitGames.Client.Photon.Hashtable challengeinfo = new ExitGames.Client.Photon.Hashtable();
            challengeinfo.Add("world", challengeWorld);
            challengeinfo.Add("section", challengeSection);
            challengeinfo.Add("level", challengeLevel);
            challengeinfo.Add("player1email",player1email);

            challengeinfo.Add("player2email", user1Email);
            player2email = user1Email;  
            
            PhotonNetwork.CurrentRoom.SetCustomProperties(challengeinfo);
            Debug.Log("After Join, " + challengeLevel + " " + challengeSection + " " + challengeWorld);
            playerID = 2;

        }

        if (playerCount != MaxPlayersPerRoom)
        {
            Debug.Log("client is waiting for opponent");
            Debug.Log(PhotonNetwork.CurrentRoom.Name + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
            waitingpanel.SetActive(true);
            roomN.SetText("Room No.: " + PhotonNetwork.CurrentRoom.Name + " Players: " + PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/" + MaxPlayersPerRoom);

        }
        else
        {
            Debug.Log("Matching is ready");
            roomN.SetText("Room No.: " + PhotonNetwork.CurrentRoom.Name + " Players: " + PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/" + MaxPlayersPerRoom);
            if(playerID==1){
                player2email = PhotonNetwork.CurrentRoom.CustomProperties["player2email"].ToString();}
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayersPerRoom)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            Debug.Log("Match is ready to begin");

            PhotonNetwork.LoadLevel("Challenge Mode Quiz");
        }
        else
        {
            Debug.Log(PhotonNetwork.CurrentRoom.Name + PhotonNetwork.CurrentRoom.PlayerCount.ToString());
        }
    }

    // when leaveRoom() is called
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(sceneName:"4.1 Challenge Create Or Join Room Menu");

        base.OnLeftRoom();
    }

}
