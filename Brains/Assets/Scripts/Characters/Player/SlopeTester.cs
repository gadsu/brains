using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeTester : MonoBehaviour
{
    public bool isOnSlope = false;
    private readonly float _slopeLimit = 45f;
    public bool isOnGround = false;

    private void OnCollisionStay(Collision collision)
    {
        /** Get our initial contact point, conveniently this is the bottom one on a capsule collider. */
        ContactPoint contact = collision.contacts[0];

        /** Determine the slope of the surface we are on. */
        Vector3 slope = Vector3.Cross(contact.normal, -transform.right);
        /** Determine the angle difference from our forward vector to the direction of the slope. */
        float angle = Vector3.Angle(slope, transform.forward);

        /** The minimum height for our collision based on the bottom portion of our capsule. */
        float minHeight = GetComponent<CapsuleCollider>().bounds.min.y + GetComponent<CapsuleCollider>().radius;
        foreach (ContactPoint c in collision.contacts)
        {
            /** If our contact points height is less than our minimum height than we are grounded,
            * otherwise it is a surface higher than the ground*/
            if (c.point.y < minHeight)
            {
                isOnGround = true;
            }
            else
                isOnGround = false;
        }
        isOnSlope = (angle > _slopeLimit && isOnGround) ? true : false;
    }

    private void OnCollisionExit(Collision collision)
    {
        isOnSlope = false;
    }
}
