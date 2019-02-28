using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour {

    public void ReturnMenu()
    {
        Debug.Log("Returning to Menu...");
        SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);
    }
}
