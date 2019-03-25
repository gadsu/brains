using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level1Selection : MonoBehaviour
{

    private void Awake()
    {
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        Debug.Log("Test Play button.");
        SceneManager.LoadScene("Level 1");
    }

}

