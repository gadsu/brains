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
[RequireComponent(typeof(PlayerDictionary))] // requires a PlayerDictionary component if one is not found it then generates one.
[RequireComponent(typeof(StealthHandler))]
[RequireComponent(typeof(GroanHandler))]
[RequireComponent(typeof(BodyHandler))]
public class Player : ACharacter {

    private float moveSpeed; // stores how fast the player should be moving.
    private Vector3 moveDir; // stores the players movement direction.
    private Rigidbody rbody; // stores the Rigidbody component.

    private PlayerMovement pmove; // stores the PlayerMovement script.
    private PlayerDictionary diction;
    private StealthHandler stealth;
    private GroanHandler groan;
    private BodyHandler body;

    private int animationKey;
    private int backwards;
    private int moving;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        MvState = MovementState.Idling; // sets the players initial movement state.
        moveSpeed = 0f; // sets the players initial speed.
        moveDir = new Vector3(0, 0, 0); // sets the initial direction.
        animationKey = 0;
        backwards = 0;
        moving = 0;

        rbody = GetComponent<Rigidbody>(); // gets and saves the Rigidbody component and access attached to this character.
        pmove = GetComponent<PlayerMovement>(); // gets and saves access to this character PlayerMovement.
        diction = GetComponent<PlayerDictionary>();
        stealth = GetComponent<StealthHandler>();
        groan = GetComponent<GroanHandler>();
        body = GetComponent<BodyHandler>();

        rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MvState = MovementState.Idling;
        moveDir = Vector3.zero;
        moving = 0;
        if (Input.anyKey)
        {
            moveDir = pmove.SetDirection();
            if (moveDir != Vector3.zero) MvState = MovementState.Creeping;
            if (Input.GetKey(KeyCode.LeftShift)) MvState = MovementState.Crawling;

            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }

        moveSpeed = (moveDir != Vector3.zero) ? pmove.SetSpeed((int)MvState) : 0f;
        pmove.RotatePlayer(transform, moveDir, moveSpeed);
        if (moveSpeed > 0f)
        {
            moving = 1;
        }
	}

    private void FixedUpdate()
    {
        backwards = (moveDir.z < 0f) ? -1 : 1;
        if (moveDir * moveSpeed != rbody.velocity)
        {
            pmove.Move(moveSpeed, rbody, moveDir);
            stealth.UpdateStealthState(0, body.GetArms(), body.GetLegs(), (int)MvState);
            groan.SetGroanSpeed((int)MvState, moveSpeed);
        }
    }

    private void LateUpdate()
    {
        animationKey = diction.RetrieveKey(moving, (int)MvState, body.GetArms(), body.GetLegs(), 0);
        diction.Animate(animationKey, moveSpeed, backwards);
        if (groan.UpdateGroanAmount())
            groan.Groan();
    }
}
