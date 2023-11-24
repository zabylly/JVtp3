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
        //quand le sol est touché, le personnage peux à nouveau sauter
        inputText.GroundTouched();
    }
}
