using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ActivateNavMesh: TaskBT
{
    ennemi_AI_Activable navMeshScript;

    public ActivateNavMesh(GameObject follower)
    {
        navMeshScript = follower.GetComponent<ennemi_AI_Activable>(); ;

    }
    public override TaskState Execute()
    {
       navMeshScript.Activate(true);
       return TaskState.Success;
    }
}
