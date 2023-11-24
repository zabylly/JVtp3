using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Button : MonoBehaviour
{
    public bool isActive { get; private set; } = false;
    [SerializeField] private Activable[] activables;
    private Animator animator;
    //Color color { get { return isActive ? Color.green : Color.red; } }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void FlipFlopButton()
    {
        isActive = !isActive;

        animator.SetTrigger("interact");

        //active/désactive tout les élements qui doivent etre impacter par le bouton
        foreach (var activable in activables)
         activable.Activate(isActive);

    }
}
