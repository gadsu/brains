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

    // Aesthetic stuff
    private TextSetter bigText;
    public ObjectSounds eventSounds;
    public string[] loseLines;
    public string[] winLines;
    public Color winColor;
    public Color loseColor;

    public GameObject gameOverTint;
    public GameObject gameWonTint;
    public GameObject uiPeriphery;
    public GameObject groanMeter;
    public GameObject cameraContainer;
    public GameObject pauseMenuContainer;
    public GameObject pauseMenu;
    private PersistentStateController psc;
    /////////////////////////

    private int lastState = -1337;
    [HideInInspector]
    public GameObject killer;

    private void Awake()
    {
        gameOver = false;
        bigText = GetComponent<TextSetter>();
        SetState(currentState);
        bigText.Show(false);
        eventSounds.InitSounds(gameObject, GetComponent<AudioSource>());
        gameOverTint.SetActive(false);
        gameWonTint.SetActive(false);


        psc = GameObject.Find("PersistentStateController").GetComponent<PersistentStateController>();
        psc.Restart();
    }

    public void SetState(GameState pState)
    {
        // So we don't switch to the same state as before
        if((int)pState != lastState)
        {
            switch (pState)
            {
                case GameState.InPlay:
                    cameraContainer.GetComponent<CameraOperator>().doDisableControls = false;
                    pauseMenuContainer.SetActive(false);
                    bigText.Show(false);
                    uiPeriphery.SetActive(true);
                    groanMeter.SetActive(true);
                    Time.timeScale = 1f;
                    break;



                case GameState.Won:
                    gameOver = true;
                    cameraContainer.GetComponent<CameraOperator>().doDisableControls = true;

                    bigText.Show(true);
                    bigText.Set(winLines[Random.Range(0, winLines.Length)], winColor);
                    gameWonTint.SetActive(true);
                    uiPeriphery.SetActive(false);
                    groanMeter.SetActive(false);
                    psc.SilenceMusic();
                    eventSounds.Play("Victory");

                    Time.timeScale = .5f;
                    StartCoroutine("PlayingVictory");
                    break;



                case GameState.Lost:
                    cameraContainer.GetComponent<CameraOperator>()._doTrackObject = true;
                    cameraContainer.GetComponent<CameraOperator>().lookTarget = killer;
                    cameraContainer.GetComponent<CameraOperator>().doDisableControls = true;
                    GameObject.Find("Spud").GetComponent<Player>().playDead = 1;
                    gameOver = true;

                    bigText.Show(true);
                    bigText.Set(loseLines[Random.Range(0, loseLines.Length)], loseColor);
                    gameOverTint.SetActive(true);
                    uiPeriphery.SetActive(false);
                    groanMeter.SetActive(false);
                    psc.SilenceMusic();
                    eventSounds.Play("Loss");

                    Time.timeScale = .5f;
                    StartCoroutine("PlayingLoss");
                    break;



                case GameState.Paused:
                    cameraContainer.GetComponent<CameraOperator>().doDisableControls = true;
                    pauseMenuContainer.SetActive(true);
                    bigText.Show(false);
                    uiPeriphery.SetActive(false);
                    groanMeter.SetActive(false);
                    Time.timeScale = 0f;
                    break;



                default:
                    break;
            }
            lastState = (int)pState;
        }
        currentState = (pState != currentState) ? pState : currentState; 
    }

    private IEnumerator PlayingVictory()
    {
        yield return new WaitForSeconds(WaitForVictory);
        pauseMenu.GetComponent<PauseMenu>().LoadLevelLevel(levelToLoad);
    }

    private IEnumerator PlayingLoss()
    {
        yield return new WaitForSeconds(WaitForVictory);
        pauseMenu.GetComponent<PauseMenu>().LoadLevelLevel(thisScene);
    }

    // IEnumerator
}
