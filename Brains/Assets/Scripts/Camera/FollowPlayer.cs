using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float offsetX, offsetY, offsetZ;
    public GameObject target;

    private Vector3 pos;
    private Quaternion rot;

    private void Start()
    {
        pos = transform.position;
        rot = gameObject.transform.rotation;
    }

    private void Update()
    {
        rot = transform.rotation;

        pos = target.transform.position;
        pos.x = pos.x - offsetX;
        pos.y = pos.y - offsetY;
        pos.z = pos.z - offsetZ;



        transform.position = pos;
    }
}
