using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Pathway))]
public class PathTo : MonoBehaviour
{
    private Pathway path;

    NavMeshAgent m_agent;
    Animator anim;
    Vector3 m_target;
    int destinationI;
    bool IsVisible;

    // Use this for initialization
    private void Awake()
    {
        path = GetComponent<Pathway>();
        m_agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        destinationI = 0;
        m_target = (path._destinations.Length > 0) ? path._destinations[destinationI]._destinationLocation : transform.position;
        m_agent.SetDestination(m_target);
        IsVisible = false;
    }

    public Vector3 UpdateDestination(bool chasing, Vector3 p_currentDestination, float p_distanceFromPoint)
    {
        Vector3 l_destination = new Vector3();
        if (!chasing)
        {
            if (path._destinations.Length > 0)
            {
                if (Mathf.Abs(p_distanceFromPoint) < .1f)
                {
                    destinationI = (destinationI + 1 < path._destinations.Length) ? destinationI + 1 : 0;
                    l_destination = path._destinations[destinationI]._destinationLocation;
                }
            }
        }
        else
        {
            l_destination = GameObject.Find("Spud").transform.position;
        }

        return l_destination;
    }
}
