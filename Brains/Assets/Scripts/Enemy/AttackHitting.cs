using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitting : MonoBehaviour
{
    public EnemyBase tah;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tah.registerAttack && other.transform.name == "Spud")
        {
            tah.SoundEvent("Hit");
            if(Random.value>0.5f)
            {
                tah.SoundEvent("Victory");
            }
            GameObject.Find("GameStateController").GetComponent<GameStateHandler>().SetState(GameStateHandler.GameState.Lost);
        }
    }
}
