using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyAnimationKeys))]
public class EnemyAnimationKeysGUI : Editor
{
    EnemyAnimationKeys _target;
    string _keyInput, _valueInput;
    bool _foldout = false;

    private void OnEnable()
    {
        _target = (EnemyAnimationKeys)target;
        EditorUtility.SetDirty(_target);

        if (_target.keys == null)
        {
            _target.keys = new List<string>();
        }
        if (_target.values == null)
        {
            _target.values = new List<string>();
        }
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        _keyInput = GUILayout.TextField(_keyInput);
        _valueInput = GUILayout.TextField(_valueInput);
        GUILayout.EndHorizontal();

        GUILayout.Label("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Add Key"))
        {
            if (_keyInput != "")
            {
                if (_valueInput != "")
                {
                    bool exists = false;
                    if (_target.keys.Capacity > 0)
                    {
                        foreach (string s in _target.keys)
                        {
                            if (_keyInput == s)
                                exists = true;
                        }
                    }

                    if (!exists)
                    {
                        _target.keys.Add(_keyInput);
                        _target.values.Add(_valueInput);
                        _keyInput = "Generic key";
                        _valueInput = "Actual value";
                        _target.keys.TrimExcess();
                        _target.values.TrimExcess();
                    }
                }
            }
        }
        if (GUILayout.Button("Remove Key"))
        {
            if (_keyInput != "")
            {
                bool exists = false;
                if (_target.keys.Capacity > 0)
                {
                    foreach (string s in _target.keys)
                    {
                        if (_keyInput == s)
                            exists = true;
                    }

                    if (exists)
                    {
                        _target.values.RemoveAt(_target.keys.IndexOf(_keyInput));
                        _target.keys.Remove(_keyInput);
                        _target.keys.TrimExcess();
                        _target.values.TrimExcess();
                    }
                }
            }
        }

        GUILayout.Label("", GUI.skin.horizontalSlider);


        _foldout = EditorGUILayout.Foldout(_foldout, "Generic Animation Dictionary", true);
        if (_foldout)
        {
            for (int i = 0; i < _target.keys.Capacity; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(_target.keys[i] + ": ", GUILayout.Width(Screen.width / 3));
                GUILayout.Label(_target.values[i]);
                GUILayout.EndHorizontal();
            }
        }
    }

    private void OnDisable()
    {
        _target = null;
    }
}
