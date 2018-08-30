using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : ACharacter {

    private float moveSpeed;
    private Vector3 moveDir;
    private Rigidbody rbody;
    private PlayerMovement pmove;
    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        MvState = MovementState.Idling;
        moveSpeed = 0;
        rbody = GetComponent<Rigidbody>();
        rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        pmove = GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MvState = MovementState.Idling;
        moveDir = pmove.SetDirection();
        if (moveDir.x != 0f || moveDir.z != 0f) MvState = MovementState.Creeping;
        if (Input.GetKey(KeyCode.LeftControl)) MvState = MovementState.Crawling;
        moveSpeed = pmove.SetSpeed(MvState);
	}

    private void FixedUpdate()
    {
        pmove.Move(moveSpeed, moveDir, rbody);
    }

    private void LateUpdate()
    {
        
    }
}
