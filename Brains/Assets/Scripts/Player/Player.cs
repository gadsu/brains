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
/******************************************************************/

    //TODO: Figure out how to do this:
    // One.Philadelphia();

public class Player : ACharacter
{
    Rigidbody _rbody;

    GameStateHandler _gameState;
    PlayerMovement _scriptPMove; 
    PlayerDictionary _scriptPDiction;
    StealthHandler _scriptStealthHandler;
    GroanHandler _scriptGroanHandler;

    int _animationKey;
    int _moving;
    public GameObject limbToLookAt;
    private Vector3 _outOfDeadSnapPosition;
    public CapsuleCollider[] colliders;
    private float _deadDeltaTime;
    public float minimumPlayDeadTime = 0.5f;

    [HideInInspector]
    public int playDead;

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
        /****************************************************/

        spudSounds.InitSounds(gameObject, GetComponent<AudioSource>());
        footSounds.InitSounds(gameObject);
    }

    void Start ()
    { // Sets all of the default states.
        MvState = MovementState.Idling;
        _spawn = transform.position;
        _animationKey = 0;
        _moving = 0;
        playDead = 0;
        _rbody.maxDepenetrationVelocity = .1f;

        colliders[0].enabled = true;
        colliders[1].enabled = false;

        _rbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

    // Update is called once per frame, at the beginning.
	void Update()
	{
		_moving = 0;
		MvState = MovementState.Idling;
		_outOfDeadSnapPosition = limbToLookAt.transform.position;
        _deadDeltaTime += Time.deltaTime;

        switch (_gameState.currentState)
		{
			case GameStateHandler.GameState.Lost:
				playDead = 1;
				break; // End of Lost case
				
			case GameStateHandler.GameState.Won:
				break; // End of Won case
				
			case GameStateHandler.GameState.InPlay:
				if(Input.GetButtonDown("Dead") && (_deadDeltaTime > minimumPlayDeadTime)) // Put a delay on exiting playdead.
                {
                    _deadDeltaTime = 0;
                    GetComponent<Animator>().enabled = !GetComponent<Animator>().enabled;
					playDead = Convert.ToInt32(!GetComponent<Animator>().enabled);
					
					if(playDead == 0)
					{ // Is set here because it only happens when exiting the playDead state.
                        colliders[0].enabled = true;
                        transform.position = _outOfDeadSnapPosition;
						_rbody.isKinematic = false;
                        
                        foreach (CharacterJoint cJ in GetComponentsInChildren<CharacterJoint>())
						{
							cJ.enableProjection = false;
                            cJ.GetComponent<Rigidbody>().drag = 0f;
						}
					} // End of playDead == 0
                    else spudSounds.Play("PlayDead");
                }
				
				if(playDead == 0)
				{ // Sets InPlay standards when not playing dead.

					_scriptPMove.SetDirection();

					if(_scriptPMove.playerDirection != Vector3.zero)
					{ // Sets standard movement information.
						_moving = 1;
						MvState = MovementState.Creeping;
					}

                    if (Input.GetButtonDown("Crawl"))
                    { // Sets standard crawling information.
                        MvState = MovementState.Crawling;
                        colliders[1].enabled = true;
                        colliders[0].enabled = false;
                        spudSounds.Play("CrawlStart");
                    }
                    else if(Input.GetButton("Crawl") || mustCrawl)
                    { // Keeps crawl state persistent.
                        if (colliders[0].enabled)
                        {
                            colliders[1].enabled = true;
                            colliders[0].enabled = false;
                        }
                        MvState = MovementState.Crawling;
					}else if(Input.GetButtonUp("Crawl"))
					{ // Sets standards for when crawling stops.
                        spudSounds.Play("Uncrawl");
                        colliders[0].enabled = true;
                        colliders[1].enabled = false;
                    }
				} // End of playDead == 0
				
				_scriptPMove.SetSpeed((int)MvState)
					.RotatePlayer(playDead);

				_scriptPDiction.SetAnimationSpeed((_rbody.velocity.magnitude / 1.4f + 0.1f) * Mathf.Sign(Input.GetAxis("Vertical")));
				break; // End of InPlay case
				
			case GameStateHandler.GameState.Paused:
				_scriptPMove.SetSpeed((int)MovementState.Idling)
					.RotatePlayer(1);
				_scriptPDiction.SetAnimationSpeed(((_rbody.velocity.magnitude / 1.4f) + 0.1f)); // sets the speed and the direction of the animation.
				break; // End of Paused case

            default:
                break;
		} // end of switch(_gameState.currentState)

		if (playDead == 1)
		{
            if (colliders[0].enabled || colliders[1].enabled)
            {
                colliders[0].enabled = true;
                colliders[1].enabled = false;
            }
			_rbody.velocity = Vector3.zero;
			GetComponent<Animator>().enabled = false;
			_rbody.isKinematic = true;

			foreach (CharacterJoint cJ in GetComponentsInChildren<CharacterJoint>())
			{
                if (!cJ.enableProjection)
                {
                    cJ.enableProjection = true;
                    cJ.GetComponent<Rigidbody>().drag = 1f;
                }
            }

			_scriptPDiction.SetAnimationSpeed((_rbody.velocity.magnitude / 1.4f + 0.1f) * Mathf.Sign(Input.GetAxis("Vertical")));
		} // End of playDead == 1
	} // End of Update

    // FixedUpdate is called at a regular interval. (Not frame dependent.)
    private void FixedUpdate()
    {
        switch (_gameState.currentState)
        {
            case GameStateHandler.GameState.InPlay:
                if (transform.position.y < -10 || transform.position.y > 150)
                    transform.position = _spawn;

                _scriptPMove.Move(_rbody);
                _scriptStealthHandler.UpdateStealthState(playDead, (int)MvState);
                _scriptGroanHandler.SetGroanSpeed((int)MvState, playDead);
                break; // End of InPlay case
            default:
                _scriptGroanHandler.SetGroanSpeed((int)MovementState.Idling, 1f);
                _rbody.velocity = Vector3.zero;
                break; // End of default case

        } // End of switch(_gameState.currentState)
    } // End of FixedUpdate

    // Late Update is called once per frame, at the end.
    private void LateUpdate()
    {
        switch (_gameState.currentState)
        {
            case GameStateHandler.GameState.InPlay:
                if (_scriptGroanHandler.UpdateGroanAmount()) // if spud has to groan.
                    _scriptGroanHandler.Groan(); // then groan.

                _animationKey =
                    _scriptPDiction.RetrieveKey(_moving, (int)MvState, playDead); 

                _scriptPDiction.Animate(_animationKey, _scriptPMove.MoveSpeed); 
                break; // End of InPlay case
            default:
                break;
        } // End of switch(_gameState.currentState)
    } // End of LateUpdate

    public void FootEvent()
    {
        if(footSounds.objectSounds.Count > 0)
            footSounds.Play(footSounds.objectSounds[Mathf.RoundToInt(UnityEngine.Random.Range(0, footSounds.objectSounds.Capacity - 1))]);
    }

    public void SendToPoint(Vector3 point)
    { // Code for when the player dies after reaching a checkpoint.
        transform.position = point;
    }
}
