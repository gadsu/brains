using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACharacter : MonoBehaviour
{
    public enum MovementState
    {
        Idling = 0,
        Crawling = 5,
        Creeping = 10
    }

    private MovementState mvState;

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
}