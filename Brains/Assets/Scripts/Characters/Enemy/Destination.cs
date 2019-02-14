using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Destination : System.Object
{
    public enum DestinationType : int
    {
        Idle = 0,
        Pass = 1,
        Stop = 2
    }

    public DestinationType _destinationType;
    public Vector3 _destinationLocation;
    private Color[] _typeColor = {Color.yellow, Color.green, Color.blue};
    public Color _destinationColor { get { return _typeColor[(int)_destinationType]; } }

    public float idleTime;
}
