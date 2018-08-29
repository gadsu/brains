using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : ACharacter {
    private float moveSpeed;
    private Vector3 move;
    private Rigidbody rbody;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        MvState = MovementState.Idling;
        moveSpeed = 0;
        rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void FixedUpdate()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.LeftControl) && MvState != MovementState.Crawling)
            {
                MvState = MovementState.Crawling;
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (MvState != MovementState.Crawling ^ MvState != MovementState.Creeping)
                {
                    MvState = MovementState.Creeping;
                }

                move = Vector3.forward;
            }
        }

        moveSpeed = ((-3 / 2) * (((int)MvState) ^ 2) + (7 / 2) * ((int)MvState));
        Debug.Log("<color=black>" + MvState + "</color>");
        Debug.Log("<color=cyan>" + moveSpeed + "</color>");
        Debug.Log("<color=red>" + move + "</color>");

        rbody.AddForce(move * moveSpeed, ForceMode.Impulse);
        Debug.Log("<color=yellow>"+move * moveSpeed+"</color>");
    }

    private void LateUpdate()
    {
        
    }
}
