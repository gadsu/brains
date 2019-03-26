using System;
using System.Collections;
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

    // Use this for initialization
    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("PSC");
        if (objs.Length > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }
	
	// GameStateController hook that plays intro cutscene / sounds if needed.
	public void Restart() {
        if(activeLevel != SceneManager.GetActiveScene().name)
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

    public void Checkpoint(int ind, Vector3 trans)
    {
        if(ind > currentCheckpointIndex)
        {
            Debug.Log(currentCheckpointIndex + " to " + ind);
            currentCheckpointTransform = trans;
            currentCheckpointIndex = ind;
        }
    }
}
