using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CountdownTimer : MonoBehaviour
{

    float currentTime = 0f; /**< the time remaining*/
    float startingTime = 30f; /**< initialised timer value*/

    public Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if(currentTime <= 0){
            currentTime = 0;
        }
        
    }
}
