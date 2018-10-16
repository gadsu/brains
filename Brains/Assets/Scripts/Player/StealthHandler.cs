using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthHandler : MonoBehaviour
{
    private float stealth_val; // sets the stealth_val to be unmanipulatable by anything but this script.
    public float Stealth_val { get { return stealth_val; } } // lets other scripts read the stealth_val.
    private float offset, hidden;

    private void Awake()
    {
        /* Initializes 'simple' data variables. */
        offset = 0f;
        hidden = 0f;
        stealth_val = 0f;
        /****************************************/
    }

    private void UpdateHiddenState(bool state)
    {
        hidden = (state) ? 1f : 0f; // allows for places such as a pile of dead bodies to set the hidden state to true.
    }

    public void UpdateStealthState(int playDead, int arms, int legs, int mvState)
    {
        offset = (hidden > 0f && mvState != 5) ? 1f : 0f; // offsets the value of the states if not crawling.

        stealth_val = (hidden - playDead * (arms + legs)) + (((hidden - 1) * Mathf.Abs(5 - mvState) + 5 * playDead) / 2) - offset; // calculates the stealth_val.
    }
}
