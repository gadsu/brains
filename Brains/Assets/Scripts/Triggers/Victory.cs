using UnityEngine;

public class Victory : MonoBehaviour
{
    GameStateHandler _gameState;
    public float waitForVictory;

    private void Awake()
    {
        _gameState = GameObject.Find("GameStateController").GetComponent<GameStateHandler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _gameState.SetState(GameStateHandler.GameState.Won);
    }
}
