using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DetectPlayer : MonoBehaviour
{
    Camera m_camera;
    Transform m_target;
    Vector3 m_targetPosisition, worldView;


    Ray m_ray;
    RaycastHit m_out;

    bool inView, isVisible;

    private void Awake()
    {
        m_camera = GetComponent<Camera>();
        m_target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        m_camera.enabled = false;
        inView = false;
        isVisible = false;
        m_ray = new Ray();
        m_targetPosisition = new Vector3();
    }

    public bool IsInView(Vector3 p_targetPosition)
    {
        m_ray.origin = transform.position;
        m_targetPosisition = p_targetPosition;
        m_targetPosisition.x -= m_ray.origin.x;
        m_targetPosisition.y -= m_ray.origin.y;
        m_targetPosisition.z -= m_ray.origin.z;

        worldView = m_camera.WorldToViewportPoint(m_targetPosisition);
        m_ray.direction = Vector3.RotateTowards(m_ray.origin, m_targetPosisition, Mathf.Infinity, Mathf.Infinity);

        inView = (worldView.z < 0f) ? 
            false : (worldView.x > 0f && worldView.x < 1f) ?
            true : false;
        
        return inView;
    }

    public bool IsVisible()
    {
        m_isVisible = false;
        if (m_inView)
            if (Physics.Raycast(m_ray, out m_out, Vector3.Distance(m_ray.origin, m_targetPosisition)))
                if (m_out.transform.CompareTag("Player"))
                    m_isVisible = true;

        return m_isVisible;
    }
}
