using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Abstracts;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter {
    private MovementState mvState;
    public MovementState MVState
    {
        get
        {
            return mvState;
        }

        set
        {
            mvState = value;
        }
    }

    public void AnimateMovement(Animation anim, MovementState mState)
    {
        switch (mState)
        {
            case MovementState.Idle:
                break;
            case MovementState.Creep:
                break;
            case MovementState.Crawl:
                break;
        }
    }

    public void Move(Rigidbody rbody, Vector3 moveDir, MovementState mState)
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void FixedUpdate()
    {

    }

    void LateUpdate ()
    {

    }
}
