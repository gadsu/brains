using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthHandler : MonoBehaviour
{
    private float stealth_val;

    public float Stealth_val { get { return stealth_val; } }

    private float offset, hidden;

    private void Awake()
    {
        offset = 0f;
        hidden = 0f;
        stealth_val = 0f;
    }

    private void UpdateHiddenState(bool state)
    {
        hidden = (state) ? 1f : 0f;
    }

    public void UpdateStealthState(int playDead, int arms, int legs, int mvState)
    {
        offset = (hidden > 0f && mvState != 5) ? 1f : 0f;

        stealth_val = (hidden - playDead * (arms + legs)) + (((hidden - 1) * Mathf.Abs(5 - mvState) + 5 * playDead) / 2) - offset;
    }
}
