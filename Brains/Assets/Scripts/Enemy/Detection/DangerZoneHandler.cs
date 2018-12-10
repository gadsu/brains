using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneHandler : MonoBehaviour {

    public GameObject cameraMotionRoot;
    public GameObject dangerZonePivot;
    public GameObject frustumPyramid;
    public Color[] colors;
    private DetectPlayer m_detect;

    // Use this for initialization
    void Start () {
        m_detect = GetComponentInParent<DetectPlayer>();
        if (colors.Length < 3) throw new System.Exception("Please choose three colors!");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(
            transform.position, 
            cameraMotionRoot.transform.position, 
            Time.deltaTime / 2
            );
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            cameraMotionRoot.transform.rotation,
            Time.deltaTime
            );
    }
    private void FixedUpdate()
    {
        if (m_detect.m_inView)
        {
            if(m_detect.m_isVisible)
            {
                frustumPyramid.GetComponent<Renderer>().material.SetColor("_TintColor",colors[2]);
            }
            else
            {
                frustumPyramid.GetComponent<Renderer>().material.SetColor("_TintColor", colors[1]);
            }
        }
        else
        {
            frustumPyramid.GetComponent<Renderer>().material.SetColor("_TintColor", colors[0]);
        }
    }
}
