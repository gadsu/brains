using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACharacter : MonoBehaviour
{
    public enum MovementState
    {
        Idling = 0,
        Creeping = 1,
        Crawling = 2
    }

    private MovementState mvState;
    private Animation _anim;
    private bool animStateChange;

    protected MovementState MvState
    {
        get
        {
            return mvState;
        }

        set
        {
            mvState = value;
        }
    }
    protected Animation Anim
    {
        get
        {
            return _anim;
        }

        set
        {
            _anim = value;
        }
    }
    protected bool AnimStateChange
    {
        get
        {
            return animStateChange;
        }

        set
        {
            animStateChange = value;
        }
    }
}