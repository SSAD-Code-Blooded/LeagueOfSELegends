using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Auth;

/// This class is used to manage the scene for Challenge Room. (room creation / joining a room)
///
/// It manages the loading of different scenes from Challenge Lobby scene and the overall flow of room creation / room joining.
public class ChallengeRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput; /**< placeholder in unity UI to get access to the roomNameInput */
    public GameObject waitingpanel; /**< placeholder in unity UI to get access to the waitingpanel */
    public TextMeshProUGUI roomN; /**< placeholder in unity UI to get access to the roomN */
    public TMP_Dropdown worldDD; /**< placeholder in unity UI to get access to the worldDD */
    public TMP_Dropdown sectionDD; /**< placeholder in unity UI to get access to the sectionDD */
    public TMP_Dropdown levelDD; /**< placeholder in unity UI to get access to the levelDD */
    public static string challengeWorld; /**< this declares the selected ChallengeWorld */
    public static string challengeSection; /**< this declares the selected ChallengeSection */
    public static string challengeLevel; /**< this declares the selected challengelevel */
    public static int playerID; /**< this declares the playerID */
    public static string player1email; /**< this declares the email of player 1 */
    public static string player2email; /**< this declares the email of player 2 */
    private bool p2notyet;
    public GameObject join_room_error_canvas; /**< placeholder in unity UI to get access to the join_room_error_canvas */
    public TMP_Text join_room_error_text; /**< placeholder in unity UI to get access to the join_room_error_text */

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
    
    /// This method is called whenever users wants to leave lobby to main menu.
    ///
    /// It loads Main Menu scene.
    public void onBackButtonToMainMenu()
    {
        PhotonNetwork.LeaveLobby();
        Debug.Log("Player has left lobby to main menu.");
        SceneManager.LoadScene(sceneName:"3 Main Menu");
    }

    /// This method is called whenever users is in the newly created room and wants to go back to Create / Join room lobby.
    ///
    /// It loads Create / Join room lobby.
    public void onBackButtonToCreateRoomLobby() 
    {
        PhotonNetwork.LeaveRoom();
        Debug.Log("Player has left room to create room lobby.");
    }

    /// This method will be used to override OnConnectedToMaster function.
    ///
    /// It added the syncing of all players and initialise user 1 email and trigger join lobby.
    public override void OnConnectedToMaster()
    {
        // means if the MasterClient loads Scene B, all other clients will load Scene B as well
        PhotonNetwork.AutomaticallySyncScene = true; 
        PhotonNetwork.NickName = user1Email;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    /// This method will be used to override OnJoinedLobby function.
    ///
    /// It allows us to see debug message when user joined lobby or is not in room.
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        if (!PhotonNetwork.InRoom)
        {
            Debug.Log("Notinroom");
        }
    }

    /// This method will be called when users clicks on Create Room after selecting world, section and level.
    ///
    /// It will create a room and print debug message to let us know if room is created successfully.
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

    /// This method will be called when users clicks on Join Room after keying in room ID
    ///
    /// It will bring player to the room if room ID is valid and print error message if room ID is invalid.
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

    /// This method will be used to override OnRoomListUpdate function.
    ///
    /// It print out all the room available in the server.
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

    /// This method will be used to override OnJoinedRoom function.
    ///
    /// It will do the logic flow and initialise the room details with the player details.
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

    /// This method will be used to override OnPlayerEnteredRoom function.
    ///
    /// It will do the logic flow and only start game when 2 players are in the room.
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

    /// This method will be used to override OnLeftRoom function.
    ///
    /// It will be called when LeaveRoom() is called. Once called, it will load Create Room / Join Room Menu.
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(sceneName:"4.1 Challenge Create Or Join Room Menu");
        base.OnLeftRoom();
    }

}
