using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAnimator : MonoBehaviour
{
    private PlayerMovement player;
    private Animator anim;
    
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        player = playerObj.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {   
        Animations();
    }

    void Animations()
    {
        float moveX = player.moveInput.x;
        float moveY = player.moveInput.y;
        bool areWalking = player.walking;
        float lastY = player.lastCord.y;
        float lastX = player.lastCord.x;

        anim.SetBool("isWalking", areWalking);
        anim.SetFloat("LastInputX", lastX);
        anim.SetFloat("LastInputY", lastY);
        
        anim.SetFloat("InputX", moveX);
        anim.SetFloat("InputY", moveY);
    }
}
