using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCrawl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            other.GetComponent<Player>().mustCrawl = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            other.GetComponent<Player>().mustCrawl = false;
        }
    }
}
