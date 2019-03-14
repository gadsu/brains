using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Camera tempCamera;
    Vector3 _targetPosition, _worldView;

    public Dictionary<string, string> animationGenericCalls;

    Ray _ray;
    RaycastHit _out;

    public bool inView, isVisible;
    public GameObject textNotice;

    [Range(1f, 5f)]
    public float xtime;
    public float playerSizeY;

    [HideInInspector]
    public float detectionAmount;
    private bool hearing = false;

    private void Start()
    {
        tempCamera.enabled = false;
        inView = false;
        isVisible = false;
        _ray = new Ray();
        _targetPosition = new Vector3();
        detectionAmount = 0f;
    }

    public void UpdatingDetectionAmountFromSound(float objectHearability)
    {
        if (!hearing)
        {
            detectionAmount += (gameObject.GetComponent<EnemyBase>().mHearValue * (objectHearability/3f) + objectHearability);
        }
        hearing = true;
    }

    public bool IsInView(Vector3 p_targetPosition)
    {
        _worldView = tempCamera.WorldToViewportPoint(p_targetPosition);

        inView = (_worldView.z < tempCamera.farClipPlane && _worldView.z > tempCamera.nearClipPlane) ?
            (_worldView.x < 1f && _worldView.x > 0f) ? true : false :
            false;

        isVisible = false;

        return inView;
    }

    public void UpdateRayToPlayer(Vector3 p_targetPosition, int pPlayDead)
    {
        _ray.origin = tempCamera.transform.position;
        _targetPosition = p_targetPosition;
        _targetPosition.x = _targetPosition.x - _ray.origin.x;
        if (pPlayDead == 0)
            _targetPosition.y = (_targetPosition.y - _ray.origin.y) + (.5f * playerSizeY);
        else
            _targetPosition.y = (_targetPosition.y - _ray.origin.y) - (playerSizeY) + .2f;
        _targetPosition.z -= _ray.origin.z;

        _ray.direction = Vector3.RotateTowards(_ray.origin, _targetPosition, Mathf.Infinity, Mathf.Infinity);
    }

    public bool IsVisible(Vector3 p_targetPosition)
    {
        if (Physics.Raycast(_ray, out _out, tempCamera.farClipPlane))
            if (_out.transform.CompareTag("Player")) isVisible = true;

        return isVisible;
    }

    public void UpdatingDetectionAmount(int p_sight, int p_hear, Transform p_player, int p_detection, int p_awareness)
    {
        float _s = p_player.GetComponent<StealthHandler>().Stealth_val;
        detectionAmount += ((((p_detection - 3) + p_awareness) - _s) + p_sight - 1.5f) * .25f;


        if (detectionAmount > 100f)
        {
            detectionAmount = 100f;
            Debug.Log("Detected");
        }

        if (detectionAmount < 0f)
        {
            detectionAmount = 0f;
            Debug.Log("Lost");
        }

        Debug.Log("<color=orange>" + detectionAmount + "</color>");
    }

    public void NotHearing()
    {
        hearing = false;
    }
}
