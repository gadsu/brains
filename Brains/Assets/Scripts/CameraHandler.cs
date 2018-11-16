using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    Camera[] _cameras;
    private void Awake()
    {
        _cameras = new Camera[2];
        _cameras[0] = GameObject.Find("FirstPersonCamera").GetComponent<Camera>();
        _cameras[1] = GameObject.Find("ThirdPersonCamera").GetComponent<Camera>();

        _cameras[0].enabled = false;
        _cameras[1].enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (_cameras[0].enabled == true)
            {
                _cameras[1].enabled = true;
                _cameras[0].enabled = false;
            }
            else if (_cameras[1].enabled == true)
            {
                _cameras[0].enabled = true;
                _cameras[1].enabled = false;
            }
            else { }
        }
    }
}
