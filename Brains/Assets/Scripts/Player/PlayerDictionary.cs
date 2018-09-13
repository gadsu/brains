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
    AnimatorStateInfo animInfo;
    string id;
    int animationID, playingID;

    private void Start()
    {
        // Add the player animations to the animation dictionary.
        diction = GameObject.Find("Dictionary").GetComponent<AnimationDictionary>();
        anim = GetComponent<Animator>();
        animInfo = new AnimatorStateInfo();
        animationID = 0;
        id = "";
        playingID = -1;

        /****Idle Animatiions****/
        diction.AddToDictionary(0000, "zomb_rig|zomb_idle");
        diction.AddToDictionary(5000, "zomb_rig|zomb_crawl");
        diction.AddToDictionary(10000, "zomb_rig|zomb_creep");
    }

    public int RetrieveKey(int mvState, int arms, int legs, int playDead)
    {
        return ((mvState * 1000) + (arms*100) + (legs*10) + (playDead));
    }

    public void Animate(int p_id, float speed)
    {
        id = diction.ReadFromDictionary(p_id);

        //anim.SetTrigger(animationID);
        animationID = Animator.StringToHash(id);

        if (animationID != playingID)
        {
            if (p_id == 0000 ^ p_id == 0001)
            {
                anim.SetBool("Moving", false);
                Debug.Log("False");
            }
            else
            {
                anim.SetBool("Moving", true);
            }

            anim.SetFloat("Speed", speed);
            anim.Play(animationID);
            playingID = animationID;
        }
    }
}
