using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RotateForward : MonoBehaviour
{
    Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rotate();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Rotate(start: false);
        }
    }
    public void Rotate(bool start = true)
    {
        if (_collider.GetType() == typeof(CapsuleCollider))
        {
            //_collider.GetType().GetProperty("Center").
        }
    }


}


/*        Vector3 forward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        if (!start)
        {
            transform.Rotate(forward * 90, Space.Self);
        }
        else
        {
            transform.Rotate(-forward * 90f, Space.Self);
        }
*/