using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Vector3 mvDir;

    [Range(1.0f, 5f)]
    public float v = 2.5f;

    private void Start()
    {
        mvDir = new Vector3(0, 0, 0);
    }

    public float SetSpeed(int mvState)
    {
        //Debug.Log((float)(v * (Math.Sin(mvState / (Math.Sqrt(mvState) + 1)))));
        return (float)(v * (Math.Sin(mvState / (Math.Sqrt(mvState) + 1))));
    }

    public Vector3 SetDirection()
    {
        mvDir.x = Input.GetAxis("Horizontal");
        mvDir.z = Input.GetAxis("Vertical");
        return mvDir;
    }

    public void RotatePlayer(Transform pt, Vector3 dir, float p_rate)
    {
        pt.forward += dir*p_rate;
    }

    public void Move(float moveSpeed, Vector3 moveDir, Rigidbody rbody, Transform pt)
    {
        rbody.velocity = pt.forward * moveSpeed;
    }
}
