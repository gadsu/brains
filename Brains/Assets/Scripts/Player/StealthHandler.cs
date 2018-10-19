using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthHandler : MonoBehaviour
{
    private float stealth_val;
    public float Stealth_val { get { return stealth_val; } } // lets other scripts read the stealth_val without accidently manipulating it's value.
    private int offset, hidden;
    public bool Hidden{ get { return Convert.ToBoolean(hidden); } }

    private void Awake()
    {
        /* Initializes 'simple' data variables. */
        offset = 0;
        hidden = 0;
        stealth_val = 0f;
        /****************************************/
    }

    public void UpdateHiddenState(bool state)
    {
        hidden = Convert.ToInt32(state); // allows for places such as a pile of dead bodies to set the hidden state to true.
    }

    public void UpdateStealthState(int playDead, int arms, int legs, int mvState)
    {
        offset = (hidden > 0f && mvState != 5) ? 1 : 0; // offsets the value of the states if not crawling.

        stealth_val = 
            (float)((hidden - playDead * (arms + legs)) + (((hidden - 1) * Mathf.Abs(5 - mvState) + 5 * (playDead + hidden) / 2) - offset)); // calculates the new stealth rate value.
    }
}
