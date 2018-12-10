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

    public bool m_inView, m_isVisible;
    public GameObject m_text_notice;

    [Range(1f, 5f)]
    public float xtime;
    public float m_playerSizeY;

    [HideInInspector]
    public float m_detectionAmount;

    private void Start()
    {
        m_camera.enabled = false;
        m_inView = false;
        m_isVisible = false;
        m_ray = new Ray();
        m_targetPosition = new Vector3();
        m_detectionAmount = 0f;
    }

    public bool IsInView(Vector3 p_targetPosition)
    {
        worldView = m_camera.WorldToViewportPoint(p_targetPosition);

        m_inView = (worldView.z < m_camera.farClipPlane && worldView.z > m_camera.nearClipPlane) ?
            (worldView.x < 1f && worldView.x > 0f) ? true : false :
            false;

        m_isVisible = false;

        return m_inView;
    }

    public void UpdateRayToPlayer(Vector3 p_targetPosition)
    {
        m_ray.origin = m_camera.transform.position;
        m_targetPosition = p_targetPosition;
        m_targetPosition.x = m_targetPosition.x - m_ray.origin.x;
        m_targetPosition.y = (m_targetPosition.y - m_ray.origin.y) + (.5f * m_playerSizeY);
        m_targetPosition.z -= m_ray.origin.z;

        m_ray.direction = Vector3.RotateTowards(m_ray.origin, m_targetPosition, Mathf.Infinity, Mathf.Infinity);
    }

    public bool IsVisible(Vector3 p_targetPosition)
    {
        if (Physics.Raycast(m_ray, out m_out, m_camera.farClipPlane))
            if (m_out.transform.CompareTag("Player")) {
                m_isVisible = true;
            }

        return m_isVisible;
    }

    public void UpdatingDetectionAmount(int p_sight, int p_hear, Transform p_player, int p_detection, int p_awareness)
    {
        float _s = p_player.GetComponent<StealthHandler>().Stealth_val;
        //Debug.Log(m_detectionAmount);
        m_detectionAmount += ((((p_detection - 3) + p_awareness) + _s) + p_sight - 1.5f) * Time.deltaTime;


        if (m_detectionAmount > 100f)
        {
            m_detectionAmount = 0f;
            //Debug.Log("Detected");
        }

        if (m_detectionAmount < 0f)
        {
            m_detectionAmount = 0f;
            //Debug.Log("Lost");
        }
    }
    /*
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
    */
}
