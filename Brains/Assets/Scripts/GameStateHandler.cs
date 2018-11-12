using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    private enum GameState
    {
        InPlay = 0,
        Won = 1,
        Lost = 2,
        Paused = 3
    };
    private GameState m_currentState;

    [HideInInspector]
    public bool m_gameOver;

    private void Awake()
    {
        m_currentState = 0;
        m_gameOver = false;
    }

    public void SetState(int p_state)
    {
        switch ((GameState)p_state)
        {
            case GameState.InPlay:
                break;
            case GameState.Paused:
                break;
            case GameState.Won:
                m_gameOver = true;
                break;
            case GameState.Lost:
                m_gameOver = true;
                break;
            default:
                break;
        }
        m_currentState = ((GameState)p_state != m_currentState) ? (GameState)p_state : m_currentState; 
    }
}
