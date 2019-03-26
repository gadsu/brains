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
    private bool _chasing = false, _surpised = false, _touched = false, _blockAnimation = false;
    private float _lastTouchedTime = 0f;

    public Vector3? knownLocation;

    // The bone of the enemy's rig that the attack hitbox will be attached to.
    public string boneToAttachHitbox;
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

        _target = GameObject.Find("Spud").GetComponent<Transform>();
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
            attackFX.SetParent(attached.transform);
        }
        else throw new System.Exception("Attack hitbox bone not found.");
    }

    private void Update()
    {
        throttle++;

        if (_touched)
            mDetecting.detectionAmount = 100f;

        if ((mDetecting.IsInView(_target.position) || _touched) && _target.GetComponent<Player>().playDead == 0)
        { 
            mDetecting.UpdateRayToPlayer(_target.position, _target.GetComponent<Player>().playDead);
            if (mDetecting.IsVisible(_target.position) || _touched)
            {
                Enemy_Detection = DetectionLevel.Detecting;
                Enemy_Awareness = AwarenessLevel.Aware;
                if (!_chasing)
                {
                    _chasing = true;
                    if (!_surpised)
                    {
                        _surpised = true;
                        _animationToPlay = animationGenerics["Surprise"];
                        enemySounds.Play("Detect");
                        _blockAnimation = true;
                    }
                }
                else
                {
                    knownLocation = _target.transform.position;
                    if (_agent.remainingDistance < _agent.speed - (1f/2f)*moveSpeedStart)
                    {
                        Debug.Log("<color=red>" + _agent.speed + "</color>");
                        _animationToPlay = animationGenerics["Attack"];
                        _blockAnimation = true;
                    }
                }
            }
            else
            {
                if (Enemy_Awareness == AwarenessLevel.Aware)
                    Enemy_Awareness = AwarenessLevel.Losing;
                else {
                    Enemy_Awareness = AwarenessLevel.Unaware;
                };

                Enemy_Awareness = AwarenessLevel.Unaware;

                _chasing = false;
            }
        }
        else
        {
            _chasing = false;
            Enemy_Awareness = AwarenessLevel.Unaware;

            if (Enemy_Detection == DetectionLevel.Detecting)
                Enemy_Detection = DetectionLevel.Searching;
            else if (Enemy_Detection == DetectionLevel.Searching)
                Enemy_Detection = DetectionLevel.Losing; 
            else
                Enemy_Detection = DetectionLevel.Unseen; 
        }

        _agent.SetDestination(mPathing.UpdateDestination(_chasing, _agent.destination, _agent.remainingDistance));

        if (Enemy_Awareness != AwarenessLevel.Aware && knownLocation != null)
        {
            //Debug.Log("Setting Destination to knownlocation");
            _agent.SetDestination((Vector3)knownLocation);

            if (_agent.remainingDistance < 3f)
            { knownLocation = null; }
        }
        _animHandler.SetAnimation(_animationToPlay, _blockAnimation, _chasing, _agent, moveSpeedStart, _target, Vector3.up);
        _animHandler.SetAnimationSpeed(_agent.velocity.magnitude);
        mDetecting.UpdatingDetectionAmount(mSightValue, mHearValue, _target, (int)Enemy_Detection, (int)Enemy_Awareness);

        if(throttle > 100)
        {
            footSounds.SetVolume((_agent.velocity.magnitude/3)+0.5f);
            throttle = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && Enemy_Awareness == AwarenessLevel.Unaware)
        {
            _lastTouchedTime = Time.time;
            _touched = true;
            enemySounds.Play("Touched");
        }
    }

    private void LateUpdate()
    { 
        _touched = (Mathf.Abs(_lastTouchedTime - Time.time) > 3f) ? false : _touched;

        _animationToPlay = animationGenerics["Walk"];
        _blockAnimation = false;
    }

    // @Paul Moved these from TomAttackHandler.
    public void StartAttackCheck()
    {
        attackFX.GetComponent<ParticleSystem>().Play();
        registerAttack = true;
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
            footSounds.Play(footSounds.objectSounds[Mathf.RoundToInt(Random.Range(0, footSounds.objectSounds.Capacity - 1))]);
    }
}
