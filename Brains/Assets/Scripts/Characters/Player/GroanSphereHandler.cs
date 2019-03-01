using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroanSphereHandler : MonoBehaviour
{
    private GroanHandler _groanInfo;

    private void Awake()
    {
        _groanInfo = gameObject.GetComponentInParent<GroanHandler>(); // Attaches the players groan script to this sphere.
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!_groanInfo.groaning)
                other.GetComponent<DetectPlayer>().NotHearing();
            else
            {
                other.GetComponent<DetectPlayer>().UpdatingDetectionAmountFromSound(15f);
            }

        }            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<DetectPlayer>().NotHearing();
        }
    }
}
