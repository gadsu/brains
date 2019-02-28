using UnityEngine;

public class UIAppear : MonoBehaviour
{
    [SerializeField]
    public GameObject customImage;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.transform.name == "Spud")
        {
            customImage.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            customImage.SetActive(false);
        }
    }

}
