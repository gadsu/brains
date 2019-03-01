using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Transform cameraTransform;
    public SlopeTester slope;
    [HideInInspector]
    public Vector3 playerDirection;

    [Range(1.0f, 5f)]
    public float speedRate = 2.5f;

    private float _moveSpeed;
    public float MoveSpeed { get { return _moveSpeed; } } // makes it so that outside scripts can read but not manipulate m_moveSpeed;

    Vector3 _playerRotation;
    float _cameraY;

    private void Awake()
    {
        /* Initializes 'simple' data variablees. */
        playerDirection = new Vector3(0, 0, 0);
        _playerRotation = new Vector3(0, 0, 0);
        _moveSpeed = 0;
        /*****************************************/
    }

    private void Start()
    {
        _cameraY = cameraTransform.rotation.y; // Gets the cameras rotation along the y-axis.
        transform.rotation = Quaternion.Euler(0f, _cameraY, 0f); // Sets the current GameObject's rotation to face the same way as the ccamera.
    }

    public PlayerMovement SetSpeed(int p_mvState)
    {
        _moveSpeed = (float)(speedRate * (Math.Sin(p_mvState / (Math.Sqrt(p_mvState) + 1)))); // calculates the new speed according to the movement state.
        return this;
    }

    public void SetDirection()
    {
        /* Gets the Input axis in order to set direction. */
        playerDirection.x = Input.GetAxis("HorizontalTranslate");
        playerDirection.z = Input.GetAxis("ForwardTranslate");
        /**************************************************/
    }

    public void RotatePlayer(int p_playDead)
    {
		
		if (p_playDead == 0 && (int)GetComponent<Player>().MState != 0)
        {
            /* Obtaining updated rotation values */
            _cameraY = cameraTransform.rotation.eulerAngles.y;
            _playerRotation = transform.rotation.eulerAngles;
            /*************************************/

            /* Checks to see if the player is trying to move forward or backward and then interprets the facing direction */
            if (playerDirection.z >= 0)
                _playerRotation.y = _cameraY + (playerDirection.x * (90f - (45f * playerDirection.z)));
            else
                _playerRotation.y = _cameraY + (-playerDirection.x * (90f - (45f * -playerDirection.z)));
            /**************************************************************************************************************/

            transform.rotation = Quaternion.Euler(_playerRotation); // Applies the new adjusted rotation.
        }
    }

    public void Move(Rigidbody p_rbody)
    {
        Vector3 l_newCameraDirection = cameraTransform.forward; // assigns the camera forward direction.
        l_newCameraDirection.y = 0f; // zeros out the rotations y value to prevent walking into the air.
		
        if (p_rbody.velocity.magnitude < 1f * _moveSpeed)
            p_rbody.AddForce(((l_newCameraDirection * playerDirection.z) +
                (cameraTransform.right * playerDirection.x)) *
                _moveSpeed, ForceMode.Impulse);  // moves the player according to the updated camera and move direction.

        if (slope.isOnSlope && p_rbody.velocity.y < 1f)
            p_rbody.AddForce(Vector3.up, ForceMode.Impulse);
    }

    public bool RagDead()
    {
        GetComponent<Animator>().enabled = !GetComponent<Animator>().enabled;

        return GetComponent<Animator>().enabled;
    }
}