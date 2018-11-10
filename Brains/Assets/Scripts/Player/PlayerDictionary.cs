using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerDictionary : MonoBehaviour
{
    AnimationDictionary diction;
    Animator anim;

    string id;
    int animationID, playingID;
    private void Awake()
    {
        /* Initialize references. */
        diction = GameObject.Find("Dictionary").GetComponent<AnimationDictionary>(); // reference to another GameObject component.
        anim = GetComponent<Animator>(); // reference to this gameObjects Animator component.
        /*************************/
    }

    private void Start()
    {
        /* Initializes 'simple' data variables. */
        animationID = 0;
        id = "";
        playingID = -1;
        /****************************************/
    }

    public int RetrieveKey(int moving, int mvState, int arms, int legs, int playDead)
    {
        return ((moving * 10000) + (mvState * 1000) + (arms*100) + (legs*10) + (playDead)); // sends back the formatted key.
    }

    public void SetAnimationSpeed(float p_speed)
    {
        anim.SetFloat("Speed", p_speed);
    }

    public void Animate(int p_id, float speed)
    {
        id = diction.ReadFromDictionary(p_id); // reads back the string stored name of the animation.

        if (speed == 0f) speed = 1f; // makes sure the animation plays

        animationID = Animator.StringToHash(id); // gets the hashed id for the animator referece.

        if (animationID != playingID)
        { // checks to make sure that the animation is not already playing.
            anim.Play(animationID); // plays the animation.
            playingID = animationID; // updates the playing animation reference.
        }
    }
}
