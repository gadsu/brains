using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilenceZone : MonoBehaviour {

    // True = keep silent even if detected. Used in Tom's Bar.
    public bool overrideOthers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("PersistentStateController").GetComponent<PersistentStateController>().SilentZone(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("PersistentStateController").GetComponent<PersistentStateController>().SilentZone(false);
        }
    }
}
