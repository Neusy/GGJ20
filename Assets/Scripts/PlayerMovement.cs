using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private GameObject player;
    public CharacterController2D controller;
    public Animator animator;
    public AudioSource footstepSource;
    public AudioClip[] footsteps;
    public float runSpeed = 20f;
    private System.Random rnd;

    float horizontalMove = 0f;
    int jump = 0;
    bool crouch = false;
    Vector3 spawn;
    Inventory inventory;
    GameObject collidedNPCReference = null;

    void Awake()
    {
        rnd = new System.Random();
        player = GameObject.Find("Player");
        inventory = GetComponent<Inventory>();
    }

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
        }
        if (Input.GetButtonDown("Crouch")) {
            crouch = true;
            animator.SetBool("Crouching", true);
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }

        if (transform.position.y < -20)
            transform.position = spawn;

        if (Input.GetButtonDown("UseEye")) {
            inventory.use(EnumPickUpType.PickUpType.Eye);
        }
        if (Input.GetButtonDown("UseHead")) {
            inventory.use(EnumPickUpType.PickUpType.Head);
        }
        if (Input.GetButtonDown("UseLeg")) {
            inventory.use(EnumPickUpType.PickUpType.Leg);
        }
        if (Input.GetButtonDown("UseOS")) {
            inventory.use(EnumPickUpType.PickUpType.OS);
        }
        if (Input.GetButtonDown("Repair") && collidedNPCReference) {
            inventory.repair(npcItemMatch(collidedNPCReference.name), collidedNPCReference);
        }
    }

    void FixedUpdate()
    {
        // Basic movement
        controller.Move(Time.fixedDeltaTime, horizontalMove, crouch, jump);

        if (collidedNPCReference)
            updateCollision();

        if ((jump > 0) && (Input.GetButton("Jump"))) {
            // Jump button is still pressed
            jump = 2;
        }
    }

    private void PlayFootsteps() {
        if (!footstepSource || footstepSource.isPlaying)
            return;
        
        footstepSource.clip = footsteps[rnd.Next(footsteps.Length)];
        footstepSource.Play();
    }

    public void OnLanding() {
        spawn = transform.position;
        animator.SetBool("Jumping", false);
        PlayFootsteps();
    }

    public void OnCrouching(bool isCrouching) {
        animator.SetBool("Crouching", isCrouching);
    }

    public void STFootstep() {
        PlayFootsteps();
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (Regex.IsMatch(coll.gameObject.name, "^NPC[0-9]$")) {
            Debug.Log("collision with NPC" + coll.gameObject.name.Substring(3));
            collidedNPCReference = coll.gameObject;
        }
    }

    EnumPickUpType.PickUpType npcItemMatch(string npcName) {
        return (EnumPickUpType.PickUpType) int.Parse(npcName.Substring(3)) - 1;
    }

    void updateCollision() {
        float deltaX = Math.Abs(player.transform.position.x - collidedNPCReference.transform.position.x);
        float deltaY = Math.Abs(player.transform.position.y - collidedNPCReference.transform.position.y);
        if (deltaX > 2 || deltaY > 2) {
            collidedNPCReference = null;
        }
    }
}
