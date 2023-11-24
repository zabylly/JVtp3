using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputSystemText : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float turningSpeed = 100;
    [SerializeField] float jumpForce = 5;
    [SerializeField] InputAction TextInputMove;
    [SerializeField] InputAction TextInputJump;
    [SerializeField] InputAction TextInputSprint;
    [SerializeField] InputAction TextInputRotate;
    [SerializeField] InputAction TextInputInteract;

    private Rigidbody rb;
    private bool jumped = false;
    private bool sprint = false;
    private Collider groundDetector;
    Button[] buttons;
    private Vector3 direction;
    private Vector3 rotation;
    private Animator animator;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundDetector = GetComponentInChildren<Collider>();
        buttons = FindObjectsOfType<Button>();

    }
    //Initialisation de déplacement
    private void Awake()
    {
        animator = GetComponent<Animator>();

        TextInputMove.Enable();
        TextInputMove.performed += (x) =>
        {
            direction = x.ReadValue<Vector3>().normalized;
            if(direction == Vector3.zero)
                animator.SetBool("isWalking", false);
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetFloat("backward", direction.z);
            }

        };
        TextInputRotate.Enable();
        TextInputRotate.performed += (x) =>
        {
            rotation = x.ReadValue<Vector3>();
        };
        TextInputJump.Enable();
        TextInputJump.started += (x) =>
        {
            if (!jumped)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                jumped = true;
                animator.SetBool("isJumping", true);
            }
        };
        TextInputSprint.Enable();
        TextInputSprint.started += (x) =>
        {
            sprint = true;
            animator.SetBool("isRunning", true);
        };
        TextInputSprint.canceled += (x) =>
        {
            sprint = false;
            animator.SetBool("isRunning", false);
        };
        
        TextInputInteract.Enable(); 
        TextInputInteract.started += (x) =>
        {
            foreach (Button button in buttons)
            {
                //si le joueur est proche d'un bouton et que celui-ci appuit sur la touche d'activation,le bouton s'allume
                if (Vector3.Distance(button.gameObject.transform.position, gameObject.transform.position) <= 6f)
                {
                    animator.SetTrigger("Interacting");
                    button.FlipFlopButton();
                }
            }
        };
    }
    //supression de boutton problématique au relancement de la scêne
    public void OnDestroy()
    {
        TextInputInteract.Disable();
        TextInputJump.Disable();
        TextInputMove.Disable();
        TextInputSprint.Disable();
    }

    void Update()
    {
        if (direction != Vector3.zero)
        {
            transform.Translate(direction * Time.deltaTime * speed * (sprint?2:1));
        }
        if (rotation != Vector3.zero)
        {
            transform.Rotate(rotation * Time.deltaTime * turningSpeed);
        }
    }
    public void GroundTouched()
    {
        jumped = false;
        animator.SetBool("isJumping", false);
    }
}

