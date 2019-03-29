using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Awake()
    {
        Cursor.visible = true;
    }

    public void PlayGame ()
    {
        Debug.Log("Test Play button.");
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadCredits()
    {
        Debug.Log("Loading Credits...");
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
