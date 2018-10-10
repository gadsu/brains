﻿// Modified Unity documentation examples
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class TutorialMoveTo : MonoBehaviour
{
    public Transform goal;
    public Camera targetCamera;
    // Flag for clicking to set destination
    public int mousePositionSetter;
    public bool doDebugAgentTesterTracking;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;
    }
  
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0) && mousePositionSetter == 1)
        {
            RaycastHit hit;

            if (Physics.Raycast(targetCamera.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }*/
        if(transform.position == agent.destination)
        {
            doDebugAgentTesterTracking = true;
        }
    }
    public void PathTo(Vector3 position) 
    {
        if (doDebugAgentTesterTracking == true)
        {
            agent.destination = position;
            doDebugAgentTesterTracking = false;
        }
    }
}
