using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DetectPlayer))]
public class DetectPlayerInspector : Editor
{
    DetectPlayer _target;

    private void OnEnable()
    {
        _target = (DetectPlayer)target;

       // SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }


}
