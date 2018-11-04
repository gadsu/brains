using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PathTo : MonoBehaviour
{
    NavMeshAgent m_agent;
    public Vector3[] m_destinations;
    Vector3 m_target;
    int destinationI;

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
                Gizmos.DrawSphere(l_des, .25f);
            }
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
	}

    private void UpdateDestination()
    {
        if (m_destinations.Length > 0)
        {
            if (Mathf.Abs(m_agent.remainingDistance) < .1f)
            {
                //Debug.Log("Index position: " + destinationI + " remaining distance: " + m_agent.remainingDistance);
                destinationI = (destinationI < m_destinations.Length - 1) ? destinationI + 1 : 0;
                //Debug.DrawLine(transform.position, (transform.position + transform.forward), Color.green);
                m_target = m_destinations[destinationI];
                m_agent.SetDestination((m_target));
                //Debug.Log(m_agent.remainingDistance);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateDestination();
	}
}
