using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathTo))]
[RequireComponent(typeof(DetectPlayer))]
public class AEnemy : ACharacter
{
    public enum DetectionLevel
    {
        Unseen = 0,
        Losing = 1,
        Searching = 2,
        Detecting = 3,
        Pursuing = 4
    }

    public enum AwarenessLevel
    {
        Unaware = 0,
        Losing = 1,
        Aware = 2
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

    protected PathTo mAEnemy_pathing;
    protected DetectPlayer mAEnemy_detecting;

    [Range(-1, 1)]
    public int mAEnemy_hearValue;

    [Range(-1, 1)]
    public int mAEnemy_sightValue;
}
