using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles the players mechanics and animates them.
 * 
 * Author: Paul Manley
 */

/* Ensures that the desired componenets exist on this gameObject. */
[RequireComponent(typeof(Rigidbody))] 
[RequireComponent(typeof(PlayerMovement))] 
[RequireComponent(typeof(PlayerDictionary))] 
[RequireComponent(typeof(StealthHandler))]
[RequireComponent(typeof(GroanHandler))]
[RequireComponent(typeof(BodyHandler))]
/******************************************************************/

public class Player : ACharacter
{
    /* Sets up repeatedly used variables. */
    Rigidbody m_rbody; 

    PlayerMovement m_scriptPMove; 
    PlayerDictionary m_scriptPDiction;
    StealthHandler m_scriptStealthHandler;
    GroanHandler m_scriptGroanHandler;
    BodyHandler m_scriptBodyHandler;

    int m_animationKey;
    int m_backwards;
    int m_moving;
    /**************************************/

    Vector3 spawn; // For sending this game object back to it's spawn when out of bounds.

    private void Awake()
    {
        /* Initializes 'simple' data types.*/
        MvState = MovementState.Idling;
        spawn = transform.position;
        m_animationKey = 0;
        m_backwards = 0;
        m_moving = 0;
        /***********************************/
    }

    void Start ()
    {
        /* Initializes references to gameObject components. */
        m_rbody = GetComponent<Rigidbody>();
        m_scriptPMove = GetComponent<PlayerMovement>();
        m_scriptPDiction = GetComponent<PlayerDictionary>();
        m_scriptStealthHandler = GetComponent<StealthHandler>();
        m_scriptGroanHandler = GetComponent<GroanHandler>();
        m_scriptBodyHandler = GetComponent<BodyHandler>();
        /****************************************************/

        m_rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ; // To prevent the gameObject from turning along the x and z rotation axis when moving.
	}
	
	// Update is called once per frame
	void Update ()
    {
        MvState = MovementState.Idling; // sets the default movement state to idle.

        m_scriptPMove.SetDirection();

        m_moving = (m_scriptPMove.m_playerDirection != Vector3.zero) ? 1 : 0; // is the player moving?
        m_backwards = (m_scriptPMove.m_playerDirection.z < 0f) ? -1 : 1; // is the player moving backwards?

        /* Overrides the default movement state if the condition is met. */
        if (m_moving == 1) MvState = MovementState.Creeping;
        if (Input.GetKey(KeyCode.LeftShift)) MvState = MovementState.Crawling; // (overrides the moving comparison in order to determine how movement is occuring.)
        /*****************************************************************/

        m_scriptPMove.SetSpeed((int)MvState)
            .RotatePlayer();

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit(); // Quits the application (does not work in editor.)
    }

    private void FixedUpdate()
    {
        if (m_scriptPMove.m_playerDirection * m_scriptPMove.M_MoveSpeed != m_rbody.velocity)
        { // if the player is not moving at the same speed and direction then update critical information sets.
            m_scriptPMove.Move(m_rbody);
            m_scriptStealthHandler.UpdateStealthState(0, m_scriptBodyHandler.GetArms(), m_scriptBodyHandler.GetLegs(), (int)MvState);
            m_scriptGroanHandler.SetGroanSpeed((int)MvState, m_scriptPMove.M_MoveSpeed);
        }
    }

    private void LateUpdate()
    {
        if (m_scriptGroanHandler.UpdateGroanAmount()) // if spud has to groan.
            m_scriptGroanHandler.Groan(); // then groan.

        m_animationKey = 
            m_scriptPDiction.RetrieveKey(m_moving, (int)MvState, m_scriptBodyHandler.GetArms(), m_scriptBodyHandler.GetLegs(), 0); // Gets the key
        m_scriptPDiction.Animate(m_animationKey, m_scriptPMove.M_MoveSpeed, m_backwards); // inserts the key into the dictionary then animates accordingly.
    }

    public void SendToSpawn()
    {
        transform.position = spawn;
    }
}
