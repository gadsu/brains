using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AnimationHandler))]
public class EnemyBase : AEnemy
{
    Transform _target;
    NavMeshAgent _agent;
    AnimationHandler _animHandler;

    string _animationToPlay = "";
    public float moveSpeedStart = 3f;
    bool _chasing = false, _surpised = false, _touched = false, _blockAnimation = false;
    private float _lastTouchedTime = 0f;

    private void Awake()
    {
        mPathing = GetComponent<PathTo>();
        mDetecting = GetComponent<DetectPlayer>();

        _target = GameObject.Find("Spud").GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _animHandler = GetComponent<AnimationHandler>();
    }

    private void Update()
    {
        if (_touched) mDetecting.detectionAmount = 100f;

        if ((mDetecting.IsInView(_target.position) || _touched) && _target.GetComponent<Player>().playDead == 0)
        {
            Enemy_Detection = DetectionLevel.Detecting;

            mDetecting.UpdateRayToPlayer(_target.position, _target.GetComponent<Player>().playDead);
            if (mDetecting.IsVisible(_target.position) || _touched)
            {
                Enemy_Awareness = AwarenessLevel.Aware;
                if (!_chasing)
                {
                    _chasing = true;
                    if (!_surpised)
                    {
                        _surpised = true;
                        _animationToPlay = "A_TomSurprise";
                        _blockAnimation = true;
                    }
                }
                else
                {
                    if (_agent.remainingDistance < 5f)
                    {
                        _animationToPlay = "A_TomAttack";
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

        _animationToPlay = "A_TomWalk";
        _blockAnimation = false;
    }
}
