using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tSelection : MonoBehaviour
{

    private void Awake()
    {
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        Debug.Log("Test Tutorial button.");
        SceneManager.LoadScene("Tutorial");
    }

}

