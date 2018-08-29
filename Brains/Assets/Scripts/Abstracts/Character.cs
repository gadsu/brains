using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character {
    public enum MovementState
    {
        Idle,
        Walking,
        Running,
        Crawling
    }

    private MovementState mvState;

    public MovementState MvState
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
}
