using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectPlayer : MonoBehaviour
{
    public Camera m_camera;
    Vector3 m_targetPosition, worldView;


    Ray m_ray;
    RaycastHit m_out;

    bool m_inView, m_isVisible;
    public GameObject m_text_notice;

    [Range(1f, 5f)]
    public float xtime;
    private float m_playerSizeY;
    private bool failing = false;
    public float seeDistance = 2f;
    private void Awake()
    {
    }

    private void Start()
    {
        m_camera.enabled = false;
        m_inView = false;
        m_isVisible = false;
        m_ray = new Ray();
        m_targetPosition = new Vector3();
        m_playerSizeY = GameObject.Find("Spud").transform.lossyScale.y;
        failing = false;
    }

    public bool IsInView(Vector3 p_targetPosition)
    {
        m_ray.origin = m_camera.transform.position;
        m_targetPosition = p_targetPosition;
        m_targetPosition.x -= m_ray.origin.x;
        m_targetPosition.y = (m_targetPosition.y + m_playerSizeY) -  m_ray.origin.y;
        m_targetPosition.z -= m_ray.origin.z;

        worldView = m_camera.WorldToViewportPoint(m_targetPosition);
        m_ray.direction = Vector3.RotateTowards(m_ray.origin, m_targetPosition, Mathf.Infinity, Mathf.Infinity);

        m_inView = (worldView.z < m_camera.nearClipPlane || worldView.z > m_camera.farClipPlane) ? 
            false : (worldView.x > 0f && worldView.x < 1f) ?
            true : false;
        
        return m_inView;
    }

    public bool IsVisible(Vector3 p_targetPosition)
    {
        m_isVisible = false;
        if (m_inView)
        {
            Debug.DrawRay(m_ray.origin, m_ray.direction * Vector3.Distance(m_ray.origin, m_targetPosition), Color.red);
            if (Physics.Raycast(m_ray, out m_out, Vector3.Distance(m_ray.origin, p_targetPosition)))
                if (m_out.transform.CompareTag("Player"))
                    m_isVisible = true;
        }
        return m_isVisible;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") && !failing)
        {
            failing = true;
            FindObjectOfType<AudioManager>().Play("Fail");
            FindObjectOfType<AudioManager>().setVol("BGMusicHigh", 0.75f);
            m_text_notice.SetActive(true);
            GetComponent<EnemyBase>().blockingAnim("A_TomAttack");
            StartCoroutine(ReloadScene(SceneManager.GetActiveScene(), xtime));  
        }
    }

    private IEnumerator ReloadScene(Scene _scene, float _time)
    {
        yield return new WaitForSecondsRealtime(_time);
        FindObjectOfType<AudioManager>().Start();
        SceneManager.LoadScene(_scene.name);
    }
}
