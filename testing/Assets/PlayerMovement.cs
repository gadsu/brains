using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Vector3 mvDir;

    [Range(1.0f, 2.5f)]
    public float v = 2.5f;

    private void Start()
    {
        mvDir = new Vector3(0, 0, 0);
    }

    public float SetSpeed(ACharacter.MovementState mvState)
    {
        float s;

        s = v * (float)Math.Abs(Math.Sin((int)mvState/(Math.Sqrt((int)mvState)+1)));
        Debug.Log((int)mvState);

        Debug.Log("<color=red>Speed: </color>" + s);
        return s;
    }

    public Vector3 SetDirection()
    {
        mvDir.x = Input.GetAxis("Horizontal");
        mvDir.y = 0;
        mvDir.z = Input.GetAxis("Vertical");

        return mvDir;
    }

    public void Move(float moveSpeed, Vector3 moveDir, Rigidbody rbody)
    {
        rbody.velocity = (moveDir * moveSpeed);
    }
}
