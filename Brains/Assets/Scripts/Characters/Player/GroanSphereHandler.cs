using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroanSphereHandler : MonoBehaviour
{
    private GroanHandler groanInfo;

    private void Awake()
    {
        groanInfo = gameObject.GetComponentInParent<GroanHandler>(); // Attaches the players groan script to this sphere.
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!groanInfo.groaning)
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
