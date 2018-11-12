using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject target;
    public int m_rate;
    private Vector3 targetPos, pos, dist;
    private Quaternion rot;

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

        transform.position = pos;

        transform.RotateAround(targetPos, new Vector3(0f, Input.GetAxis("Mouse X"), 0f), m_rate*Time.deltaTime);
        transform.Rotate(new Vector3(-(Input.GetAxis("Mouse Y")), 0f, 0f) * (m_rate * .5f) * Time.deltaTime);
    }
}
