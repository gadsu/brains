using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DetectPlayer : MonoBehaviour
{
    Camera m_camera;
    Transform m_target;
    Vector3 targetPosisition;

    Ray m_ray;
    RaycastHit m_out;

    bool inView;
    private void Awake()
    {
        m_camera = GetComponent<Camera>();
        m_target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        m_camera.enabled = false;
        inView = false;
        m_ray = new Ray();
        targetPosisition = new Vector3();
    }

    private void Update()
    {
        m_ray.origin = transform.position;
        targetPosisition = m_target.position;
        targetPosisition.x -= m_ray.origin.x;
        targetPosisition.y -= m_ray.origin.y;
        targetPosisition.z -= m_ray.origin.z;

        Vector3 worldView = m_camera.WorldToViewportPoint(targetPosisition);
        m_ray.direction = Vector3.RotateTowards(m_ray.origin, targetPosisition, Mathf.Infinity, Mathf.Infinity);

        inView = (worldView.z <= 0f) ? 
            false : (worldView.x >= 0f && worldView.x <= 1f) ?
            true : false;
        //Debug.Log(Vector3.Angle(m_ray.origin, m_target.position));
    }

    private void FixedUpdate()
    {
        if (inView)
        {
            //Debug.Log("Seeing player!");
            Debug.DrawRay(m_ray.origin, m_ray.direction * Vector3.Distance(m_ray.origin, targetPosisition), Color.red);
            if (Physics.Raycast(m_ray, out m_out, Vector3.Distance(m_ray.origin, targetPosisition)))
            {
                if (m_out.transform.CompareTag("Player"))
                {
                    Debug.Log("Hit the player");
                }
            }
        }
    }
}
