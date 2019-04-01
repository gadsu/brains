using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RailRoadNavigation : MonoBehaviour
{
    public Path directPath;
    private int indexAlongPath;
    private NavMeshAgent _agent;
    private void Awake()
    {
        indexAlongPath = 0;
        _agent = GetComponent<NavMeshAgent>();
        if (directPath.pathPoints.Capacity > 0)
        {
            _agent.SetDestination(directPath.pathPoints[indexAlongPath].location);
            _agent.destination = directPath.pathPoints[indexAlongPath].location;
        }
        else
            Debug.Log("Missing Path on: " + name);
    }

    private void Update()
    {
        if (_agent.destination != directPath.pathPoints[indexAlongPath].location && _agent.remainingDistance < _agent.stoppingDistance + .1f)
        {
            _agent.SetDestination(directPath.pathPoints[indexAlongPath].location);
            _agent.destination = directPath.pathPoints[indexAlongPath].location;

            indexAlongPath++;
            if (indexAlongPath >= directPath.pathPoints.Capacity) indexAlongPath = 0;
        }
    }
}
