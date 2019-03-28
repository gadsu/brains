using UnityEngine;

public class MakeHidden : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            if (other.GetComponent<Player>().playDead == 1)
                other.GetComponent<StealthHandler>().UpdateHiddenState(true);
            else
                other.GetComponent<StealthHandler>().UpdateHiddenState(false);
        } // End of UpdateHiddenState true ^ false
    } // End of OnTriggerEnter

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            if (other.GetComponent<Player>().playDead == 1)
                other.GetComponent<StealthHandler>().UpdateHiddenState(true);
            else
                other.GetComponent<StealthHandler>().UpdateHiddenState(false);
        } // End of UpdateHiddenState true ^ false
    } // End of OnTriggerStay

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            other.GetComponent<StealthHandler>().UpdateHiddenState(false);
        } // UpdateHiddenState false
    } // End of OnTriggerExit
}
