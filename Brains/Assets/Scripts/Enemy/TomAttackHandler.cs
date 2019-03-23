using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomAttackHandler : MonoBehaviour
{
    [HideInInspector]
    public bool registerAttack = false;
    public void StartAttackCheck()
    {
        registerAttack = true;
    }

    public void EndAttackCheck()
    {
        registerAttack = false;
    }
}
