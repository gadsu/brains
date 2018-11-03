using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{
    internal AEnemyBase.AwarenessLevel InView(Camera thisCamera, int sightValue, AEnemyBase.AwarenessLevel awareness)
    {

        return 0;
    }

    internal AEnemyBase.DetectionLevel UpdateDetection(AEnemyBase.AwarenessLevel awareness, int sightValue, float stealth_val)
    {
        return 0;
    }

    internal bool IsDetecting(Camera m_camera, GameObject m_target)
    {
        return false;
    }

    internal AEnemyBase.AwarenessLevel UpdateAwarenes(AEnemyBase.AwarenessLevel enemy_Awareness, bool mAEnemy_isVisible, float stealth_val)
    {
        throw new NotImplementedException();
    }
}