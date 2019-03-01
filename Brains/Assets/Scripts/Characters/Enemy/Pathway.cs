using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathway : MonoBehaviour
{
    public Destination[] destinations;

    private void OnDrawGizmosSelected()
    {
        if (destinations.Length > 0)
        {
            foreach (Destination d in destinations)
            {
                Gizmos.color = d.color;
                Gizmos.DrawSphere(d.location, .25f);
            }
        }
    }
}
