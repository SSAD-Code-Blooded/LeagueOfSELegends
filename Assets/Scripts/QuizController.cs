using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Auth;
using System.Linq;

public class QuizController : MonoBehaviour
{
    public void backButton()
    {
        SceneManager.LoadScene("World");
    }
}
