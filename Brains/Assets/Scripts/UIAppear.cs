using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAppear : MonoBehaviour
{
    [SerializeField]
    public GameObject customImage;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            customImage.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.SetActive(false);
        }
    }

}
