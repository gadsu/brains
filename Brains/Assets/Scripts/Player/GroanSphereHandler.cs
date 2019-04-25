using UnityEngine;

public class GroanSphereHandler : MonoBehaviour
{
    private GroanHandler _groanInfo;
    public float groanAmount;

    private void Awake()
    {
        _groanInfo = gameObject.GetComponentInParent<GroanHandler>(); // Attaches the players groan script to this sphere.
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && other.name != "ZTRAIN" && other.name != "FrustrumPyramid" && other.name != "TRAIN")
        {
            if (!_groanInfo.groaning)
                other.GetComponent<DetectPlayer>().NotHearing();
            else
            {
                other.GetComponent<DetectPlayer>().UpdatingDetectionAmountFromSound(groanAmount);
            }
        }            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && other.name != "ZTRAIN" && other.name != "FrustrumPyramid" && other.name != "TRAIN")
        {
            other.GetComponent<DetectPlayer>().NotHearing();
        }
    }
}
