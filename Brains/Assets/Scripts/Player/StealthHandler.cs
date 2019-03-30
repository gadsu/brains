using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthHandler : MonoBehaviour
{
    private float stealth_val;
    public float Stealth_val { get { return stealth_val; } } // lets other scripts read the stealth_val without accidently manipulating it's value.
    private float _hidden;
    //offset
    private void Awake()
    {
        /* Initializes 'simple' data variables. */
        //offset = 0f;
        _hidden = 0f;
        stealth_val = 0f;
        /****************************************/
    }

    public void UpdateHiddenState(bool state)
    {
        _hidden = (state) ? 1f : 0f; // allows for places such as a pile of dead bodies to set the hidden state to true.
    }

    public void UpdateStealthState(int playDead, int mvState)
    {
        stealth_val =
            (1 - _hidden) * ((1 - playDead + .25f) / 2 * Mathf.Abs(mvState - 5));
    }
}
