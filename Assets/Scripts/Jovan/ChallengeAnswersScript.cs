using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// This class is used to manage the answering of questions for Challenge's quiz scene.
///
/// It manages the changing of button's colour when player click on it like turning it red when wrong answer is clicked.
public class ChallengeAnswersScript : MonoBehaviour
{
    public bool isCorrect = false; /**< a boolean variable to check if answer is correct. True if correct. False if wrong */
    public ChallengeQuizManager challengeQuizManager; /**< placeholder in unity UI to get access to ChallengeQuizManager script */
    public GameObject thisbutton; /**< placeholder in unity UI to get access to the button being clicked */


    IEnumerator CExecuteAfterTime(float time)
    {
     yield return new WaitForSeconds(time);
     thisbutton.GetComponent<Image>().color = Color.white;
     challengeQuizManager.correct();
    }
    

    IEnumerator WExecuteAfterTime(float time)
    {
     yield return new WaitForSeconds(time);
     thisbutton.GetComponent<Image>().color = Color.white;
     challengeQuizManager.wrong();
    }


    /// This method is called whenever user clicks on a button to answer question.
    ///
    /// It manages the changing of button's colour when player click on it like turning it red when wrong answer is clicked.
    public void Answer()
    {
        if(isCorrect)
        {
            Debug.Log("Correct Answer");
            thisbutton.GetComponent<Image>().color = Color.green;
            StartCoroutine(CExecuteAfterTime(0.1f));
            
        }
        else
        {
            Debug.Log("Wrong Answer");
            thisbutton.GetComponent<Image>().color = Color.red;
            StartCoroutine(WExecuteAfterTime(0.1f));
        
        }
        
    }
}
