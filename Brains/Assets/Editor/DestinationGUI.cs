using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
[CustomEditor(typeof(Pathway))] // Tells the Editor to only run this class if an Object has a Pathway Script.
public class DestinationGUI : Editor
{
    Pathway ptw;
    private void OnEnable() // Runs the the first time this script is called by the Editor in succession.
    {
        ptw = (Pathway)target;
    }

    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();// Displays all of the public information as the default format.
    }

    private void OnSceneGUI()
    {
        if (ptw._destinations.Length > 0) // Makes sure the list isn't empty before being called.
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
