using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyBase))]
public class EnemyBaseGUI : Editor
{
    private EnemyBase _base;

    private void OnEnable()
    {
        _base = (EnemyBase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Aware benchmark: ");
        _base.SetAwareAmount(EditorGUILayout.FloatField(_base.GetAwareAmount()));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Pursue benchmark: ");
        _base.SetPursueAmount(EditorGUILayout.FloatField(_base.GetPursueAmount()));
        GUILayout.EndHorizontal();
    }
}
