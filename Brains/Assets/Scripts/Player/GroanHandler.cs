using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroanHandler : MonoBehaviour {
    private float currentAmount, nextAmount, groanSpeed;

    [Range(1f, 5f)]
    public float groanRate;

    private void Awake()
    {
        currentAmount = 0f;
        nextAmount = 0f;
    }

    public void SetGroanSpeed(int mvState, float mvSpeed)
    {
        groanSpeed = groanRate + (mvSpeed * Mathf.Log(mvState + 1));
    }

    public bool UpdateGroanAmount()
    {
        nextAmount = (currentAmount + groanSpeed) * Time.deltaTime;

        if (nextAmount >= 100f)
            return true;

        currentAmount = nextAmount;
        return false;
    }

    public void Groan()
    {
        currentAmount = 0f;
    }
}
