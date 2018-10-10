using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroanSphereHandler : MonoBehaviour
{
    private GroanHandler groanInfo;

    private void Awake()
    {
        groanInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<GroanHandler>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && groanInfo.groaning)
            other.GetComponent<TutorialMoveTo>().PathTo(groanInfo.groanLocation);
    }
}
