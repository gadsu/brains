using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

[RequireComponent(typeof(NavMeshAgent))]
public class PathTo : MonoBehaviour
{
    public Vector3[] m_destinations;

    NavMeshAgent m_agent;
    Vector3 m_target;
    int destinationI;
    bool IsVisible;

    private void OnDrawGizmosSelected()
    {
        if (m_destinations.Length > 0)
        {
            foreach (Vector3 des in m_destinations)
            {
                Gizmos.color = Color.green;
                Vector3 l_des = new Vector3()
                {
                    x = des.x,
                    y = des.y,
                    z = des.z
                };
                if (l_des == m_destinations[0]) Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(l_des, .25f);
            }
            Handles.color = Color.cyan;
            Handles.DrawPolyLine(m_destinations);
            Handles.DrawLine(m_destinations[m_destinations.Length - 1], m_destinations[0]);
        }
    }
    // Use this for initialization
    private void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }
    void Start ()
    {
        destinationI = 0;
        m_target = (m_destinations.Length > 0) ? m_destinations[destinationI] : transform.position;
        m_agent.SetDestination(m_target);
        IsVisible = false;
	}

    private void UpdateDestination()
    {
        if (m_destinations.Length > 0 && !IsVisible)
        {
            if (m_agent.destination != m_target)
                m_agent.SetDestination(m_target);

            if (Mathf.Abs(m_agent.remainingDistance) < .1f)
            {
                destinationI = (destinationI < m_destinations.Length - 1) ? destinationI + 1 : 0;
                m_target = m_destinations[destinationI];
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
	
	// Update is called once per frame
	void Update ()
    {
        UpdateDestination();
	}
}
