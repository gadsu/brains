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

    public void AnimateMovement(Animation anim)
    {
        throw new System.NotImplementedException();
    }

    public void Move(Rigidbody rbody, Vector3 moveDir, MovementState mState)
    {
        throw new System.NotImplementedException();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
