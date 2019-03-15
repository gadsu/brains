using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Pathway))]
public class PathTo : MonoBehaviour
{
    private Pathway _path;

    NavMeshAgent _agent;
    Vector3 _target;
    int _destinationI;
    bool _isIdle, _checking;

    // Use this for initialization
    private void Awake()
    {
        _path = GetComponent<Pathway>();
        _agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        _destinationI = 0;
        _agent.SetDestination((_path.destinations.Length > 0) ? _path.destinations[_destinationI].location : transform.position);
        _isIdle = false;
        _checking = false;
    }

    public Vector3 UpdateDestination(bool chasing, Vector3 p_currentDestination, float p_distanceFromPoint)
    {
        Vector3? l_destination = null;
        if (_path.destinations.Length <= 0)
            return _path.destinations[_destinationI].location;

        if (!chasing)
        {
            if (p_distanceFromPoint < .1f)
            {
                if (!_checking)
                {
                    switch (_path.destinations[_destinationI].type)
                    {
                        case Destination.DestinationType.Pass:
                            _checking = false;
                            _isIdle = false;
                            break;
                        case Destination.DestinationType.Stop:
                            _checking = true;
                            _isIdle = false;
                            break;
                        case Destination.DestinationType.Idle:
                            _isIdle = !_isIdle;
                            if (_isIdle)
                            {
                                _checking = true;
                                StartCoroutine(Idling(_path.destinations[_destinationI].idleTime));
                            }
                            break;
                        default:
                            break;
                    }
                }

                if (!_checking)
                {
                    _destinationI = (_destinationI + 1 < _path.destinations.Length) ? _destinationI + 1 : 0;
                    l_destination = _path.destinations[_destinationI].location;
                }
            }
        }
        else
        {
            l_destination = GameObject.Find("Spud").GetComponent<Transform>().position;
        }

        if (l_destination == null)
            l_destination = _path.destinations[_destinationI].location;

        return (Vector3)l_destination;
    }

    private IEnumerator Idling(float pIdleTime)
    {
        yield return new WaitForSeconds(pIdleTime);
        _checking = false;
    }
}
