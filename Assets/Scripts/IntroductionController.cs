using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This class is used to manage the introduction scene at the start
///
/// It will wait for 3 seconds for the video to finish loading before going to Game Title scene
public class IntroductionController : MonoBehaviour
{
    public float wait_time = 3f;
    
    void Start()
    {
        StartCoroutine(Wait_for_intro());

    }

    IEnumerator Wait_for_intro()
    {
        yield return new WaitForSeconds(wait_time);

        SceneManager.LoadScene("0.1 Game Title");
    }
}
