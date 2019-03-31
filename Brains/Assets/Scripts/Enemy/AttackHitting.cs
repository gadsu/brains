using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitting : MonoBehaviour
{
    public EnemyBase tah;
    public bool isTrain;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && (tah.registerAttack || isTrain) && other.transform.name == "Spud")
        {
            if(isTrain)
            {
                // Camera tracks the *TRAIN* who killed spud.
                GameObject.Find("GameStateController").GetComponent<GameStateHandler>().killer = gameObject;
            }
            tah.SoundEvent("Hit");
            if(Random.value>0.5f)
            {
                tah.SoundEvent("Victory");
            }
            GameObject.Find("GameStateController").GetComponent<GameStateHandler>().SetState(GameStateHandler.GameState.Lost);
        }
    }
}
