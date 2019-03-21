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
    Rigidbody _rbody;

    GameStateHandler _gameState;
    PlayerMovement _scriptPMove; 
    PlayerDictionary _scriptPDiction;
    StealthHandler _scriptStealthHandler;
    GroanHandler _scriptGroanHandler;
    BodyHandler _scriptBodyHandler;

    int _animationKey;
    int _moving;
    public GameObject limbToLookAt;
    private Vector3 _outOfDeadSnapPosition;
    public CapsuleCollider[] colliders;

    [HideInInspector]
    public int playDead;
    /**************************************/

    Vector3 _spawn; // For sending this game object back to it's spawn when out of bounds.
    public ObjectSounds spudSounds;
    public ObjectSounds footSounds;
	public MovementState MState{get{return MvState;}}

    public bool mustCrawl = false;

    private void Awake()
    {
        /* Initializes references to gameObject components. */
        _gameState = GameObject.Find("GameStateController").GetComponent<GameStateHandler>();
        _rbody = GetComponent<Rigidbody>();
        _scriptPMove = GetComponent<PlayerMovement>();
        _scriptPDiction = GetComponent<PlayerDictionary>();
        _scriptStealthHandler = GetComponent<StealthHandler>();
        _scriptGroanHandler = GetComponent<GroanHandler>();
        _scriptBodyHandler = GetComponent<BodyHandler>();
        /****************************************************/

        spudSounds.InitSounds(gameObject, GetComponent<AudioSource>());
        footSounds.InitSounds(gameObject);
    }

    void Start ()
    {
        /* Initializes 'simple' data types.*/
        MvState = MovementState.Idling;
        _spawn = transform.position;
        _animationKey = 0;
        _moving = 0;
        playDead = 0;
        _rbody.maxDepenetrationVelocity = .1f;
        /***********************************/

        colliders[0].enabled = true;
        colliders[1].enabled = false;

        _rbody.constraints = RigidbodyConstraints.FreezeRotation;// To prevent the gameObject from turning along the x and z rotation axis when moving.
	}

    // Update is called once per frame
    void Update()
    {
        _outOfDeadSnapPosition = limbToLookAt.transform.position;
        if (!_gameState.gameOver)
        {
            if (_gameState.currentState == GameStateHandler.GameState.InPlay)
            {
                if (Input.GetButtonDown("Dead"))
                { // Ignore all previous input and set the state for playing dead.
                    _moving = 0;
                    MvState = MovementState.Idling;
                    playDead = Mathf.Abs(1 - Convert.ToInt32(_scriptPMove.RagDead()));

                    if (playDead == 1)
                    {
                        _rbody.velocity = Vector3.zero;
                        spudSounds.Play("PlayDead");
                    }
                    else if (playDead == 0) { transform.position = _outOfDeadSnapPosition; }

                    _rbody.isKinematic = !_rbody.isKinematic;
                    foreach (CharacterJoint cJ in GetComponentsInChildren<CharacterJoint>())
                    {
                        cJ.enableProjection = !cJ.enableProjection;
                    }
                }

                if (playDead == 0)
                {
                    MvState = MovementState.Idling; // sets the default movement state to idle.

                    _scriptPMove.SetDirection();

                    _moving = (_scriptPMove.playerDirection != Vector3.zero) ? 1 : 0; // is the player moving?

                    /* Overrides the default movement state if the condition is met. */
                    if (_moving == 1) MvState = MovementState.Creeping;
                    if (Input.GetButton("Crawl")) MvState = MovementState.Crawling; // (overrides the moving comparison in order to determine how movement is occuring.)
                    if (mustCrawl) MvState = MovementState.Crawling;
                    /*****************************************************************/
                }

                _scriptPMove.SetSpeed((int)MvState)
                    .RotatePlayer(playDead);

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

                _scriptPDiction.SetAnimationSpeed(((_rbody.velocity.magnitude / 1.4f) + 0.1f) * Mathf.Sign(Input.GetAxis("Vertical"))); // sets the speed and the direction of the animation.
            }
            else
            {
                _scriptPMove.SetSpeed((int)MovementState.Idling)
                    .RotatePlayer(1);

                _scriptPDiction.SetAnimationSpeed(((_rbody.velocity.magnitude / 1.4f) + 0.1f)); // sets the speed and the direction of the animation.
            }
        }
        else
        {
            if (playDead == 1)
            {
                //Debug.Log("Game Over");
                _moving = 0;
                MvState = MovementState.Idling;
                _rbody.velocity = Vector3.zero;
                GetComponent<Animator>().enabled = false;
                //spudSounds.Play("PlayDead");
                _rbody.isKinematic = true;

                foreach (CharacterJoint cJ in GetComponentsInChildren<CharacterJoint>())
                {
                    cJ.enableProjection = !cJ.enableProjection;
                }

                _scriptPMove.SetSpeed((int)MvState)
                    .RotatePlayer(playDead);

                _scriptPDiction.SetAnimationSpeed(((_rbody.velocity.magnitude / 1.4f) + 0.1f) * Mathf.Sign(Input.GetAxis("ForwardTranslate")));
            }
        }
    }
    private void FixedUpdate()
    {
        if (!_gameState.gameOver)
        {
            // Temp out-of-bounds band-aid
            if (transform.position.y < -10 || transform.position.y > 150)
            {
                SendToSpawn();
            }
            if (_gameState.currentState == GameStateHandler.GameState.InPlay)
            {
                _scriptPMove.Move(_rbody);
                _scriptStealthHandler.UpdateStealthState(playDead, (int)MvState);
                _scriptGroanHandler.SetGroanSpeed((int)MvState, _scriptPMove.MoveSpeed);
            }
            else
            {
                _scriptGroanHandler.SetGroanSpeed((int)MovementState.Idling, 0);
                _rbody.velocity = Vector3.zero;
            }
        }
    }

    private void LateUpdate()
    {
        if (!_gameState.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) { spudSounds.Play("CrawlStart"); }
            if (Input.GetKeyUp(KeyCode.LeftShift)) { spudSounds.Play("Uncrawl"); }

            if (_gameState.currentState == GameStateHandler.GameState.InPlay)
            {
                if (_scriptGroanHandler.UpdateGroanAmount()) // if spud has to groan.
                    _scriptGroanHandler.Groan(); // then groan.*/

                _animationKey =
                    _scriptPDiction.RetrieveKey(_moving, (int)MvState, _scriptBodyHandler.GetArms(), _scriptBodyHandler.GetLegs(), playDead); // Gets the key
                _scriptPDiction.Animate(_animationKey, _scriptPMove.MoveSpeed); // inserts the key into the dictionary then animates accordingly.
            }
        }
    }

    public void FootEvent()
    {
        if(footSounds.objectSounds.Count > 0)
            footSounds.Play(footSounds.objectSounds[Mathf.RoundToInt(UnityEngine.Random.Range(0, footSounds.objectSounds.Capacity - 1))]);
    }

    public void SendToSpawn()
    {
        transform.position = _spawn;
    }

    public void SendToPoint(Vector3 point)
    {
        transform.position = point;
    }
}
