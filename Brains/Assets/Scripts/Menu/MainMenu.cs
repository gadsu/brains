using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Awake()
    {
        Cursor.visible = true;
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("MMusic");
    }

    public void PlayGame ()
    {
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
