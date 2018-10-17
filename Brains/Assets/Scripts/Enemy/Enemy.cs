using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AEnemyBase
{
    protected virtual void Awake()
    {
        mAEnemy_pathing = GetComponent<PathHandler>();
        mAEnemy_detecting = GetComponent<DetectionHandler>();
    }

    protected virtual void Start()
    {
        Enemy_Awareness = AwarenessLevel.Unaware;
        Enemy_Detection = DetectionLevel.Detecting;
        mAEnemy_isVisible = false;
    }
}
