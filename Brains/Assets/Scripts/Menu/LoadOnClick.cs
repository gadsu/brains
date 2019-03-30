using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {
    public GameObject loadingImage;
    public GameObject[] scenes;
    
	public void LoadScene(string level)
    {
        switch (level)
        {
            case "Tutorial":
                loadingImage = scenes[0];
                break;
            case "Level 1":
                loadingImage = scenes[1];
                break;
            case "Level 2":
                loadingImage = scenes[2];
                break;
            default:
                break;
        }
        loadingImage.SetActive(true);
        SceneManager.LoadScene(level);
    }
}

