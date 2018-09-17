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

    public int RetrieveKey(int mvState, int arms, int legs, int playDead)
    {
        //Debug.Log("<color=red>" + ((mvState * 1000) + (arms * 100) + (legs * 10) + (playDead)) + "</color>");
        return ((mvState * 1000) + (arms*100) + (legs*10) + (playDead));
    }

    public void Animate(int p_id, float speed, int b)
    {
        id = diction.ReadFromDictionary(p_id);

        if (speed == 0f) speed = 1f;
        animationID = Animator.StringToHash(id);
        anim.SetFloat("Speed",(speed * b));

        if (animationID != playingID)
        {
            anim.Play(animationID);
            playingID = animationID;
        }
    }

    void InitializeAnimationList()
    {
        diction.AddToDictionary(0000, "zomb_rig|zomb_idle");
        diction.AddToDictionary(5000, "zomb_rig|zomb_crawl");
        diction.AddToDictionary(10000, "zomb_rig|zomb_creep");
    }
}
