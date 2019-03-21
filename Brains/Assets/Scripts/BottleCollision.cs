using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollision : MonoBehaviour
{
    // Holds the reference for the object sounds.
    public ObjectSounds sounds;

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
            // if there is a rigidbody associate with this gameObject.
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().
                    AddForce(GetComponent<Rigidbody>().velocity, ForceMode.Impulse);

                if (sounds.objectSounds.Capacity > 0)
                    sounds.Play(sounds.objectSounds[Random.Range(0, sounds.objectSounds.Capacity - 1)]);

                for (int i = 0; i < GetComponent<EnemiesInRange>().enemies.Capacity; i++)
                {
                    GetComponent<EnemiesInRange>().
                        enemies[i].GetComponent<DetectPlayer>().
                        UpdatingDetectionAmountFromSound((1f / Vector3.
                        Distance(GetComponent<EnemiesInRange>().enemies[i].transform.position,
                        gameObject.transform.position)));
                }
            }
        }
    }
}
