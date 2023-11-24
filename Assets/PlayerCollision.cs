using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] ShowTime showTime;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ennemi"))
        {
            showTime.Lose();//perd la partie si un ennemei rentre en collision avec lui
        }
    }
}
