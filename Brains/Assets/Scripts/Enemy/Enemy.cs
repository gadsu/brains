using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Enemy : AEnemyBase
{
    Camera m_camera;
    GameObject m_target;
    protected virtual void Awake()
    {
        mAEnemy_pathing = GetComponent<PathHandler>();
        mAEnemy_detecting = GetComponent<DetectionHandler>();
        m_camera = GetComponent<Camera>();
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
        Enemy_Awareness = mAEnemy_detecting.UpdateAwarenes(Enemy_Awareness, mAEnemy_isVisible, m_target);
        Enemy_Detection = 
    }
}
