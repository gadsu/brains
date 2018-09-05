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
        moveSpeed = 0f;
        moveDir = new Vector3(0, 0, 0);

        rbody = GetComponent<Rigidbody>();
        pmove = GetComponent<PlayerMovement>();

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
