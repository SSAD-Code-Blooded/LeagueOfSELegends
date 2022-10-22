using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChallengeAnswersScript : MonoBehaviour
{
    public bool isCorrect = false;
    public ChallengeQuizManager challengeQuizManager;
    public GameObject thisbutton;
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
