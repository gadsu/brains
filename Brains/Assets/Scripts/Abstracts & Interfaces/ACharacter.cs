using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACharacter : MonoBehaviour
{
    public enum MovementState
    {
        Idling,
        Crawling,
        Creeping
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

    protected abstract Vector3 Move();
    protected abstract void Animate();
}