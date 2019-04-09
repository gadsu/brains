using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public int index = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            GameObject.Find("PersistentStateController").GetComponent<PersistentStateController>().Checkpoint(index, transform.localPosition);
            Destroy(gameObject);
        }
    }
}
