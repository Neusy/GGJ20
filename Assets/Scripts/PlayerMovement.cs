﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public AudioSource footstep;
    public float runSpeed = 20f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jumping", true);
        } else if (Input.GetButton("Jump")) {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
            animator.SetBool("Crouching", true);
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        // Basic movement
        controller.Move(Time.fixedDeltaTime, horizontalMove, crouch, jump);
        jump = false;
    }

    public void OnLanding() {
        animator.SetBool("Jumping", false);
        if (footstep)
            footstep.Play();
    }

    public void OnCrouching(bool isCrouching) {
        animator.SetBool("Crouching", isCrouching);
    }

    public void STFootstep(int step) {
        // -1 Left, 1 Right
        if (footstep)
            footstep.Play();
    }
}
