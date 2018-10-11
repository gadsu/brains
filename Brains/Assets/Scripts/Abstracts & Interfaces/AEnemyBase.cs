using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private DetectionLevel detection;
    private AwarenessLevel awareness;

    [Range(-1, 1)]
    public int hearValue;

    [Range(-1, 1)]
    public int sightValue;

    protected DetectionLevel Detection
    {
        get { return detection; }
        set { detection = value; }
    }

    protected AwarenessLevel Awareness
    {
        get { return awareness; }
        set { awareness = value; }
    }
}
