using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroanHandler : MonoBehaviour {
    private float currentAmount, nextAmount, groanSpeed;
    private Vector3 groanTransformScale;

    [Range(.1f, .5f)]
    public float groanRate;

    public Image groanMeter;

    public GameObject agentTester;

    private void Awake()
    {
        currentAmount = 0f;
        nextAmount = 0f;
        groanTransformScale = Vector3.zero;
        groanTransformScale.y = 1f;
        groanTransformScale.z = 1f;
    }
   
    public void SetGroanSpeed(int mvState, float mvSpeed)
    {
        groanSpeed = groanRate + (.5f * groanRate)*(mvSpeed * Mathf.Log(mvState + 1));
    }

    public bool UpdateGroanAmount()
    {
        nextAmount = currentAmount + (groanSpeed * Time.deltaTime);
        currentAmount = nextAmount;
        groanTransformScale.x = currentAmount;
        groanMeter.transform.localScale = groanTransformScale;

        if (currentAmount > 1f)
        {
            return true;
        }

        return false;
    }

    public void Groan()
    {
        currentAmount = 0f;
        StartCoroutine(Groaning());
    }

    private IEnumerator Groaning()
    {
        int x = 0;
        Vector3 groanLocation = transform.position;
        do
        {
            x++;
            if (x == 3)
            {
                agentTester.GetComponent<TutorialMoveTo>().PathTo(groanLocation);
            }
            Debug.Log("Groan!");
            yield return new WaitForSeconds(.5f);
        } while (x < 5);
    }
}
