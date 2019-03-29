using UnityEngine;

[CreateAssetMenu(menuName = "BrainIconText")]
public class BrainIconObject : TriggerEvent
{
    [TextArea]
    public string words = "";
    public ObjectSounds eventSounds;
    private GameObject thisGameObject;

    public override void CustomAwake(GameObject pThisGameObject)
    {
        base.CustomAwake(pThisGameObject);
        thisGameObject = pThisGameObject;

        eventSounds.InitSounds(thisGameObject);
    }
    public override void CustomEnter(Collider other)
    {
        base.CustomEnter(other);

        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            eventSounds.Play("BrainShow");
            thisGameObject.GetComponent<SpinningAnimation>().speed = -300;
            thisGameObject.GetComponent<TextSetter>().Set(words);
            thisGameObject.GetComponent<TextSetter>().Show(true);
        }
    }

    public override void CustomExit(Collider other)
    {
        base.CustomExit(other);

        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            eventSounds.Play("BrainHide");
            thisGameObject.GetComponent<SpinningAnimation>().speed = 120;
            thisGameObject.GetComponent<TextSetter>().Show(false);
            thisGameObject.GetComponent<TextSetter>().Set("");
        }
    }
}
