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
        /* Initializes 'simple' data variables. */
        animationID = 0;
        id = "";
        playingID = -1;
        /****************************************/
    }

    private void Start()
    {
        /* Initialize references. */
        diction = GameObject.Find("Dictionary").GetComponent<AnimationDictionary>(); // reference to another GameObject component.
        anim = GetComponent<Animator>(); // reference to this gameObjects Animator component.
        /*************************/

        InitializeAnimationList();
    }

    public int RetrieveKey(int moving, int mvState, int arms, int legs, int playDead)
    {
        return ((moving * 10000) + (mvState * 1000) + (arms*100) + (legs*10) + (playDead)); // sends back the formatted key.
    }

    public void Animate(int p_id, float speed, int b)
    {
        id = diction.ReadFromDictionary(p_id); // reads back the string stored name of the animation.

        if (speed == 0f) speed = 1f; // makes sure the animation plays

        animationID = Animator.StringToHash(id); // gets the hashed id for the animator referece.
        anim.SetFloat("Speed", (speed * b)); // sets the speed and the direction of the animation.

        if (animationID != playingID)
        { // checks to make sure that the animation is not already playing.
            anim.Play(animationID); // plays the animation.
            playingID = animationID; // updates the playing animation reference.
        }
    }

    // Initial filenames from the FBX asset are weird, should them in the animator.
    // i.e remove 'zomb_rig|' prefix from everything. 
    void InitializeAnimationList()
    {
        /* sends in a reference for the formated key and string pair. */
        /* Creep */
        diction.AddToDictionary(00220, "A_SpudIdle");
        diction.AddToDictionary(20220, "A_SpudCreep");
        /*********/

        /* Crawl */
        diction.AddToDictionary(05220, "A_SpudCrawlIdle");
        diction.AddToDictionary(15220, "A_SpudCrawl");
        /*********/
        /**************************************************************/
    }
}
