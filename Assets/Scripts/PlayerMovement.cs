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
    int jump = 0;
    bool crouch = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if ((jump == 0) && Input.GetButtonDown("Jump"))
        {
            // Requested jump (no validation inside PM.cs)
            jump = 1;
            animator.SetBool("Jumping", true);
        } else if (Input.GetButtonUp("Jump")) {
            jump = 0;
        } else if ((jump > 0) && (Input.GetButton("Jump"))) {
            // Jump button is still pressed
            jump = 2;
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
    }

    public void OnLanding() {
        animator.SetBool("Jumping", false);
        if (footstep)
            footstep.Play();
    }

    public void OnCrouching(bool isCrouching) {
        animator.SetBool("Crouching", isCrouching);
    }

    // Sound callback to be triggered inside the animation
    public void STFootstep(int step) {
        // -1 Left, 1 Right
        if (footstep)
            footstep.Play();
    }
}
