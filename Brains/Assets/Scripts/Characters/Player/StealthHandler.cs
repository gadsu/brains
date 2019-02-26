using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthHandler : MonoBehaviour
{
    private float stealth_val;
    public float Stealth_val { get { return stealth_val; } } // lets other scripts read the stealth_val without accidently manipulating it's value.
    private float hidden;
    //offset
    private void Awake()
    {
        /* Initializes 'simple' data variables. */
        //offset = 0f;
        hidden = 0f;
        stealth_val = 0f;
        /****************************************/
    }

    public void UpdateHiddenState(bool state)
    {
        hidden = (state) ? 1f : 0f; // allows for places such as a pile of dead bodies to set the hidden state to true.
    }

    public void UpdateStealthState(int playDead, int mvState)
    {
        //offset = (hidden > 0f && mvState != 5) ? 1f : 0f; // offsets the value of the states if not crawling.

        stealth_val =
            (hidden - 1) * (-(Mathf.Abs(5 - mvState) * (playDead - 1)) + hidden);
        //(hidden - playDead * (arms + legs)) + (((hidden - 1) * Mathf.Abs(5 - mvState) + 5 * playDead) / 2) - offset; // calculates the new stealth rate value.
        //Debug.Log("<color=cyan>" + stealth_val + "</color>");
    }
}
