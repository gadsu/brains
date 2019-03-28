using UnityEngine;

[CreateAssetMenu(menuName = "Make Hidden")]
public class MakeHidden : TriggerEvent
{
    public override void CustomEnter(Collider other)
    {
        //base.CustomEnter(other);
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            if (other.GetComponent<Player>().playDead == 1)
                other.GetComponent<StealthHandler>().UpdateHiddenState(true);
            else
                other.GetComponent<StealthHandler>().UpdateHiddenState(false);
        }
    }

    public override void CustomStay(Collider other)
    {
        //base.CustomStay(other);

        if (other.GetComponent<Player>().playDead == 1)
            other.GetComponent<StealthHandler>().UpdateHiddenState(true);
        else
            other.GetComponent<StealthHandler>().UpdateHiddenState(false);
    }

    public override void CustomExit(Collider other)
    {
        //base.CustomExit(other);
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            other.GetComponent<StealthHandler>().UpdateHiddenState(false);
        }
    }
}
