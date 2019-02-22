﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool hearing = false;

    private void Start()
    {
        m_camera.enabled = false;
        m_inView = false;
        m_isVisible = false;
        m_ray = new Ray();
        m_targetPosition = new Vector3();
        m_detectionAmount = 0f;
    }

    public void UpdatingDetectionAmountFromSound(float objectHearability)
    {
        if (!hearing)
        {
            m_detectionAmount += (gameObject.GetComponent<EnemyBase>().mAEnemy_hearValue * (objectHearability/3f) + objectHearability);
        }
        hearing = true;
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

    public void UpdateRayToPlayer(Vector3 p_targetPosition, int pPlayDead)
    {
        m_ray.origin = m_camera.transform.position;
        m_targetPosition = p_targetPosition;
        m_targetPosition.x = m_targetPosition.x - m_ray.origin.x;
        if (pPlayDead == 0)
            m_targetPosition.y = (m_targetPosition.y - m_ray.origin.y) + (.5f * m_playerSizeY);
        else
            m_targetPosition.y = (m_targetPosition.y - m_ray.origin.y) - (m_playerSizeY) + .2f;
        m_targetPosition.z -= m_ray.origin.z;

        m_ray.direction = Vector3.RotateTowards(m_ray.origin, m_targetPosition, Mathf.Infinity, Mathf.Infinity);
    }

    public bool IsVisible(Vector3 p_targetPosition)
    {
        if (Physics.Raycast(m_ray, out m_out, m_camera.farClipPlane))
            if (m_out.transform.CompareTag("Player")) m_isVisible = true;

        return m_isVisible;
    }

    public void UpdatingDetectionAmount(int p_sight, int p_hear, Transform p_player, int p_detection, int p_awareness)
    {
        float _s = p_player.GetComponent<StealthHandler>().Stealth_val;
        m_detectionAmount += ((((p_detection - 3) + p_awareness) - _s) + p_sight - 1.5f) * .25f;


        if (m_detectionAmount > 100f)
        {
            m_detectionAmount = 100f;
            Debug.Log("Detected");
        }

        if (m_detectionAmount < 0f)
        {
            m_detectionAmount = 0f;
            Debug.Log("Lost");
        }

        Debug.Log("<color=orange>" + m_detectionAmount + "</color>");
    }

    public void NotHearing()
    {
        hearing = false;
    }
}
