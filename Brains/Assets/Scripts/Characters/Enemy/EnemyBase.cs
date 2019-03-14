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

    private Vector3? knownLocation;

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

        knownLocation = null;
    }

    private void Update()
    {
        if (_touched) mDetecting.detectionAmount = 100f;

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
                        _blockAnimation = true;
                    }
                }
                else
                {
                    knownLocation = _target.transform.position;
                    if (_agent.remainingDistance < 5f)
                    {
                        _animationToPlay = animationGenerics["Attack"];
                        _blockAnimation = true;
                    }
                }
            }
            else
            {
                if (Enemy_Awareness == AwarenessLevel.Aware) Enemy_Awareness = AwarenessLevel.Losing;
                else Enemy_Awareness = AwarenessLevel.Unaware;

                Enemy_Awareness = AwarenessLevel.Unaware;

                _chasing = false;
            }
        }
        else
        {
            _chasing = false;
            Enemy_Awareness = AwarenessLevel.Unaware;

            if (Enemy_Detection == DetectionLevel.Detecting) Enemy_Detection = DetectionLevel.Searching;
            else if (Enemy_Detection == DetectionLevel.Searching) Enemy_Detection = DetectionLevel.Losing;
            else Enemy_Detection = DetectionLevel.Unseen;
        }

        _agent.SetDestination(mPathing.UpdateDestination(_chasing, _agent.destination, _agent.remainingDistance));

        if (Enemy_Awareness != AwarenessLevel.Aware && knownLocation != null)
        {
            _agent.SetDestination((Vector3)knownLocation);

            if (_agent.destination == knownLocation)
                if (_agent.remainingDistance < .5f)
                    knownLocation = null;
        }
        _animHandler.SetAnimation(_animationToPlay, _blockAnimation, _chasing, _agent, moveSpeedStart, _target, Vector3.up);
        _animHandler.SetAnimationSpeed(_agent.velocity.magnitude);
        mDetecting.UpdatingDetectionAmount(mSightValue, mHearValue, _target, (int)Enemy_Detection, (int)Enemy_Awareness);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && Enemy_Awareness == AwarenessLevel.Unaware)
        {
            _lastTouchedTime = Time.time;
            _touched = true;
        }
    }

    private void LateUpdate()
    { 
        _touched = (Mathf.Abs(_lastTouchedTime - Time.time) > 3f) ? false : _touched;

        _animationToPlay = animationGenerics["Walk"];
        _blockAnimation = false;
    }
}
