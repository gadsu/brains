using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(AnimationHandler))]
public class EnemyBase : AEnemy
{
    Transform _target;
    NavMeshAgent _agent;
    AnimationHandler _animHandler;

    public EnemyAnimationKeys animationKeys;
    public Dictionary<string, string> animationGenerics = new Dictionary<string, string>();
    public float moveSpeedStart = 3f;

    private string _animationToPlay = "";
    private bool _surpised = false, _touched = false, _blockAnimation = false, _interrupted;
    private float _lastTouchedTime = 0f;

    public Vector3? knownLocation;
    public float rangeToAttack = 1f;

    // The bone of the enemy's rig that the attack hitbox will be attached to.
    public string boneToAttachHitbox;
    public string boneToAttachFXOverride;
    public Vector3 attackHitboxSize;
    // Spiky, swooshy particle fx for tom's fists of fury.
    private Transform attackFX;

    // All sounds for the enemy.
    public ObjectSounds enemySounds;
    public ObjectSounds footSounds;
    private int throttle;

    [HideInInspector]
    public bool registerAttack = false;

    private void Awake()
    {
        mPathing = GetComponent<PathTo>();
        mDetecting = GetComponent<DetectPlayer>();

        
        _agent = GetComponent<NavMeshAgent>();
        _animHandler = GetComponent<AnimationHandler>();

        for (int i = 0; i < animationKeys.keys.Capacity; i++)
        {
            animationGenerics.Add(animationKeys.keys[i], animationKeys.values[i]);
        }

        enemySounds.InitSounds(gameObject);
        // TODO DOES NOT WORK
        footSounds.InitSounds(gameObject, GetComponent<AudioSource>());

        knownLocation = null;
        GameObject attached = transform.FindChildByRecursion(boneToAttachHitbox).gameObject;
        if (attached)
        {
            attached.AddComponent<BoxCollider>();
            attached.GetComponent<BoxCollider>().size = attackHitboxSize;
            attached.GetComponent<BoxCollider>().isTrigger = true;
            attached.AddComponent<AttackHitting>();
            attached.GetComponent<AttackHitting>().tah = GetComponent<EnemyBase>();
            attackFX = transform.Find("Attack FX");
            // Quick patch for Mac misplaced attack fx.
            if (attackFX)
            {
                if (boneToAttachFXOverride != "")
                {
                    GameObject fxroot = transform.FindChildByRecursion(boneToAttachFXOverride).gameObject;
                    attackFX.SetParent(fxroot.transform);
                }
                else attackFX.SetParent(attached.transform);
            }
        }
        else throw new System.Exception("Attack hitbox bone not found.");
    }
    private void Start()
    {
        _target = GameObject.Find("Spud").GetComponent<Transform>();
        GetComponent<Rigidbody>().drag = 1;
    }

    private void Update()
    {
        if (mDetecting.IsInView(_target.position))
        {
            mDetecting.UpdateRayToPlayer(_target.position, _target.GetComponent<Player>().playDead);

            Enemy_Detection = DetectionLevel.Detecting;

            if (mDetecting.IsVisible(_target.position))
                Enemy_Awareness = AwarenessLevel.Aware;
        } // End of mDetecting.IsInView
        else
        {
            if (Enemy_Detection == DetectionLevel.Pursuing)
                Enemy_Detection = DetectionLevel.Searching;
        } // End of !mDetecting.IsInView

        if (_touched)
        {
            mDetecting.detectionAmount = 100f;
            Enemy_Detection = DetectionLevel.Pursuing;
            Enemy_Awareness = AwarenessLevel.Aware;
        } // End of _touched

        if (mDetecting.detectionAmount > 70f)
        {
            Enemy_Detection = DetectionLevel.Pursuing;
        }

        switch (Enemy_Awareness)
        {
            case AwarenessLevel.Aware:
                if (mDetecting.detectionAmount > 30f && knownLocation == null && mDetecting.detectionAmount < 70f)
                {
                    knownLocation = Vector3.MoveTowards(transform.position, _target.transform.position, 3f);
                } // End of Surprised check

                //knownLocation = _target.transform.position;
                break; // End of Aware case
            case AwarenessLevel.Losing:
                Enemy_Detection = DetectionLevel.Losing;
                break; // End of Losing case
            case AwarenessLevel.Unaware:
                Enemy_Detection = DetectionLevel.Unseen;
                break; // End of Unaware
            default:
                break; // End of default case
        } // End of switch(Enemy_Awareness)


        switch (Enemy_Detection)
        {
            case DetectionLevel.Pursuing:
                GameObject.Find("PersistentStateController")
                    .GetComponent<PersistentStateController>().AddEnemyToList(gameObject);

                transform.LookAt(_target, Vector3.up);
                _interrupted = true;

                if (Mathf.Abs(Vector3.Distance(transform.position, _target.position)) < rangeToAttack)
                {
                    _animationToPlay = animationGenerics["Attack"];
                    _blockAnimation = true;
                    _agent.isStopped = true;
                    _agent.velocity = Vector3.zero;
                } // End of Attack check

                if (!_surpised)
                {
                        _surpised = true;
                        _animationToPlay = animationGenerics["Surprise"];
                        enemySounds.Play("Detect");
                    _blockAnimation = true;
                    _agent.isStopped = true;
                } // End of !_surprised

                if (_animationToPlay != animationGenerics["Attack"] || _animationToPlay != animationGenerics["Surprise"])
                {
                    if (_agent.destination != _target.position)
                    {
                        _agent.SetDestination(_target.position);
                        _agent.destination = _target.position;
                    } // End of if we are not already at the player location.
                } // End of if animation doesn't block movement

                knownLocation = _target.transform.position;
                break; // End of Pursuing case
            case DetectionLevel.Detecting:
                break; // End of Detecting case
            case DetectionLevel.Searching:
                if (_agent.remainingDistance < 1f)
                {
                    knownLocation = null;
                    Enemy_Awareness = AwarenessLevel.Losing;
                } // End of knownLocation check

                break; // End of Searching case
            case DetectionLevel.Losing:
                Enemy_Awareness = AwarenessLevel.Unaware;
                GameObject.Find("PersistentStateController")
                    .GetComponent<PersistentStateController>().RemoveEnemyFromList(gameObject);
                break; // End of Losing case
            case DetectionLevel.Unseen:
                break; // End of Unseen case
            default:
                break; // End of default case
        } // End of switch(Enemy_Detection)

        if(Enemy_Detection != DetectionLevel.Pursuing)
        {
            transform.LookAt(transform, transform.forward);
            if (knownLocation != null)
            {
                if (knownLocation != _agent.destination)
                {
                    _agent.SetDestination((Vector3)knownLocation);
                    _agent.destination = (Vector3)knownLocation;
                }
            } // End of knownLocation != null
            else
            {
                Vector3 tempLocation = mPathing.UpdateDestination(_agent.destination, _agent.remainingDistance);

                if (_interrupted || _agent.destination != tempLocation)
                {
                    _agent.SetDestination(tempLocation);
                    _agent.destination = tempLocation;
                    _interrupted = false;
                } // End of _agent.destination != tempLocation
            } // End of knownLocation == null
        } // End of !DetectionLevel.Pursuing

        throttle++;

        if (throttle > 100)
        {
            footSounds.SetVolume((_agent.velocity.magnitude / 3) + 0.5f);
            throttle = 0;
        } // End of adjust Footstep sound check

    } // End of Update

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _lastTouchedTime = Time.time;
            _touched = true;
            enemySounds.Play("Touched");
        }
    }

    private void LateUpdate()
    {
        /*** Animation information ***/
        _animHandler.
            SetAnimation(_animationToPlay, _blockAnimation, (int)Enemy_Detection, _agent, moveSpeedStart);
        _animHandler.
            SetAnimationSpeed(_agent.velocity.magnitude);
        /*****************************/

        /*** Detection updates ***/
        mDetecting.
            UpdatingDetectionAmount(mSightValue, mHearValue, _target, (int)Enemy_Detection, (int)Enemy_Awareness);

        if (mDetecting.detectionAmount < 1f)
            knownLocation = null;

        _touched = (Mathf.Abs(_lastTouchedTime - Time.time) > 3f) ? false : _touched;
        /*************************/

        /*** Resets ***/
        _animationToPlay = animationGenerics["Walk"];
        _blockAnimation = false;
        /**************/
    }

    public void StartAttackCheck()
    {
        attackFX.GetComponent<ParticleSystem>().Play();
        registerAttack = true;
        // Camera tracks the enemy who killed spud.
        GameObject.Find("GameStateController").GetComponent<GameStateHandler>().killer = gameObject;
    }

    public void EndAttackCheck()
    {
        attackFX.GetComponent<ParticleSystem>().Stop();
        registerAttack = false;
    }

    public void SoundEvent(string name)
    {
        //enemySounds.SetVolume("Detect",0f);
        enemySounds.Play(name);
    }

    public void FootEvent()
    {
        if (footSounds.objectSounds.Count > 0)
            footSounds.Play(footSounds
                .objectSounds[Mathf.RoundToInt(Random.Range(0, footSounds
                .objectSounds.Capacity - 1))]);
    }
}
