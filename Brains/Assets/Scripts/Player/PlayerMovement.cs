using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Vector3 mvDir;
    private void Start()
    {
        mvDir = new Vector3(0, 0, 0);
    }

    public float SetSpeed(ACharacter.MovementState mvState)
    {
        float s;

        s = 2 * (Mathf.Abs((float)Math.Sin((-3 * (int)mvState + (int)mvState) + ((7 / 2) * (int)mvState))) * (int)mvState);
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
