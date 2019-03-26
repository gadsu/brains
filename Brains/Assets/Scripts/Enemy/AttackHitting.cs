using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitting : MonoBehaviour
{
    public TomAttackHandler tah;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tah.registerAttack && other.transform.name == "Spud")
        {
            GameObject.Find("GameStateController").GetComponent<GameStateHandler>().SetState(GameStateHandler.GameState.Lost);
        }
    }
}
