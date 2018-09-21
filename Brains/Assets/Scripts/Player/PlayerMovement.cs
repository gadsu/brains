using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Vector3 mvDir;
    Vector3 playerRotation;
    [Range(1.0f, 5f)]
    public float v = 2.5f;

    private void Start()
    {
        mvDir = new Vector3(0, 0, 0);
        playerRotation = transform.rotation.eulerAngles;
    }

    public float SetSpeed(int mvState)
    {
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
        playerRotation += dir * p_rate;
        pt.rotation = Quaternion.Euler(0f, playerRotation.x, 0f);
    }

    public void Move(float moveSpeed, Vector3 moveDir, Rigidbody rbody, Transform pt)
    {
        rbody.velocity = (pt.forward * moveDir.z) * moveSpeed;
    }
}
