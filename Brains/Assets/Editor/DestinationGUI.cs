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
        if (ptw.destinations.Length > 0) // Makes sure the list isn't empty before being called.
        {
            foreach (Destination d in ptw.destinations)
            {
                if (ptw.destinations[0] == d)
                    Handles.Label(d.location + Vector3.up, "Start");

                if (ptw.destinations[ptw.destinations.Length - 1] == d)
                    Handles.Label(d.location + Vector3.up, "End");
                d.location = Handles.PositionHandle(d.location, Quaternion.identity);
            }

            for (int i = 0; i < ptw.destinations.Length - 1; i++)
            {
                if(i+1 < ptw.destinations.Length)
                    Handles.DrawLine(ptw.destinations[i].location, ptw.destinations[i + 1].location);
            }
        }
    }
}
