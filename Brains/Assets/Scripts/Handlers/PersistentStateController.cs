using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentStateController : MonoBehaviour {

    private GameStateHandler gstate;
    private int currentCheckpointIndex;
    private Vector3 currentCheckpointTransform;
    private Dictionary<string, int> levelAttempts = new Dictionary<string, int>();
    private string activeLevel;
    public ObjectSounds eventSounds;
    public ObjectSounds musicSounds;
    private List<GameObject> detectedList;
    private bool detectedOnce;
    public FloatVariable maxVolume;

    // Use this for initialization
    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("PSC");
        if (objs.Length > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
        musicSounds.InitSounds(gameObject);
        detectedList = new List<GameObject>();
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            musicSounds.Play("Menu", maxVolume.GetFloat());
        }
    }
	

	// GameStateController hook that plays intro cutscene / sounds if needed.
	public void Restart() {
        musicSounds.SetVolume("Menu", 0f);
        detectedList = new List<GameObject>();
        musicSounds.Play("Gameplay", .25f * maxVolume.GetFloat());
        musicSounds.Play("Cool", 0f);
        musicSounds.Play("Danger", 0f);
        detectedOnce = false;

        if (activeLevel != SceneManager.GetActiveScene().name)
        {
            // Scene was changed
            currentCheckpointIndex = 0;
            currentCheckpointTransform = new Vector3();
        }
        activeLevel = SceneManager.GetActiveScene().name;
        if (!levelAttempts.ContainsKey(activeLevel)) {
            levelAttempts.Add(activeLevel, 0);
        }
        //Debug.Log(levelAttempts[activeLevel]);
        levelAttempts[activeLevel]+= 1;

        gstate = GameObject.Find("GameStateController").GetComponent<GameStateHandler>();

        if (!gstate) { throw new Exception("Could not find game state controller"); }
        eventSounds.InitSounds(gameObject, GetComponent<AudioSource>());

        if (levelAttempts[activeLevel] <= 1)
        {
            eventSounds.Play("Enter");
            // intro stuff
        }
        else
        {
            // checkpoint stuff
            if (currentCheckpointIndex > 0) {
                GameObject.Find("Spud").GetComponent<Player>().SendToPoint(currentCheckpointTransform);
            }
            // if attempts > somenumber, start throwing in difficulty tweaks
        }
    }



    // Set checkpoint to vector IF the index is higher than the previous checkpoint.
    public void Checkpoint(int ind, Vector3 trans)
    {
        if(ind > currentCheckpointIndex)
        {
            //Debug.Log(currentCheckpointIndex + " to " + ind);
            currentCheckpointTransform = trans;
            currentCheckpointIndex = ind;
        }
    }


    // Add enemy to "detected by" list.
    public void AddEnemyToList(GameObject obj)
    {
        if(!detectedList.Contains(obj)) { 
            detectedList.Add(obj);
            detectedList.TrimExcess();
            //Debug.Log(detectedList.Capacity);
        }
        DetectionMusic();
        detectedOnce = true;
    }


    // Remove enemy from "detected by" list.
    public void RemoveEnemyFromList(GameObject obj)
    {
        if (detectedList.Contains(obj))
        {
            detectedList.Remove(obj);
            detectedList.TrimExcess();
        }
        DetectionMusic();
    }


    // GameStateController hook that silences music and optionally switches to chill pause music.
    public void SilenceMusic()
    {
        musicSounds.SetVolume("Gameplay", 0f);
        musicSounds.SetVolume("Danger", 0f);
        musicSounds.SetVolume("Cool", 0f);
    }


    // EnemyBase hook that controls wether the 'danger drums' in the music mix are audible or not.
    public void DetectionMusic()
    {
        if (detectedList.Capacity > 0)
        {
            musicSounds.SetVolume("Danger", 0.75f * maxVolume.GetFloat());
            musicSounds.SetVolume("Cool", 0f);
        }
        else
        {
            musicSounds.SetVolume("Danger", 0f);
            if (detectedOnce)
            {
                musicSounds.SetVolume("Cool", 0.5f * maxVolume.GetFloat());
            }
        }
    }


    public void Duck(bool on)
    {
        if(detectedList.Capacity < 1)
        {
            if (on)
            {
                musicSounds.SetVolume("Gameplay", 0f);
            }
            else
            {
                musicSounds.SetVolume("Gameplay", 0.25f * maxVolume.GetFloat());
            }
        }
    }


    // Cancel music for Tom's Bar areas.
    public void SilentZone(bool enter)
    {
        /*if (enter)
        {
            // soften volume.
            musicSounds.SetVolume("Gameplay", 0f);
            musicSounds.SetVolume("Cool", 0f);
        }
        else
        {
            // return volume.
            musicSounds.SetVolume("Gameplay", 0.25f);
            if (detectedOnce)
            {
                musicSounds.SetVolume("Cool", 0.5f);
            }
        }*/
    }
}
