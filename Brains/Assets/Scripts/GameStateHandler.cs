using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    public enum GameState
    {
        InPlay = 0,
        Won = 1,
        Lost = 2,
        Paused = 3
    };
    public string levelToLoad, thisScene;

    public GameState currentState;

    [HideInInspector]
    public bool gameOver;
    public float WaitForVictory;

    // @paul sorry if these don't belong here, feel free to relocate them. :)
    private TextSetter bigText;
    public string[] loseLines;
    public string[] winLines;
    public Color winColor;
    public Color loseColor;

    private int lastState = -1337;

    private void Awake()
    {
        gameOver = false;
        bigText = GetComponent<TextSetter>();
        SetState((int)currentState);
        bigText.Show(false);
    }

    public void SetState(int p_state)
    {
        // So we don't switch to the same state as before
        if(p_state != lastState)
        {
            switch ((GameState)p_state)
            {
                case GameState.InPlay:
                    bigText.Show(false);
                    Time.timeScale = 1f;
                    break;
                case GameState.Paused:
                    bigText.Show(false);
                    Time.timeScale = 0f;
                    break;
                case GameState.Won:
                    gameOver = true;
                    bigText.Show(true);
                    bigText.Set(winLines[Random.Range(0, winLines.Length)], winColor);
                    Time.timeScale = .5f;
                    StartCoroutine("PlayingVictory");
                    break;
                case GameState.Lost:
                    GameObject.Find("Spud").GetComponent<Player>().playDead = 1;
                    gameOver = true;
                    bigText.Show(true);
                    bigText.Set(loseLines[Random.Range(0, loseLines.Length)], loseColor);
                    Time.timeScale = .5f;
                    StartCoroutine("PlayingLoss");
                    break;
                default:
                    break;
            }
            lastState = p_state;
        }
        currentState = ((GameState)p_state != currentState) ? (GameState)p_state : currentState; 
    }

    private IEnumerator PlayingVictory()
    {
        yield return new WaitForSeconds(WaitForVictory);
        GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().LoadLevelLevel(levelToLoad);
    }

    private IEnumerator PlayingLoss()
    {
        yield return new WaitForSeconds(WaitForVictory);
        GameObject.Find("PauseCanvas").GetComponent<PauseMenu>().LoadLevelLevel(thisScene);
    }

    // IEnumerator
}
