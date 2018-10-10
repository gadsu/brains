using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Animator))]
public class PlayerDictionary : MonoBehaviour
{
    AnimationDictionary diction;
    Animator anim;
    //AnimatorStateInfo animInfo;
    string id;
    int animationID, playingID;

    private void Start()
    {
        // Add the player animations to the animation dictionary.
        diction = GameObject.Find("Dictionary").GetComponent<AnimationDictionary>();
        anim = GetComponent<Animator>();
        //animInfo = new AnimatorStateInfo();
        animationID = 0;
        id = "";
        playingID = -1;

        /****Idle Animatiions****/
        InitializeAnimationList();
    }

    public int RetrieveKey(int moving, int mvState, int arms, int legs, int playDead)
    {
        //Debug.Log("<color=red>" + ((mvState * 1000) + (arms * 100) + (legs * 10) + (playDead)) + "</color>");
        return ((moving * 10000) + (mvState * 1000) + (arms*100) + (legs*10) + (playDead));
    }

    public void Animate(int p_id, float speed, int b)
    {
        id = diction.ReadFromDictionary(p_id);

        if (speed == 0f) speed = 1f;
        animationID = Animator.StringToHash(id);
        anim.SetFloat("Speed", (speed * b));

        if (animationID != playingID)
        {
            anim.Play(animationID);
            playingID = animationID;
        }
    }

    // Initial filenames from the FBX asset are weird, should them in the animator.
    // i.e remove 'zomb_rig|' prefix from everything. 
    void InitializeAnimationList()
    {
        diction.AddToDictionary(00220, "A_SpudIdle");
        diction.AddToDictionary(20220, "A_SpudCreep");

        diction.AddToDictionary(05220, "A_SpudCrawlIdle");
        diction.AddToDictionary(15220, "A_SpudCrawl");
    }
}
