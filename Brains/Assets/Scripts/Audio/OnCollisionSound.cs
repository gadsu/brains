using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider))]
public class OnCollisionSound : MonoBehaviour
{

    public AudioClip[] clips;
    public Vector3 direction;
    public float magnitude;
    private AudioSource _source;

    // Use this for initialization
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        //chosen = clips[Mathf.RoundToInt(Random.Range(1, clips.Length))];
        Debug.Log("blup");
    }
    private void OnTriggerExit(Collider collider)
    {
        //chosen = clips[Mathf.RoundToInt(Random.Range(1, clips.Length))];
        Debug.Log("blup3");
    }
    private void OnCollisionEnter(Collision collider)
    {
        //chosen = clips[Mathf.RoundToInt(Random.Range(1, clips.Length))];
        Debug.Log("asdf");
    }
    private void OnCollisionExit(Collision collider)
    {
        //chosen = clips[Mathf.RoundToInt(Random.Range(1, clips.Length))];
        Debug.Log("sdfd");
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        // Check for intersection
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, magnitude))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.blue);
            Debug.Log("plop");

        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * magnitude, Color.yellow);
        }
    }
}
