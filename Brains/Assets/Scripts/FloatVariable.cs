using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "new float")]
public class FloatVariable : ScriptableObject
{
    private float _float;

    public float GetFloat()
    {
        return _float;
    }

    public void SetFloat(float pFloat)
    {
        _float = pFloat;
    }
}
