using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This class is used to manage the loading of scenes from Leaderboard Scene.
///
/// It contains function for us to load other scenes when we are in Leaderboard Scene.
public class LeaderboardController : MonoBehaviour
{
    /// This method is called whenever users wants to leave leaderboard scene to main menu.
    ///
    /// It loads Main Menu scene.
    public void CloseButton()
    {
        SceneManager.LoadScene("3 Main Menu");
    }
}
