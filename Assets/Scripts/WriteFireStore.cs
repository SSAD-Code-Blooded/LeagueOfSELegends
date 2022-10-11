using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Firebase.Firestore;
using TMPro;





public class WriteFireStore : MonoBehaviour
{
    [SerializeField] private TMP_InputField _UserNameField;
    [SerializeField] private TMP_InputField _EmailAddressField;
    [SerializeField] private TMP_InputField _MatriculationNoField;
    [SerializeField] private Button _submitButton;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
