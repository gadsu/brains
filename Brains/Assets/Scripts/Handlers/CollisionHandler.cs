using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public CollisionEvent[] events;

    private void Awake()
    { // Calls all of the CustomAwake startup events for all of of the CollisionEvent events
        for (int i = 0; i < events.Length; i++)
            events[i].CustomAwake(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    { // Calls all of the CustomEnter collision events for all of of the CollisionEvent events
        for (int i = 0; i < events.Length; i++)
            events[i].CustomEnter(collision);
    }

    private void OnCollisionStay(Collision collision)
    { // Calls all of the CustomStay collision events for all of of the CollisionEvent events
        for (int i = 0; i < events.Length; i++)
            events[i].CustomStay(collision);
    }

    private void OnCollisionExit(Collision collision)
    { // Calls all of the CustomExit collision events for all of of the CollisionEvent events
        for (int i = 0; i < events.Length; i++)
            events[i].CustomExit(collision);
    }
}
