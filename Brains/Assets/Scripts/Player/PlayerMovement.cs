using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Vector3 m_playerRotation;
    [Range(1.0f, 5f)]
    public float m_speedRate = 2.5f;

    public Transform m_cameraTransform;
    private float m_cameraY;

    private void Start()
    {
        m_cameraY = m_cameraTransform.rotation.y;
        m_playerRotation = new Vector3(0, 0, 0)
        {
            y = m_cameraY
        };
        transform.rotation = Quaternion.Euler(0f, m_playerRotation.y, 0f);
    }

    public float SetSpeed(int p_mvState)
    {
        return (float)(m_speedRate * (Math.Sin(p_mvState / (Math.Sqrt(p_mvState) + 1))));
    }

    public Vector3 SetDirection()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    }

    public void RotatePlayer(Transform p_playerTransform, Vector3 p_direction, float p_rate)
    {
        m_cameraY = m_cameraTransform.rotation.eulerAngles.y;
        m_playerRotation = p_playerTransform.rotation.eulerAngles;
        if (p_direction.z >= 0)
            m_playerRotation.y = m_cameraY + (p_direction.x * (90f - (45f * p_direction.z)));
        else
            m_playerRotation.y = m_cameraY + (-p_direction.x * (90f - (45f * -p_direction.z)));

        p_playerTransform.rotation = Quaternion.Euler(m_playerRotation);
    }

    public void Move(float p_moveSpeed, Rigidbody p_rbody, Vector3 p_direction)
    {
        Vector3 l_newCameraDirection = m_cameraTransform.forward;
        l_newCameraDirection.y = 0f;
        p_rbody.velocity = ((l_newCameraDirection * p_direction.z) + (m_cameraTransform.right * p_direction.x)) * p_moveSpeed;
    }
}

/*
 * cameraY = cameraTransform.rotation.y;
 * playerRotation += dir * p_rate;
 * playerRotation.y += cameraY;
 * pt.rotation = Quaternion.Euler(0f, cameraY * playerRotation.x, 0f);
 */
