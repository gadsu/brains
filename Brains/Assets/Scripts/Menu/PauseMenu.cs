using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public enum ButtonFunction { Pause, Resume, LoadScene, LoadScreen };

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
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
        Debug.Log("Loading level1");
        SceneManager.LoadScene("Scenes/"+level, LoadSceneMode.Single);
    }

}
