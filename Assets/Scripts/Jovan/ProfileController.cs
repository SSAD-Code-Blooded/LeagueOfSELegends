using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This class is used to manage the next scene while in Profile scene.
///
/// It loads new scene while in Profile scene.
public class ProfileController : MonoBehaviour
{
    /// This method is called whenever users wants to leave profile scene to main menu.
    ///
    /// It loads Main Menu scene.
    public void CloseButton()
    {
        SceneManager.LoadScene("3 Main Menu");
    }
}
