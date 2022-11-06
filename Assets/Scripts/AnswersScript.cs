using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnswersScript : MonoBehaviour
{
    public bool isCorrect = false; /**< flag for correct*/
    public QuizManager quizManager; /**< Accesses quizmanager gameobject*/
    public GameObject thisbutton; /**< the button for each answer*/

    ///
    /// the following method is an IEnumerator which excecutes after a time delay when correct answer is clicked
    ///
    
    IEnumerator CExecuteAfterTime(float time)
    {
     yield return new WaitForSeconds(time);
     thisbutton.GetComponent<Image>().color = Color.white;
     quizManager.correct();
     }

    ///
    /// the following method is an IEnumerator which excecutes after a time delay when wrong answer is clicked
    ///

    IEnumerator WExecuteAfterTime(float time)
    {
     yield return new WaitForSeconds(time);
     thisbutton.GetComponent<Image>().color = Color.white;
     quizManager.wrong();
     }

    ///
    /// the following method is an IEnumerator which excecutes after a time delay when wrong answer is clicked
    ///

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
