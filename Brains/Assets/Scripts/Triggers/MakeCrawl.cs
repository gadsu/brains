using UnityEngine;

public class MakeCrawl : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            other.GetComponent<Player>().mustCrawl = true;
        } // End of mustCrawl = true
    } // End of OnTriggerEnter

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            other.GetComponent<Player>().mustCrawl = false;
        } // End of mustCrawl = false
    } // End of OnTriggerExit
}
