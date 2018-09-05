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
    string animationString;
    int animationID;

    private void Start()
    {
        // Add the player animations to the animation dictionary.
        diction = GameObject.Find("Dictionary").GetComponent<AnimationDictionary>();
        anim = GetComponent<Animator>();
        animInfo = new AnimatorStateInfo();

        /****Idle Animatiions****/
        diction.AddToDictionary(0000, "Idle");
    }

    public void Animate(int id, float speed)
    {
        anim.SetFloat("Speed", speed);
        animationString = diction.ReadFromDictionary(id);

        //animInfo = anim.GetCurrentAnimatorStateInfo(0);
        animationID = Animator.StringToHash(animationString);

        anim.SetTrigger(animationID);
    }
}
