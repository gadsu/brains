using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Vector3 mvDir;
    Vector3 playerRotation;
    [Range(1.0f, 5f)]
    public float speedRate = 2.5f;

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
        return (float)(speedRate * (Math.Sin(mvState / (Math.Sqrt(mvState) + 1))));
    }

    public Vector3 SetDirection()
    {
        mvDir.x = Input.GetAxis("Horizontal");
        mvDir.z = Input.GetAxis("Vertical");
        return mvDir;
    }

    public void RotatePlayer(Transform pt, Vector3 dir, float p_rate)
    {
        /*cameraY = cameraTransform.rotation.y;
        playerRotation += dir * p_rate;
        playerRotation.y += cameraY;
        pt.rotation = Quaternion.Euler(0f, cameraY * playerRotation.x, 0f);
        */

        cameraY = cameraTransform.rotation.eulerAngles.y;
        playerRotation = pt.rotation.eulerAngles;
        playerRotation.y = cameraY + (dir.x * (90f - ((.5f * Math.Abs(dir.z))*90f)));

        pt.rotation = Quaternion.Euler(playerRotation);
    }

    public void Move(float moveSpeed, Rigidbody rbody, Vector3 dir)
    {
        rbody.velocity = ((cameraTransform.forward * dir.z) + (cameraTransform.right * dir.x)) * moveSpeed;
    }
}
