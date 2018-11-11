using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectPlayer : MonoBehaviour
{
    public Camera m_camera;
    Transform m_target;
    Vector3 m_targetPosition, worldView;


    Ray m_ray;
    RaycastHit m_out;

    bool m_inView, m_isVisible;
    public GameObject m_text_notice;

    [Range(1f, 5f)]
    public float xtime;
    private float m_playerSizeY;
    private void Awake()
    {
        m_target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        m_camera.enabled = false;
        m_inView = false;
        m_isVisible = false;
        m_ray = new Ray();
        m_targetPosition = new Vector3();
        m_playerSizeY = GameObject.Find("Player").transform.lossyScale.y;
    }

    public bool IsInView(Vector3 p_targetPosition)
    {
        m_ray.origin = transform.position;
        m_targetPosition = p_targetPosition;
        m_targetPosition.x -= m_ray.origin.x;
        m_targetPosition.y = (m_targetPosition.y + m_playerSizeY) -  m_ray.origin.y;
        m_targetPosition.z -= m_ray.origin.z;

        worldView = m_camera.WorldToViewportPoint(m_targetPosition);
        m_ray.direction = Vector3.RotateTowards(m_ray.origin, m_targetPosition, Mathf.Infinity, Mathf.Infinity);

        m_inView = (worldView.z < 0f) ? 
            false : (worldView.x > 0f && worldView.x < 1f) ?
            true : false;
        
        return m_inView;
    }

    public bool IsVisible()
    {
        m_isVisible = false;
        if (m_inView)
            if (Physics.Raycast(m_ray, out m_out, Vector3.Distance(m_ray.origin, m_targetPosition)))
                if (m_out.transform.CompareTag("Player"))
                    m_isVisible = true;

        return m_isVisible;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            ReloadScene(SceneManager.GetActiveScene(), xtime);      
        }
    }

    private IEnumerator ReloadScene(Scene _scene, float _time)
    {
        m_text_notice.SetActive(true);
        yield return new WaitForSecondsRealtime(_time);
        SceneManager.LoadScene(_scene.name);
    }
}
