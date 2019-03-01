﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeHidden : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            if(other.GetComponent<Player>().playDead == 1)
                other.GetComponent<StealthHandler>().UpdateHiddenState(true);
            else
                other.GetComponent<StealthHandler>().UpdateHiddenState(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            other.GetComponent<StealthHandler>().UpdateHiddenState(false);
        }
    }
}
