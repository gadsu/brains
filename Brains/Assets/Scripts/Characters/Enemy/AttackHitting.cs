using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitting : MonoBehaviour
{
    public TomAttackHandler tah;
    public TomSoundManager tsm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tah.registerAttack && other.transform.name == "Spud")
        {
            tsm.punchHitEvent();
            Debug.Log("hitting");
            GameObject.Find("GameStateController").GetComponent<GameStateHandler>().SetState(2);
        }
    }
}
