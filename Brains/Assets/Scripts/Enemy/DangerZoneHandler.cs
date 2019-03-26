using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneHandler : MonoBehaviour {

    public string cameraMotionBone;
    private Transform cameraMotionRoot;
    //public GameObject dangerZonePivot;
    public GameObject frustumPyramid;
    private DetectPlayer _detect;

    // Use this for initialization
    void Start () {
        cameraMotionRoot = transform.parent.transform.FindChildByRecursion(cameraMotionBone);
        _detect = GetComponentInParent<DetectPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(
            transform.position, 
            cameraMotionRoot.position, 
            Time.deltaTime / 2
            );
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            cameraMotionRoot.rotation,
            Time.deltaTime
            );
    }
    private void FixedUpdate()
    {
        frustumPyramid.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(0.1f,0.11f,0.12f));//26,29,31

        if (_detect.inView)
        {
            frustumPyramid.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(0.23f, 0.18f, 0.11f));//58,47,29

            if (_detect.isVisible)
            {
                frustumPyramid.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(0.34f, 0.14f, 0.18f));//87,36,47
            }
        }
    }
}
