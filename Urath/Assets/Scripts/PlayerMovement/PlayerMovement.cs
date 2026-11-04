using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 moveInput;
    private Animator animator;
    [HideInInspector] public bool walking;
    [HideInInspector] public Vector2 lastCord;



    bool isPaused;

    private CinemachineVirtualCamera vcam;
    private Transform playerTrans;

    void Start()
    {
        //Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vcam = GameObject.FindAnyObjectByType<CinemachineVirtualCamera>();
        playerTrans = GetComponent<Transform>();

        vcam.Follow = playerTrans;
    }

    void Update()
    {
        if(PauseController.isGamePaused){
            rb.linearVelocity = Vector2.zero; //stop movement
            animator.SetBool("isWalking", false); //stop animation
            return; 
        }
        rb.linearVelocity = moveInput * moveSpeed;
        animator.SetBool("isWalking", rb.linearVelocity.magnitude > 0);
        if(rb.linearVelocity.magnitude > 0){
            walking = true;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled){
            animator.SetBool("isWalking", false);
            walking = false;
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
            lastCord = moveInput;
        }

        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

 //   void SetFocus(Interactable newFocus)
   // {
     //   focus = newFocus;
   // }
}
