using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public enum DestinationType : int
    {
        Pass = 0,
        Stop = 1,
        Turn = 2,
        Rest = 3
    }

    public DestinationType _destinationType;
    public Vector3 _destinationLocation;
    private Color[] _destinationColors = {Color.green, Color.red, Color.cyan, Color.blue};

    public float _size;

    private void OnDrawGizmosSelected()
    {
        Color _color = _destinationColors[(int)_destinationType];
        Gizmos.color = _color;
        Gizmos.DrawSphere(_destinationLocation, _size);
    }
}
