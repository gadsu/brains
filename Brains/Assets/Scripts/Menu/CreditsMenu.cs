using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour {

    public void ReturnMenu()
    {
        Debug.Log("Returning to Menu...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
