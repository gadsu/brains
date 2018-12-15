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

    public GameState m_currentState;

    [HideInInspector]
    public bool m_gameOver;

    public float WaitForVictory;

    private void Awake()
    {
        m_gameOver = false;
        SetState((int)m_currentState);
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
                m_gameOver = true;
                winText.SetActive(true);
                Time.timeScale = .5f;
                StartCoroutine("PlayingVictory");
                break;
            case GameState.Lost:
                GameObject.Find("Spud").GetComponent<Player>().m_playDead = 1;
                m_gameOver = true;
                Time.timeScale = .5f;
                break;
            default:
                break;
        }
        m_currentState = ((GameState)p_state != m_currentState) ? (GameState)p_state : m_currentState; 
    }

    private IEnumerator PlayingVictory()
    {
        yield return new WaitForSeconds(WaitForVictory);
        GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().LoadCredits();
    }

    // IEnumerator
}
