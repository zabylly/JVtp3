using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpScript : MonoBehaviour
{
    InputSystemText inputText;
    private void Start()
    {
        inputText = transform.parent.GetComponent<InputSystemText>();
    }
    void OnTriggerEnter()
    {
        //quand le sol est touch�, le personnage peux � nouveau sauter
        inputText.GroundTouched();
    }
}
