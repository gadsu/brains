using UnityEngine;

[CreateAssetMenu(menuName = "Make Crawl")]
public class MakeCrawl : TriggerEvent
{
    public override void CustomEnter(Collider other)
    {
        //base.CustomEnter(other);

        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            other.GetComponent<Player>().mustCrawl = true;
        }
    }

    public override void CustomExit(Collider other)
    {
        //base.CustomExit(other);
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            other.GetComponent<Player>().mustCrawl = false;
        }
    }
}
