using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadOnClick : MonoBehaviour {
    public GameObject loadingImage;
    
	public void LoadScene(string level)
    {
        loadingImage.SetActive(true);
        Application.LoadLevel(level);
    }
}

