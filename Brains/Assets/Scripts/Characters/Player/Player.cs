using System;
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

    //TODO: Figure out how to do this:
    // One.Philadelphia();

public class Player : ACharacter
{
    /* Sets up repeatedly used variables. */
    Rigidbody m_rbody;

    GameStateHandler m_gameState;
    PlayerMovement m_scriptPMove; 
    PlayerDictionary m_scriptPDiction;
    StealthHandler m_scriptStealthHandler;
    GroanHandler m_scriptGroanHandler;
    BodyHandler m_scriptBodyHandler;

    int m_animationKey;
    int m_moving;
    public GameObject limbToLookAt;
    private Vector3 outOfDeadSnapPosition;
    public CapsuleCollider[] colliders;

    [HideInInspector]
    public int m_playDead;
    /**************************************/

    Vector3 spawn; // For sending this game object back to it's spawn when out of bounds.
    public ObjectSounds spudSounds;
    public ObjectSounds _footSounds;
	public MovementState mState{get{return MvState;}}

    public bool mustCrawl = false;

    private void Awake()
    {
        /* Initializes references to gameObject components. */
        m_gameState = GameObject.Find("GameStateController").GetComponent<GameStateHandler>();
        m_rbody = GetComponent<Rigidbody>();
        m_scriptPMove = GetComponent<PlayerMovement>();
        m_scriptPDiction = GetComponent<PlayerDictionary>();
        m_scriptStealthHandler = GetComponent<StealthHandler>();
        m_scriptGroanHandler = GetComponent<GroanHandler>();
        m_scriptBodyHandler = GetComponent<BodyHandler>();
        /****************************************************/

        spudSounds.InitSounds(gameObject, GetComponent<AudioSource>());
        _footSounds.InitSounds(gameObject);
    }

    void Start ()
    {
        /* Initializes 'simple' data types.*/
        MvState = MovementState.Idling;
        spawn = transform.position;
        m_animationKey = 0;
        m_moving = 0;
        m_playDead = 0;
        m_rbody.maxDepenetrationVelocity = .1f;
        /***********************************/

        colliders[0].enabled = true;
        colliders[1].enabled = false;

        m_rbody.constraints = RigidbodyConstraints.FreezeRotation;// To prevent the gameObject from turning along the x and z rotation axis when moving.
	}

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameStateController").GetComponent<GameStateHandler>().m_currentState == GameStateHandler.GameState.InPlay)
        {
            outOfDeadSnapPosition = limbToLookAt.transform.position;
            if (!m_gameState.m_gameOver)
            {
                if (Input.GetButtonDown("Dead"))
                { // Ignore all previous input and set the state for playing dead.
                    m_moving = 0;
                    MvState = MovementState.Idling;
                    m_playDead = Mathf.Abs(1 - Convert.ToInt32(m_scriptPMove.RagDead()));

                    if (m_playDead == 1)
                    {
                        m_rbody.velocity = Vector3.zero;
                        spudSounds.Play("PlayDead");
                    }
                    else if (m_playDead == 0) { transform.position = outOfDeadSnapPosition; }

                    m_rbody.isKinematic = !m_rbody.isKinematic;
                    foreach (CharacterJoint cJ in GetComponentsInChildren<CharacterJoint>())
                    {
                        cJ.enableProjection = !cJ.enableProjection;
                    }
                }

                if (m_playDead == 0)
                {
                    MvState = MovementState.Idling; // sets the default movement state to idle.

                    m_scriptPMove.SetDirection();

                    m_moving = (m_scriptPMove.m_playerDirection != Vector3.zero) ? 1 : 0; // is the player moving?

                    /* Overrides the default movement state if the condition is met. */
                    if (m_moving == 1) MvState = MovementState.Creeping;
                    if (Input.GetButton("Crawl")) MvState = MovementState.Crawling; // (overrides the moving comparison in order to determine how movement is occuring.)
                    if (mustCrawl) MvState = MovementState.Crawling;
                    /*****************************************************************/
                }

                m_scriptPMove.SetSpeed((int)MvState)
                    .RotatePlayer(m_playDead);

                if (MvState == MovementState.Crawling)
                {
                    colliders[1].enabled = true;
                    colliders[0].enabled = false;
                }
                else
                {
                    colliders[0].enabled = true;
                    colliders[1].enabled = false;
                }

                m_scriptPDiction.SetAnimationSpeed(((m_rbody.velocity.magnitude / 1.4f) + 0.1f) * Mathf.Sign(Input.GetAxis("ForwardTranslate"))); // sets the speed and the direction of the animation.
            }
            else
            {
                m_scriptPMove.SetSpeed((int)MovementState.Idling)
                    .RotatePlayer(1);

                m_scriptPDiction.SetAnimationSpeed(((m_rbody.velocity.magnitude / 1.4f) + 0.1f)); // sets the speed and the direction of the animation.
            }
        }
    }
    private void FixedUpdate()
    {
        if (GameObject.Find("GameStateController").GetComponent<GameStateHandler>().m_currentState == GameStateHandler.GameState.InPlay)
        {
            // Temp out-of-bounds band-aid
            if (transform.position.y < -10 || transform.position.y > 150)
            {
                SendToSpawn();
            }
            if (!m_gameState.m_gameOver)
            {
                m_scriptPMove.Move(m_rbody);
                m_scriptStealthHandler.UpdateStealthState(m_playDead, (int)MvState);
                m_scriptGroanHandler.SetGroanSpeed((int)MvState, m_scriptPMove.M_MoveSpeed);
            }
            else
            {
                m_scriptGroanHandler.SetGroanSpeed((int)MovementState.Idling, 0);
                m_rbody.velocity = Vector3.zero;
            }
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) { spudSounds.Play("CrawlStart"); }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { spudSounds.Play("Uncrawl"); }

        if (GameObject.Find("GameStateController").GetComponent<GameStateHandler>().m_currentState == GameStateHandler.GameState.InPlay)
        {
            if (!m_gameState.m_gameOver)
            {
                if (m_scriptGroanHandler.UpdateGroanAmount()) // if spud has to groan.
                    m_scriptGroanHandler.Groan(); // then groan.*/

                m_animationKey =
                    m_scriptPDiction.RetrieveKey(m_moving, (int)MvState, m_scriptBodyHandler.GetArms(), m_scriptBodyHandler.GetLegs(), m_playDead); // Gets the key
                m_scriptPDiction.Animate(m_animationKey, m_scriptPMove.M_MoveSpeed); // inserts the key into the dictionary then animates accordingly.
            }
        }
    }

    public void FootEvent()
    {
        _footSounds.Play(_footSounds.objectSounds[Mathf.RoundToInt(UnityEngine.Random.Range(0, _footSounds.objectSounds.Capacity - 1))]);
    }

    public void SendToSpawn()
    {
        transform.position = spawn;
    }
}
