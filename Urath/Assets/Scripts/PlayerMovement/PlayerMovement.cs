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
        rb.velocity = moveInput * moveSpeed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(isPaused)
        {
            return;
        }
        animator.SetBool("isWalking", true);
        walking = true;

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
