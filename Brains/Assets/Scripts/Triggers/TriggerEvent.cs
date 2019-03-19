using UnityEngine;

[CreateAssetMenu(fileName = "BaseTriggerEvent", menuName = "Base Trigger Event")]
public class TriggerEvent : ScriptableObject
{
    public virtual void CustomEnter(Collider other)
    {
        Debug.Log("<color=red>Trigger</color> CustomEnter: " + name);
    }

    public virtual void CustomStay(Collider other)
    {
        Debug.Log("<color=red>Trigger</color> CustomStay: " + name);
    }

    public virtual void CustomExit(Collider other)
    {
        Debug.Log("<color=red>Trigger</color> CustomExit: " + name);
    }
}
