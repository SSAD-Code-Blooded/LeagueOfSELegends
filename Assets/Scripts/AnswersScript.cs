using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnswersScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;
    public GameObject thisbutton;
     IEnumerator CExecuteAfterTime(float time)
    {
     yield return new WaitForSeconds(time);
     thisbutton.GetComponent<Image>().color = Color.white;
     quizManager.correct();
     }
     IEnumerator WExecuteAfterTime(float time)
    {
     yield return new WaitForSeconds(time);
     thisbutton.GetComponent<Image>().color = Color.white;
     quizManager.wrong();
     }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
