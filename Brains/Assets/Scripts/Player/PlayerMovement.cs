using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Transform m_cameraTransform;
    [HideInInspector]
    public Vector3 m_playerDirection;

    [Range(1.0f, 5f)]
    public float m_speedRate = 2.5f;

    private float m_moveSpeed;
    public float M_MoveSpeed { get { return m_moveSpeed; } } // makes it so that outside scripts can read but not manipulate m_moveSpeed;

    Vector3 m_playerRotation;
    float m_cameraY;

    private void Awake()
    {
        /* Initializes 'simple' data variablees. */
        m_playerDirection = new Vector3(0, 0, 0);
        m_playerRotation = new Vector3(0, 0, 0);
        m_moveSpeed = 0;
        /*****************************************/
    }

    private void Start()
    {
        m_cameraY = m_cameraTransform.rotation.y; // Gets the cameras rotation along the y-axis.

        transform.rotation = Quaternion.Euler(0f, m_cameraY, 0f); // Sets the current GameObject's rotation to face the same way as the ccamera.
        
    }

    public PlayerMovement SetSpeed(int p_mvState)
    {
        m_moveSpeed = (float)(m_speedRate * (Math.Sin(p_mvState / (Math.Sqrt(p_mvState) + 1)))); // calculates the new speed according to the movement state.
        return this;
    }

    public void SetDirection()
    {
        /* Gets the Input axis in order to set direction. */
        m_playerDirection.x = Input.GetAxis("Horizontal");
        m_playerDirection.z = Input.GetAxis("Vertical");
        /**************************************************/
    }

    public void RotatePlayer()
    {
        /* Obtaining updated rotation values */
        m_cameraY = m_cameraTransform.rotation.eulerAngles.y;
        m_playerRotation = transform.rotation.eulerAngles;
        /*************************************/

        /* Checks to see if the player is trying to move forward or backward and then interprets the facing direction */
        if (m_playerDirection.z >= 0)
            m_playerRotation.y = m_cameraY + (m_playerDirection.x * (90f - (45f * m_playerDirection.z)));
        else
            m_playerRotation.y = m_cameraY + (-m_playerDirection.x * (90f - (45f * -m_playerDirection.z)));
        /**************************************************************************************************************/

        transform.rotation = Quaternion.Euler(m_playerRotation); // Applies the new adjusted rotation.
    }

    public void Move(Rigidbody p_rbody)
    {
        Vector3 l_newCameraDirection = m_cameraTransform.forward; // assigns the camera forward direction.
        l_newCameraDirection.y = 0f; // zeros out the rotations y value to prevent walking into the air.

        p_rbody.velocity = 
            ((l_newCameraDirection * m_playerDirection.z) + (m_cameraTransform.right * m_playerDirection.x)) * m_moveSpeed; // moves the player according to the updated camera and move direction.
    }
}