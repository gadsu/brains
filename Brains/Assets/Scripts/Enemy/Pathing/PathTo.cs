using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Pathway))]
public class PathTo : MonoBehaviour
{
    private Pathway path;

    NavMeshAgent m_agent;
    Vector3 m_target;
    int destinationI;

    // Use this for initialization
    private void Awake()
    {
        path = GetComponent<Pathway>();
        m_agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        destinationI = 0;
        m_agent.SetDestination((path._destinations.Length > 0) ? path._destinations[destinationI]._destinationLocation : transform.position);
    }

    public Vector3 UpdateDestination(bool chasing, Vector3 p_currentDestination, float p_distanceFromPoint)
    {
        Vector3 l_destination = new Vector3();
        if (!chasing)
        {
            if (path._destinations.Length > 0)
            {
                if (p_distanceFromPoint < .1f)
                {
                    destinationI = (destinationI + 1 < path._destinations.Length) ? destinationI + 1 : 0;
                    l_destination = path._destinations[destinationI]._destinationLocation;
                }
                else
                {
                    l_destination = path._destinations[destinationI]._destinationLocation;
                }
            }
        }
        else
        {
            l_destination = GameObject.Find("Spud").GetComponent<Transform>().position;
        }

        return l_destination;
    }
}
