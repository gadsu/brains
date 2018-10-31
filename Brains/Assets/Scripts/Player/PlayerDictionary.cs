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

        /* PlayDead */
        diction.AddToDictionary(00221, "A_SpudPlayDead"); // not moving, MvState = 0, full body, playing dead
        diction.AddToDictionary(00211, "A_SpudPlayDead"); // -1 leg
        diction.AddToDictionary(00201, "A_SpudPlayDead"); // 0 legs
        diction.AddToDictionary(00121, "A_SpudPlayDead"); // -1 arm
        diction.AddToDictionary(00111, "A_SpudPlayDead"); // -1 arm, -1 leg
        diction.AddToDictionary(00101, "A_SpudPlayDead"); // -1 arm, 0 legs
        /************/

        /* Idle */
        diction.AddToDictionary(00220, "A_SpudIdle"); // not moving, MvState = 0, full body, not playing dead
        diction.AddToDictionary(00210, "A_SpudIdle"); // -1 leg
        diction.AddToDictionary(00120, "A_SpudIdle"); // -1 arm
        diction.AddToDictionary(00110, "A_SpudIdle"); // -1 arm, -1 leg
        /********/

        /* Creep */
        diction.AddToDictionary(20220, "A_SpudCreep"); // moving, MvState = 10, full body, not playing dead
        diction.AddToDictionary(20210, "A_SpudCreep"); // -1 leg
        diction.AddToDictionary(20120, "A_SpudCreep"); // -1 arm
        diction.AddToDictionary(20110, "A_SpudCreep"); // -1 arm, -1 leg
        /*********/

        /* Crawl Idle */
        diction.AddToDictionary(05220, "A_SpudCrawlIdle"); // not moving, MvState = 5, full body, not playing dead
        diction.AddToDictionary(05210, "A_SpudCrawlIdle"); // -1 leg
        diction.AddToDictionary(05200, "A_SpudCrawlIdle"); // 0 legs
        diction.AddToDictionary(05120, "A_SpudCrawlIdle"); // -1 arm
        diction.AddToDictionary(05110, "A_SpudCrawlIdle"); // -1 arm, -1 leg
        diction.AddToDictionary(05100, "A_SpudCrawlIdle"); // -1 arm, 0 legs
        /**************/

        /* Crawl */
        diction.AddToDictionary(15220, "A_SpudCrawl"); // moving, MvState = 5, full body, not playing dead
        diction.AddToDictionary(15210, "A_SpudCrawl"); // -1 leg
        diction.AddToDictionary(15200, "A_SpudCrawl"); // 0 legs
        diction.AddToDictionary(15120, "A_SpudCrawl"); // -1 arm
        diction.AddToDictionary(15110, "A_SpudCrawl"); // -1 arm, -1 leg
        diction.AddToDictionary(15100, "A_SpudCrawl"); // -1 arm, 0 legs
        /*********/
        /**************************************************************/
    }
}
