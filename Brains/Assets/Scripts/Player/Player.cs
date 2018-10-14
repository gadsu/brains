using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles the players mechanics and animates them.
 * 
 * Author: Paul Manley
 */

[RequireComponent(typeof(Rigidbody))] // requires a Rigidbody component if one is not found it then generates one.
[RequireComponent(typeof(PlayerMovement))] // requires a PlayerMovement component if one is not found it then generates one.
[RequireComponent(typeof(PlayerDictionary))] // requires a PlayerDictionary component if one is not found it then generates one.
[RequireComponent(typeof(StealthHandler))]
[RequireComponent(typeof(GroanHandler))]
[RequireComponent(typeof(BodyHandler))]
public class Player : ACharacter {

    private float m_moveSpeed; // stores how fast the player should be moving.
    private Vector3 m_moveDir; // stores the players movement direction.
    private Rigidbody m_rbody; // stores the Rigidbody component.

    private PlayerMovement m_scriptPMove; // stores the PlayerMovement script.
    private PlayerDictionary m_scriptPDiction;
    private StealthHandler m_scriptStealthHandler;
    private GroanHandler m_scriptGroanHandler;
    private BodyHandler m_scriptBodyHandler;

    private int m_animationKey;
    private int m_backwards;
    private int m_moving;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start ()
    {
        MvState = MovementState.Idling;
        m_moveSpeed = 0f; 
        m_moveDir = new Vector3(0, 0, 0);
        m_animationKey = 0;
        m_backwards = 0;
        m_moving = 0;

        m_rbody = GetComponent<Rigidbody>();
        m_scriptPMove = GetComponent<PlayerMovement>();
        m_scriptPDiction = GetComponent<PlayerDictionary>();
        m_scriptStealthHandler = GetComponent<StealthHandler>();
        m_scriptGroanHandler = GetComponent<GroanHandler>();
        m_scriptBodyHandler = GetComponent<BodyHandler>();

        m_rbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MvState = MovementState.Idling;
        m_moveDir = Vector3.zero;
        m_moving = 0;
        if (Input.anyKey)
        {
            if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
                m_moveDir = m_scriptPMove.SetDirection();

            if (m_moveDir != Vector3.zero) MvState = MovementState.Creeping;
            if (Input.GetKey(KeyCode.LeftShift)) MvState = MovementState.Crawling;

            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        }

        m_moveSpeed = (m_moveDir != Vector3.zero) ? m_scriptPMove.SetSpeed((int)MvState) : 0f;
        m_scriptPMove.RotatePlayer(transform, m_moveDir, m_moveSpeed);

        if (m_moveSpeed > 0f)
        {
            m_moving = 1;
        }
	}

    private void FixedUpdate()
    {
        m_backwards = (m_moveDir.z < 0f) ? -1 : 1;

        if (m_moveDir * m_moveSpeed != m_rbody.velocity)
        {
            m_scriptPMove.Move(m_moveSpeed, m_rbody, m_moveDir);
            m_scriptStealthHandler.UpdateStealthState(0, m_scriptBodyHandler.GetArms(), m_scriptBodyHandler.GetLegs(), (int)MvState);
            m_scriptGroanHandler.SetGroanSpeed((int)MvState, m_moveSpeed);
        }
    }

    private void LateUpdate()
    {
        m_animationKey = m_scriptPDiction.RetrieveKey(m_moving, (int)MvState, m_scriptBodyHandler.GetArms(), m_scriptBodyHandler.GetLegs(), 0);
        m_scriptPDiction.Animate(m_animationKey, m_moveSpeed, m_backwards);
        if (m_scriptGroanHandler.UpdateGroanAmount())
            m_scriptGroanHandler.Groan();
    }
}
