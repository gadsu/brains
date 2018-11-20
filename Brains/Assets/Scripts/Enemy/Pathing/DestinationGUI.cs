using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pathway))]
public class DestinationGUI : Editor
{
    Pathway ptw;
    private void OnEnable()
    {
        ptw = (Pathway)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        if (ptw._destinations.Length > 0)
        {
            foreach (Destination d in ptw._destinations)
            {
                if (ptw._destinations[0] == d)
                    Handles.Label(d._destinationLocation + Vector3.up, "Start");

                if (ptw._destinations[ptw._destinations.Length - 1] == d)
                    Handles.Label(d._destinationLocation + Vector3.up, "End");
                d._destinationLocation = Handles.PositionHandle(d._destinationLocation, Quaternion.identity);
            }

            for (int i = 0; i < ptw._destinations.Length - 1; i++)
            {
                if(i+1 < ptw._destinations.Length)
                    Handles.DrawLine(ptw._destinations[i]._destinationLocation, ptw._destinations[i + 1]._destinationLocation);
            }
        }
    }
}
