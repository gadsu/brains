using UnityEngine;
using UnityEngine.UI;

public class BrainIcon : MonoBehaviour
{
    [TextArea]
    public string words = "";
    public ObjectSounds eventSounds;

    private void Awake()
    {
        eventSounds.InitSounds(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.transform.name == "Spud")
        {
            eventSounds.Play("BrainShow");
            GetComponent<SpinningAnimation>().speed = -300;
            GetComponent<TextSetter>().Set(words);
            GetComponent<TextSetter>().Show(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            eventSounds.Play("BrainHide");
            GetComponent<SpinningAnimation>().speed = 120;
            GetComponent<TextSetter>().Show(false);
            GetComponent<TextSetter>().Set("");
        }
    }

}
