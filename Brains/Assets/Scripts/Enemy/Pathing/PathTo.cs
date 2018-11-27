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
    void Start ()
    {
        destinationI = 0;
        m_target = (path._destinations.Length > 0) ? path._destinations[destinationI]._destinationLocation : transform.position;
        m_agent.SetDestination(m_target);
        IsVisible = false;
	}

    private void UpdateDestination()
    {
        if (path._destinations.Length > 0 && !IsVisible)
        {
            if (m_agent.destination != m_target)
                m_agent.SetDestination(m_target);

            if (Mathf.Abs(m_agent.remainingDistance) < .1f)
            {
                destinationI = (destinationI + 1 < path._destinations.Length - 1) ? destinationI + 1 : 0;
                m_target = path._destinations[destinationI]._destinationLocation;
                m_agent.SetDestination((m_target));
            }
        }
        else
        {
            m_agent.SetDestination(GameObject.Find("Player").transform.position);
        }
    }

    public void SetVisible(bool p_visible)
    {
        IsVisible = p_visible;
    }

	void Update ()
    {
        UpdateDestination();
        // ADDED STUFF HERE PAUL -----------------------------------------------------------------------------------------------
        anim.SetFloat("Velocity", m_agent.velocity.magnitude);
	}
}
