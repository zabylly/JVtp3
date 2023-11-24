using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ennemi_AI_Activable : Activable
{
    // Start is called before the first frame update
    [SerializeField] private Transform targetTransform;
    [SerializeField] private bool isUpdatingTargetPosition = true;
    private NavMeshAgent agent;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    override public void Activate(bool activate)
    {
        agent.enabled = activate;
        if (activate)
        {
            StartCoroutine(UpdateTargetPosition());//l'ennemi commence seulement à bouger lorsque cette fonction est appelé
        }
    }
    private IEnumerator UpdateTargetPosition()
    {
        //a chaque demi-seconde l'ennemi vérifie la position du joueur
        while (isUpdatingTargetPosition && agent.enabled)
        {
            agent.destination = targetTransform.position;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
