using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level2Selection : MonoBehaviour
{

    private void Awake()
    {
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        Debug.Log("Test Level 2 button.");
        SceneManager.LoadScene("Level 2");
    }

}

