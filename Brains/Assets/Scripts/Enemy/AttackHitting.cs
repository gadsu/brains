using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitting : MonoBehaviour
{
    public EnemyBase tah;
    public ObjectSounds train;
    public bool isTrain;

    private void Awake()
    {
        if (train != null)
        {
            train.InitSounds(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.name == "Spud")
        {
            if(isTrain)
            {
                train.Play("Hit");
                // Camera tracks the *TRAIN* who killed spud.
                GameObject.Find("GameStateController").GetComponent<GameStateHandler>().killer = gameObject;

                train.Play("Victory");
                GameObject.Find("GameStateController").GetComponent<GameStateHandler>().SetState(GameStateHandler.GameState.Lost);
            }

            if (tah != null && tah.registerAttack)
            {
                tah.SoundEvent("Hit");
                if (Random.value > 0.5f)
                {
                    tah.SoundEvent("Victory");
                }
                GameObject.Find("GameStateController").GetComponent<GameStateHandler>().SetState(GameStateHandler.GameState.Lost);
            }
        }
    }
}
