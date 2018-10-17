using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroanSphereHandler : MonoBehaviour
{
    private GroanHandler groanInfo;

    private void Awake()
    {
        groanInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<GroanHandler>(); // Attaches the players groan script to this sphere.
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && groanInfo.groaning) // if an enemy is in range and the player is groaning then...
            other.GetComponent<TutorialMoveTo>().PathTo(groanInfo.groanLocation); // set the path of the enemy to the groan location.
    }
}
