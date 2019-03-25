using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Original Author: Paul J. Manley
 * Modified By:
 */

[System.Serializable]
public class PathPoint
{
    #region ManipulatableObjectData
    public enum PointBehavior : byte
    {// the expected behavior of the AI once it has reached the point location.
        Start = 0,
        Idle = 1,
        PassThrough = 2,
        Interact = 3,
        End = 4
    }
    public PointBehavior beviourAtPoint; // this specific type of point behaviour.
    public Vector3 location; // the location that this point is at.
    public float idleTime; // how long something stays still
    #endregion

    #region Constructors
    public PathPoint()
    { // Default Constructor
        beviourAtPoint = PointBehavior.Start;
        location = Vector3.zero;
    }
    public PathPoint(Vector3 pLocation)
    { // Passed Location Constructor
        beviourAtPoint = PointBehavior.Start;
        location = pLocation;
    }
    public PathPoint(Vector3 pLocation, PointBehavior pbehavior)
    { // Full regular cosntructor.
        location = pLocation;
        beviourAtPoint = pbehavior;
    }
    public PathPoint(PathPoint pOriginal)
    { // Copy Constructor.
        location = pOriginal.location;
        beviourAtPoint = pOriginal.beviourAtPoint;
    }
    #endregion
}
