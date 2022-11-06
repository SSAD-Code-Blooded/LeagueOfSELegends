using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadingBar : MonoBehaviour
{
    private Slider slider; /**< Slider object that controls loading*/
    public float FillSpeed = 0.3f; /**< Speed of loading*/
    private float targetProgress = 0; /**< target progress of loading*/
    public bool flag = false; /**< flag for loading done*/

    ///
    /// slider initiation
    ///

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        IncrementProgress(1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value < targetProgress)
        {
            slider.value += FillSpeed * Time.deltaTime;
        }

        if(slider.value == targetProgress)
        {
            loadingDone();
        }
        
    }

    ///
    /// Increases the loading bar progress
    ///

    public void IncrementProgress(float newProgress)
    {
        targetProgress = slider.value + newProgress;
    }

    ///
    /// function is called when loading is complete
    ///

    public void loadingDone()
    {
        flag = true;
    }
}
