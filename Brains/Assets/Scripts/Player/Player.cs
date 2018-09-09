using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles the players mechanics and animates them.
 * 
 * Author: Paul Manley
 */

[RequireComponent(typeof(Rigidbody))] // requires a Rigidbody component if one is not found it then generates one.
[RequireComponent(typeof(PlayerMovement))] // requires a PlayerMovement component if one is not found it then generates one.
public class Player : ACharacter {

    private float moveSpeed; // stores how fast the player should be moving.
    private Vector3 moveDir; // stores the players movement direction.
    private Rigidbody rbody; // stores the Rigidbody component.
    private PlayerMovement pmove; // stores the PlayerMovement script.

    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        MvState = MovementState.Idling; // sets the players initial movement state.
        moveSpeed = 0f; // sets the players initial speed.
        moveDir = new Vector3(0, 0, 0); // sets the initial direction.

        rbody = GetComponent<Rigidbody>(); // gets and saves the Rigidbody component and access attached to this character.
        pmove = GetComponent<PlayerMovement>(); // gets and saves access to this character PlayerMovement.

        rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update ()
    {
        moveDir = Vector3.zero;
        if (Input.anyKey)
        {
            moveDir = pmove.SetDirection();
            if (moveDir != Vector3.zero) MvState = MovementState.Creeping;
            if (Input.GetKey(KeyCode.LeftShift)) MvState = MovementState.Crawling;
        }

        if (moveDir == Vector3.zero && !Input.GetKey(KeyCode.LeftShift)) MvState = MovementState.Idling;

        moveSpeed = pmove.SetSpeed((int)MvState);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    private void FixedUpdate()
    {
        if(moveDir * moveSpeed != rbody.velocity)
            pmove.Move(moveSpeed, moveDir, rbody);
    }

    private void LateUpdate()
    {
        
    }
}
