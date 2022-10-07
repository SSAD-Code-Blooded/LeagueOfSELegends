using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;

public class getUserData : MonoBehaviour
{
    public TMP_Text userInformation;
    private ListenerRegistration _listener;


    // Start is called before the first frame update
    void Start()
    {
        FirebaseUser currentuser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        var userEmail=currentuser.Email;
        var firestore = FirebaseFirestore.DefaultInstance;
        var _listener=firestore.Document("Users/"+userEmail).Listen(snapshot=>{
            // Assert.IsNull(task.Exception);
            var userData = snapshot.ConvertTo<UserData>();

            userInformation.text=$"Username: {userData.UserName}\nEmail: {userData.EmailAddress}\nMatriculation No. : {userData.MatriculationNo}\nCharacter: {userData.Character}";
            
    });}

    void OnDestroy(){
        _listener.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

