using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    GameStateHandler m_gameState;
    public float WaitForVictory;

    private void Awake()
    {
        m_gameState = GameObject.Find("GameStateController").GetComponent<GameStateHandler>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (!m_gameState.m_gameOver)
        {
            Debug.Log("Victory");
            m_gameState.SetState(1);
            StartCoroutine("PlayingVictory");
        }
    }

    private IEnumerator PlayingVictory()
    {
        yield return new WaitForSeconds(WaitForVictory);
        Debug.Log("PlayCredits");
        GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().LoadCredits();
    }
}
