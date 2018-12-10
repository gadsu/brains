using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomAttackHandler : MonoBehaviour
{
    [HideInInspector]
    public bool _registerAttack = false;
    public void StartAttackCheck()
    {
        _registerAttack = true;
    }

    public void EndAttackCheck()
    {
        _registerAttack = false;
    }
}
