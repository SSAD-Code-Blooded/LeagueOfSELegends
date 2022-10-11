using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Firebase;
using Firebase.Analytics;

public class Firebaseinit : MonoBehaviour
{
    public UnityEvent onFirebaseLoaded = new UnityEvent();
    public UnityEvent onFirebaseFailed = new UnityEvent();
    // Start is called before the first frame update
    async void Start()
    {
        var DependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        if (DependencyStatus == DependencyStatus.Available){
            onFirebaseLoaded.Invoke();
        }
        else{
            onFirebaseFailed.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
