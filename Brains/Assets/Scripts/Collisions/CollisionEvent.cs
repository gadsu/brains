using UnityEngine;

[CreateAssetMenu(fileName = "BaseCollisionEvent", menuName = "Base Collision Event")]
public class CollisionEvent : ScriptableObject
{
    public virtual void CustomAwake(GameObject pThisGameObject)
    {
        Debug.Log("<color=blue>Awake</color> CustomAwake: " + name);
    }
    public virtual void CustomEnter(Collision collision)
    {
        Debug.Log("<color=blue>Collision</color> CustomEnter: " + name);
    }

    public virtual void CustomStay(Collision collision)
    {
        Debug.Log("<color=blue>Collision</color> CustomStay: " + name);
    }

    public virtual void CustomExit(Collision collision)
    {
        Debug.Log("<color=blue>Collision</color> CustomExit: " + name);
    }
}
