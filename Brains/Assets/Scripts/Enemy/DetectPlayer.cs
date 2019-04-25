using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    public Camera tempCamera;

    Vector3 _targetPosition, _worldView;
    public Transform targetTransform;

    public Dictionary<string, string> animationGenericCalls;

    Ray _ray;
    RaycastHit _out;

    [HideInInspector]
    public bool inView, isVisible;

    [HideInInspector]
    public float detectionAmount;
    private bool hearing = false;

    public static float bMkSoundLocation;

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
            detectionAmount +=
                gameObject.GetComponent<EnemyBase>().mHearValue +
                (objectHearability / 3f) +
                objectHearability;
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
    } // End of IsInView(1 Vector3)

    public void UpdateRayToPlayer(Vector3 p_targetPosition, int pPlayDead)
    {
        /*** Update ray dependant positions ***/
        _ray.origin = tempCamera.transform.position;
        _targetPosition = targetTransform.position;
        _ray.direction = (_targetPosition - _ray.origin) * Vector3.Distance(targetTransform.position, transform.position);
        /**************************************/
    } // End of UpdateRayToPlayer(1 Vector3, 1 int)

    public bool IsVisible(Vector3 p_targetPosition)
    {
        if (Physics.Raycast(_ray, out _out, tempCamera.farClipPlane))
            if (_out.transform.GetComponentInParent<Player>() != null) isVisible = true;
            // End of CompareTag("Player")
        // End of Raycast

        return isVisible;
    } // End of IsVisible(1 Vector3)

    public void UpdatingDetectionAmount(int p_sight, int p_hear, Transform p_player, int p_detection, int p_awareness)
    {
        if (detectionAmount < 0f)
        {
            detectionAmount = 0f;
        } // End of detectionAmount < 0f
        else if (detectionAmount > 100f)
        {
            detectionAmount = 100f;
        } // End of detectionAmount > 100f
        else
        {
            if (!isVisible)
                detectionAmount -= .1f;
            // End of !isVisible
            else
                detectionAmount += p_player.GetComponent<StealthHandler>().Stealth_val +
                    (p_sight + 1f) *
                    (1f / 2f) * p_player.GetComponent<StealthHandler>().Stealth_val;
            // End of isVisible

            if (detectionAmount > bMkSoundLocation && hearing)
                GetComponent<EnemyBase>().knownLocation = GameObject.Find("Spud").transform.position;
        } // End of 0 < detectionAmount < 100.0...1

    } // End of UpdatingDetectionAmount(4 int, 1 Transform)

    public void NotHearing()
    {
        hearing = false;
    }
}