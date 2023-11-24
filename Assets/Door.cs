using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Activable
{
    Animator openCloseDoor;
    void Start()
    {
        openCloseDoor= GetComponent<Animator>();
    }

    override public void Activate(bool open)
    {
       openCloseDoor.SetTrigger("interact");
    }
}
