using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This class is used to teleport player to quiz scene when stepping on the portal.
///
/// It contains function for us to load Story Mode Quiz scenee when we are in World.
public class TeleportToBattle : MonoBehaviour
{
    /// This method is called whenever users enters a portal.
    ///
    /// It loads Story Mode Quiz
    void OnTriggerEnter2D(Collider2D other) {
        SceneManager.LoadScene("Story Mode Quiz");       
    }

}
