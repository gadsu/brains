using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
//[RequireComponent(typeof(Pathway))]
public class PathTo : MonoBehaviour
{
    //private Pathway _path;
    public Path path;

    NavMeshAgent _agent;
    Vector3 _target;
    int _destinationI;
    bool _checking;

    // Use this for initialization
    private void Awake()
    {
        //_path = GetComponent<Pathway>();
        _agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        _destinationI = 0;
        //_agent.SetDestination((_path.destinations.Length > 0) ? _path.destinations[_destinationI].location : transform.position);
        //_agent.SetDestination((path.pathPoints.Capacity > 0) ? path.pathPoints[_destinationI].location : transform.position);
        _checking = false;
    }

    public Vector3 UpdateDestination(Vector3 p_currentDestination, float p_distanceFromPoint)
    {
        Vector3 l_destination = transform.position;


        if (!_checking)
        {
            l_destination = path.pathPoints[_destinationI].location;

            if (Vector3.Distance(p_currentDestination, path.pathPoints[_destinationI].location) < 1f && p_distanceFromPoint < .2f)
            {
                Debug.Log("Is close enough");
                switch (path.pathPoints[_destinationI].beviourAtPoint)
                {
                    case PathPoint.PointBehavior.Start:
                        break;
                    case PathPoint.PointBehavior.PassThrough:
                        break;
                    case PathPoint.PointBehavior.End:
                        _checking = true;
                        break;
                    case PathPoint.PointBehavior.Idle:
                            StartCoroutine(Idling(path.pathPoints[_destinationI].idleTime));
                        break;
                }

                _destinationI += 1;

                if(_destinationI >= path.pathPoints.Capacity)
                {
                    _destinationI = 0;
                    if (path.pathPoints[_destinationI].beviourAtPoint == PathPoint.PointBehavior.Start)
                    {
                        _destinationI++;
                    }
                }
            }
        }

        return l_destination;
    }

    private IEnumerator Idling(float pIdleTime)
    {
        _checking = true;
        yield return new WaitForSecondsRealtime(pIdleTime);
        _checking = false;
    }
}
