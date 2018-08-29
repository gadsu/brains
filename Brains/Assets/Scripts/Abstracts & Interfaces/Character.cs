using Assets.Scripts.Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ICharacter
{
    MovementState MVState { get; set; }

    void Move(Rigidbody rbody, Vector3 moveDir, MovementState mState);
    void AnimateMovement(Animation anim);
}

namespace Assets.Scripts.Abstracts
{
    public enum MovementState
    {
        Idle,
        Creep,
        Crawl
    }
}