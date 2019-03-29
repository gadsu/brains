using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameStateHandler gstate;
    public enum ButtonFunction { Pause, Resume, LoadScene, LoadScreen };

    private void Awake()
    {
        gstate = GameObject.Find("GameStateController").GetComponent<GameStateHandler>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gstate.currentState == GameStateHandler.GameState.Paused)
            {
                Cursor.visible = false;
                Resume();
            }
            else
            {
                Cursor.visible = true;
                Pause();
            }
        }
	}

    public void Pause()
    {
        gstate.SetState(GameStateHandler.GameState.Paused);
    }

    public void Resume()
    {
        gstate.SetState(GameStateHandler.GameState.InPlay);
    }

    public void LoadCredits()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading Credits...");
        SceneManager.LoadScene("Scenes/Credits", LoadSceneMode.Single);
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Returning to Menu...");
        SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);
    }

    public void LoadLevelLevel(string level)
    {
        Time.timeScale = 1f;
        Debug.Log("Loading level");
        SceneManager.LoadScene("Scenes/"+level, LoadSceneMode.Single);
    }

}
