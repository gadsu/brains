using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public TriggerEvent[] events;

    private void Awake()
    {
        for (int i = 0; i < events.Length; i++)
            events[i].CustomAwake(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    { // Calls all of the CustomEnter trigger events for all of of the TriggerEvent events
        for (int i = 0; i < events.Length; i++)
            events[i].CustomEnter(other);
    }

    private void OnTriggerStay(Collider other)
    { // Calls all of the CustomStay trigger events for all of of the TriggerEvent events
        for (int i = 0; i < events.Length; i++)
            events[i].CustomStay(other);
    }

    private void OnTriggerExit(Collider other)
    { // Calls all of the CustomExit trigger events for all of of the TriggerEvent events
        for (int i = 0; i < events.Length; i++)
            events[i].CustomExit(other);
    }
}
