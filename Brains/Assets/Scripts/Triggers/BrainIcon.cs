using UnityEngine;
using UnityEngine.UI;

public class BrainIcon : MonoBehaviour
{
    [TextArea]
    public string words = "";

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.transform.name == "Spud")
        {
            GetComponent<SpinningAnimation>().speed = -300;
            GetComponent<TextSetter>().Set(words);
            GetComponent<TextSetter>().Show(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            GetComponent<SpinningAnimation>().speed = 120;
            GetComponent<TextSetter>().Show(false);
            GetComponent<TextSetter>().Set("");
        }
    }

}
