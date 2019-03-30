using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollision : MonoBehaviour
{
    // Holds the reference for the object sounds.
    public ObjectSounds sounds;
    public float dtAmount = 0f;

    private void Awake()
    { 
        // initializes the object sounds.
        sounds.InitSounds(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the player is the collision.
        if (collision.collider.CompareTag("Player") && collision.gameObject.name == "Spud")
        {
            //Debug.Log("Player");
            // if there is a rigidbody associate with this gameObject.
            if (GetComponent<Rigidbody>() != null)
            {
                //Debug.Log("Rigidbody");
                GetComponent<Rigidbody>().
                    AddForce(collision.collider.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);

                //Debug.Log(collision.collider.GetComponent<Rigidbody>().velocity);
                //Debug.Log("<color=red>" + GetComponent<Rigidbody>().velocity + "</color>");

                if (sounds.objectSounds.Capacity > 0)
                    sounds.Play(sounds.objectSounds[Random.Range(0, sounds.objectSounds.Capacity - 1)]);

                for (int i = 0; i < GetComponent<EnemiesInRange>().enemies.Capacity; i++)
                {
                    GetComponent<EnemiesInRange>().
                        enemies[i].GetComponent<DetectPlayer>().
                        UpdatingDetectionAmountFromSound(20f / Vector3.
                        Distance(GetComponent<EnemiesInRange>().enemies[i].transform.position,
                        gameObject.transform.position) + dtAmount);
                }
            }
        }
    }
}
