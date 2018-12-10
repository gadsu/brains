using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitting : MonoBehaviour
{
    public TomAttackHandler tah;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tah._registerAttack)
        {
            Debug.Log("hitting");
        }
    }
}
