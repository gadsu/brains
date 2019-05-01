using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loudspeaker : MonoBehaviour {

    public ObjectSounds sounds;
    public float timer;

    // Use this for initialization
    void Start () {
        sounds.InitSounds(gameObject, GetComponent<AudioSource>());
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
		if(timer > 20f)
        {
            timer = 0f;
            sounds.Play(sounds
                    .objectSounds[Mathf.RoundToInt(Random.Range(0, sounds
                    .objectSounds.Capacity - 1))]);
        }
	}
}
