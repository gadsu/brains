using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    public GameObject winText;
    public enum GameState
    {
        InPlay = 0,
        Won = 1,
        Lost = 2,
        Paused = 3
    };
    public string levelToLoad;

    public GameState currentState;

    [HideInInspector]
    public bool gameOver;

    public float WaitForVictory;

    private void Awake()
    {
        gameOver = false;
        SetState((int)currentState);
    }

    public void SetState(int p_state)
    {
        switch ((GameState)p_state)
        {
            case GameState.InPlay:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.Won:
                gameOver = true;
                winText.SetActive(true);
                Time.timeScale = .5f;
                StartCoroutine("PlayingVictory");
                break;
            case GameState.Lost:
                GameObject.Find("Spud").GetComponent<Player>().playDead = 1;
                gameOver = true;
                Time.timeScale = .5f;
                break;
            default:
                break;
        }
        currentState = ((GameState)p_state != currentState) ? (GameState)p_state : currentState; 
    }

    private IEnumerator PlayingVictory()
    {
        yield return new WaitForSeconds(WaitForVictory);
        GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().LoadLevelLevel(levelToLoad);
    }

    // IEnumerator
}
