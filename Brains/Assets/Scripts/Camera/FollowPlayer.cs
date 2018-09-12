using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetPos;
    private Vector3 pos;
    private Quaternion rot;

    private Vector3 dist;
    /*private float distX;
    private float distY;
    private float distZ;*/
    private void Start()
    {
        targetPos = target.GetComponent<Transform>().position;
        pos = transform.position;
        dist = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        pos = transform.position;

        dist.x = targetPos.x - pos.x;
        dist.y = targetPos.y - pos.y;
        dist.z = targetPos.z - pos.z;

        targetPos = target.GetComponent<Transform>().position;

        pos = targetPos;
        pos -= dist;
        /*pos.x -= distX;
        pos.y -= distY;
        pos.z -= distZ;*/

        transform.position = pos;

        transform.RotateAround(targetPos, new Vector3(0f, Input.GetAxis("Mouse X"), 0f), 50*Time.deltaTime);
        transform.Rotate(new Vector3(-((Input.GetAxis("Mouse Y")*50)*Time.deltaTime), 0f, 0f));
    }
}
