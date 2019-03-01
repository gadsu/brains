using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    GameStateHandler _gameState;
    public float waitForVictory;

    private void Awake()
    {
        _gameState = GameObject.Find("GameStateController").GetComponent<GameStateHandler>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!_gameState.gameOver)
        {
            Debug.Log("Victory");
            _gameState.SetState(1);
        }
    }
}
