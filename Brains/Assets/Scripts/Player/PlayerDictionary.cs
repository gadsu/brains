using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerDictionary : MonoBehaviour
{
    AnimationDictionary _diction;
    Animator _anim;

    string _id;
    int _animationID, _playingID;
    private void Awake()
    {
        /* Initialize references. */
        _diction = GameObject.Find("Dictionary").GetComponent<AnimationDictionary>(); // reference to another GameObject component.
        _anim = GetComponent<Animator>(); // reference to this gameObjects Animator component.
        /*************************/
    }

    private void Start()
    {
        /* Initializes 'simple' data variables. */
        _animationID = 0;
        _id = "";
        _playingID = -1;
        /****************************************/
    }

    public int RetrieveKey(int pMoving, int pMvState, int pPlayDead)
    {
        return ((pMoving * 100) + (pMvState * 10) + (pPlayDead)); // sends back the formatted key.
    }

    public void SetAnimationSpeed(float p_speed)
    {
        _anim.SetFloat("Speed", p_speed);
    }

    public void Animate(int pId, float pSpeed)
    {
        _id = _diction.ReadFromDictionary(pId); // reads back the string stored name of the animation.

        if (pSpeed == 0f) pSpeed = 1f; // makes sure the animation plays

        _animationID = Animator.StringToHash(_id); // gets the hashed id for the animator referece.

        if (_animationID != _playingID && _id != "A_SpudPlayDead")
        { // checks to make sure that the animation is not already playing.
            _anim.Play(_animationID); // plays the animation.
            _playingID = _animationID; // updates the playing animation reference.
        }
    }
}
