using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject player;

    /*public enum PickUpType : uint
    {
        Eye = 0,
        Head = 1,
        Leg = 2,
        OS = 3
    }*/

    public CharacterController2D controller;
    public Animator animator;
    public AudioSource footstep;
    public float runSpeed = 20f;

    float horizontalMove = 0f;
    int jump = 0;
    bool crouch = false;
    Vector3 spawn;
    Inventory inventory;

    void Awake()
    {
        //inventory = GetComponent<Inventory>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        var inventory = player.GetComponent<Inventory2>();

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

        if (Input.GetButtonDown("Eye")) {
            inventory.use(EnumPickUpType.PickUpType.Eye);
        }
        if (Input.GetButtonDown("Head")) {
            inventory.use(EnumPickUpType.PickUpType.Head);
        }
        if (Input.GetButtonDown("Leg")) {
            inventory.use(EnumPickUpType.PickUpType.Leg);
        }
        if (Input.GetButtonDown("OS")) {
            inventory.use(EnumPickUpType.PickUpType.OS);
        }
        if (Input.GetButtonDown("Give")) {
            inventory.give();
            // give dovrebbe funzionare in base alla distanza dal player da un npc al quale viene dato in automatico l'oggetto mancante
        }
    }

    void FixedUpdate()
    {
        // Basic movement
        controller.Move(Time.fixedDeltaTime, horizontalMove, crouch, jump);
        if ((jump > 0) && (Input.GetButton("Jump"))) {
            // Jump button is still pressed
            jump = 2;
        }
    }

    public void OnLanding() {
        spawn = transform.position;
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

    void OnCollisionEnter2D(Collision2D coll) {
        if (Regex.IsMatch(coll.gameObject.name, "^NPC[0-9]$")) {
            // Convert decimal NPC numbers in binary item flags
            uint item = (uint)Math.Pow(2, uint.Parse(coll.gameObject.name.Substring(3)) - 1);
            if (inventory.CarryingItem(item)) {
                inventory.UseItem(item);
                coll.gameObject.GetComponent<NPCStatus>().OnRepair();
            }
        }
/*        if (coll.gameObject.tag.Equals("Player")) {
            OnPickUpEvent.Invoke(itemID);
            Destroy(this.gameObject);
        }
        //GameObject e = Instantiate(<azione desiderata>) as GameObject;
        //e.transform.position = transform.position;
        //Destroy (other.gameObject);
        //this.gameObject.SetActive(false);/**/
    }
}
