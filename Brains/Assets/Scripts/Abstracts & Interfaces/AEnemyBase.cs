using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathHandler))]
[RequireComponent(typeof(DetectionHandler))]
public abstract class AEnemyBase : ACharacter
{
    public enum DetectionLevel
    {
        Detecting = 0,
        Losing = 1,
        Searching = 2,
        Pursuing = 3
    }

    public enum AwarenessLevel
    {
        Aware = 0,
        Unaware = 1,
        lost = 2
    }

    private DetectionLevel m_detection;
    private AwarenessLevel m_awareness;

    protected DetectionLevel Enemy_Detection
    {
        get { return m_detection; }
        set { m_detection = value; }
    }

    protected AwarenessLevel Enemy_Awareness
    {
        get { return m_awareness; }
        set { m_awareness = value; }
    }

    protected PathHandler mAEnemy_pathing;
    protected DetectionHandler mAEnemy_detecting;
    protected bool mAEnemy_isVisible;

    [Range(-1, 1)]
    public int mAEnemy_hearValue;

    [Range(-1, 1)]
    public int mAEnemy_sightValue;
}
