using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AEnemyBase
{
    public Camera thisCamera;
    private Vector3 thisLocation, targetPosition;

    private Pathing path;
    private DetectionHandler dtHandler;

    private void Awake()
    {
        Awareness = AwarenessLevel.Unaware;
        Detection = DetectionLevel.Detecting;
        thisLocation = transform.position;

        path = GetComponent<Pathing>();
        dtHandler = GetComponent<DetectionHandler>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        thisLocation = transform.position;

        Awareness = dtHandler.InView(thisCamera, sightValue, Awareness);
        Detection = dtHandler.UpdateDetection(Awareness, sightValue);

        targetPosition = path.UpdatePath(Awareness, Detection);
        path.PathTo(targetPosition);
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        
    }
}
