using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    public enum ButtonFunction { Pause, Resume, LoadScene, LoadScreen };

    /*[System.Serializable]
    public class MenuOption
    {
        //public string buttonText = "";
        public ButtonFunction buttonFunction;
        public GameObject buttonLink;
        //public string buttonArg = "";
    }
    public MenuOption[] buttonsList;

    private void Awake()
    {
        for(int i = 0; i < buttonsList.Length-1; i++)
        {
            switch(buttonsList[i].buttonFunction)
            { case ButtonFunction.Pause: buttonsList[i].buttonLink.GetComponent<Button>().addListener(Pause); break;
                default: break;
            }
        }
    }*/

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            
            if (GamePaused)
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
        GamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
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
