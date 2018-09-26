﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Vector3 mvDir;
    Vector3 playerRotation;
    [Range(1.0f, 5f)]
    public float v = 2.5f;

    public Transform cameraTransform;
    private float cameraY;

    private void Start()
    {
        mvDir = new Vector3(0, 0, 0);
        cameraY = cameraTransform.rotation.y;
        playerRotation = new Vector3(0, 0, 0)
        {
            y = cameraY
        };
        transform.rotation = Quaternion.Euler(0f, playerRotation.y, 0f);
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
        cameraY = cameraTransform.rotation.y;
        playerRotation += dir * p_rate;
        playerRotation.y += cameraY;
        pt.rotation = Quaternion.Euler(0f, cameraY * playerRotation.x, 0f);
    }

    public void Move(float moveSpeed, Rigidbody rbody, Transform pt, int backwards, Vector3 dir)
    {
        //dir = new Vector3(cameraTransform.rotation.y * dir.x, cameraTransform.rotation.z, cameraTransform.rotation.y *dir.z);
        rbody.velocity = (pt.forward * backwards) * moveSpeed;
    }
}
