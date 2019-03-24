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

    public DestinationType type;
    public Vector3 location;
    private Color[] _typeColor = {Color.yellow, Color.green, Color.blue};
    public Color color { get { return _typeColor[(int)type]; } }

    public float idleTime;
}
