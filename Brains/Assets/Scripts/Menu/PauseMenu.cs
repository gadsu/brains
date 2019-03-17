using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused = false;
    private GameStateHandler gstate;
    public enum ButtonFunction { Pause, Resume, LoadScene, LoadScreen };

    private void Awake()
    {
        gstate = GameObject.Find("GameStateController").GetComponent<GameStateHandler>();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            if (gamePaused) Resume();
            else Pause();
        }
	}

    public void Pause()
    {
        gamePaused = true;
        gstate.SetState(3);
    }

    public void Resume()
    {
        gamePaused = false;
        gstate.SetState(0);
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
