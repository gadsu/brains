using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesInRange : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> enemies;

    private void Awake()
    {
        enemies = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name.Substring(0, 3))
        {
            case "Tom":
            case "Mac":
            case "Sea": enemies.Add(other.gameObject);
                enemies.TrimExcess();
                //Debug.Log("<color=cyan>" + other.gameObject.name + "</color>");
                break;
            default: break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.name.Substring(0, 3))
        {
            case "Tom ":
            case "Mac ":
            case "Sean":
                enemies.Remove(other.gameObject);
                enemies.TrimExcess();
                break;
            default: break;
        }
    }
}
