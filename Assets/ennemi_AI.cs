using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ennemi_AI : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private bool isUpdatingTargetPosition = true;
    private Transform ennemi;
    private Animator animator;
    private NavMeshAgent agent;
    private bool falling = false;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ennemi = GetComponent<Transform>();
    }
    private void Start()
    {
        StartCoroutine(UpdateTargetPosition());
    }
    private IEnumerator UpdateTargetPosition()
    {
        //a chaque demi-seconde l'ennemi vérifie la position du joueur
        while (isUpdatingTargetPosition)
        {
            agent.destination = targetTransform.position;
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void Update()
    {
        if(agent.isOnOffMeshLink)
        {
            animator.SetBool("offlinkNav", true);
            falling = true;
        }
        else if(falling && !agent.isOnOffMeshLink)
        {
            agent.isStopped = true;
            animator.SetBool("offlinkNav", false);
            falling = false;
        }
    }

    public void Resume()
    {
        agent.isStopped = false;
    }
    public void AdjustXPos()
    {
        ennemi.position = ennemi.position + (ennemi.forward * 2);
    }
}
