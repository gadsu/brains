using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Original Author: Paul J. Manley
 * Modified By:
 */

[CreateAssetMenu(menuName = "AI - Path")]
public class Path : ScriptableObject
{
    public List<PathPoint> pathPoints; // The PathPoints that this scriptableObject contains.
    private int _positionInArray = 0; // The position in the List that is currently active for the runtime representation of the current target point.

    #region Setters & Getters
    public void SetNextPathLocation()
    { // Sets the next path location. Goes to Start by default if it reaches past the number of set points.
        _positionInArray = (_positionInArray <= pathPoints.Capacity) ? _positionInArray + 1 : 0;
    }
    public void SetNextPathLocation(int n)
    { // Lets the developer manually set which location they want to go to next. Allows for different sequencing of path lists with out changing structure of them overall.
        _positionInArray = (n < pathPoints.Capacity) ? n : _positionInArray;
    }
    public Vector3 GetPathToLocation()
    { // Returns the PathPoint location.
        return pathPoints[_positionInArray].location;
    }
    public PathPoint.PointBehavior GetPointBehavior()
    {
        return pathPoints[_positionInArray].beviourAtPoint;
    }
    public PathPoint.PointBehavior GetPointBehavior(int n)
    {
        return pathPoints[n].beviourAtPoint;
    }
    public PathPoint GetPoint(int n)
    {
        return pathPoints[n];
    }
    public PathPoint GetPoint()
    {
        return pathPoints[_positionInArray];
    }
    #endregion
}
