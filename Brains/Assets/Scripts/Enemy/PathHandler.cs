using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathHandler : MonoBehaviour
{
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;
    }
    internal Vector3 UpdatePath(AEnemyBase.AwarenessLevel awareness, AEnemyBase.DetectionLevel detection)
    {
        return Vector3.zero;
    }

    public void PathTo(Vector3 targetPosition)
    {
        agent.destination = targetPosition;
    }
}