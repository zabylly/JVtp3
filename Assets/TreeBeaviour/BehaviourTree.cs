using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTree : MonoBehaviour
{
    private Node rootBT;
    [SerializeField] GameObject rotatingSphere;
    [SerializeField] GameObject player;
    [SerializeField] float flyingSpeed;
    [SerializeField] float landingSpeed;
    // Le sphinx s'envole et suit un point en dessou de la pyramide.Quand il voit le joueur , ilk descend et le suit avec le nav mesh pendant 30 secondes, ensuite il remonte
    void Awake()
    {

        TaskBT[] desactivateNavMeshTask =
        {
            new DesactivateNavMesh(gameObject)
        };
        TaskBT[] patrolTask =
        {
            new Follow(gameObject,rotatingSphere,flyingSpeed)
            ,new FindPlayer(gameObject)
        };
        TaskBT[] goDownTask = new TaskBT[]
        {
            new Follow(gameObject,player,landingSpeed),
            new ActivateNavMesh(gameObject)
        };
        TaskBT[] waitTask = new TaskBT[]
        {
           new Wait(30)
        };
        TaskNode desactivateNavMeshNode = new TaskNode("desactivateNavMeshNode", desactivateNavMeshTask);
        TaskNodeRepeater patrolNode = new TaskNodeRepeater("patrolNode", patrolTask);
        TaskNode chasePlayerNode = new TaskNode("chasePlayerNode", goDownTask);
        TaskNode waitNode = new TaskNode("wait",waitTask);
        rootBT = new Sequence("rootBT", new Node[]{ desactivateNavMeshNode, patrolNode, chasePlayerNode,waitNode});

    }

    // Update is called once per frame
    void Update()
    {
        rootBT.Evaluate();
    }
}
