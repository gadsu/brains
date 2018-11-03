using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : AEnemyBase
{
    Camera m_camera;
    GameObject m_target;
    NavMeshAgent m_agent;
    protected virtual void Awake()
    {
        mAEnemy_pathing = GetComponent<PathHandler>();
        mAEnemy_detecting = GetComponent<DetectionHandler>();
        m_camera = GetComponent<Camera>();
        m_agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        Enemy_Awareness = AwarenessLevel.Unaware;
        Enemy_Detection = DetectionLevel.Detecting;
        mAEnemy_isVisible = false;
    }

    protected virtual void Update()
    {
        mAEnemy_isVisible = mAEnemy_detecting.IsDetecting(m_camera, m_target);
        if (mAEnemy_isVisible)
        {
            Enemy_Awareness = mAEnemy_detecting.UpdateAwarenes(Enemy_Awareness, mAEnemy_isVisible, m_target.GetComponent<StealthHandler>().Stealth_val);
            Enemy_Detection = mAEnemy_detecting.UpdateDetection(Enemy_Awareness, mAEnemy_sightValue, m_target.GetComponent<StealthHandler>().Stealth_val);
        }
        else
        {
            if (Enemy_Awareness == AwarenessLevel.Aware)
            {
                Enemy_Awareness = AwarenessLevel.lost;
                Enemy_Detection = DetectionLevel.Searching;
            }
            else if (Enemy_Awareness == AwarenessLevel.lost)
            {
                Enemy_Detection = DetectionLevel.Losing;
            }
        }
    }
}