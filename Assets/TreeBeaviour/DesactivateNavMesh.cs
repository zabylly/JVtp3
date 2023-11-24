using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class DesactivateNavMesh : TaskBT
{
    ennemi_AI_Activable navMeshScript;

    public DesactivateNavMesh(GameObject follower)
    {
        navMeshScript = follower.GetComponent<ennemi_AI_Activable>(); ;

    }
    public override TaskState Execute()
    {
        navMeshScript.Activate(false);
        return TaskState.Success;
    }
}
