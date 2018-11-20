using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathway : MonoBehaviour
{
    public Destination[] _destinations;

    private void OnDrawGizmosSelected()
    {
        if (_destinations.Length > 0)
        {
            foreach (Destination d in _destinations)
            {
                Gizmos.color = d._destinationColor;
                Gizmos.DrawSphere(d._destinationLocation, .25f);
            }

        }
    }
}
