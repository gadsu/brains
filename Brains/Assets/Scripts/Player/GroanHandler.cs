using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroanHandler : MonoBehaviour {
    private float currentAmount, groanSpeed;
    private Vector3 groanTransformScale;

    public GameObject agentTester, groanSphere;
    public Image groanMeter;

    [Range(.01f, .1f)]
    public float groanRate;

    public Vector3 groanLocation;

    public bool groaning;

    private void Awake()
    {
        /* Initializing 'simple' data variables. */
        currentAmount = 0f;
        nextAmount = 0f;
        groanTransformScale = Vector3.zero;
        groanTransformScale.y = 1f;
        groanTransformScale.z = 1f;
        groaning = false;
        /*****************************************/
    }

    public void SetGroanSpeed(int mvState, float mvSpeed)
    {
        groanSpeed = groanRate + (.5f * groanRate)*(mvSpeed * Mathf.Log(mvState + 1)); // manipulates the groan speed by the movement state and speed.
    }

    public bool UpdateGroanAmount()
    {
        /* Updates the current amount both data-wise and visual-wise. */
        currentAmount = currentAmount + (groanSpeed * Time.deltaTime);
        groanTransformScale.x = currentAmount;
        groanMeter.transform.localScale = groanTransformScale;
        /**************************************************************/

        return (currentAmount > 1f) ? true : false; // tells us that it is time to groan.. or not.
    }

    public void Groan()
    {
        /* Sets the states for groaning. */
        currentAmount = 0f;
        groaning = true;
        /*********************************/

        StartCoroutine(Groaning()); // Starts the groan
    }

    private IEnumerator Groaning()
    {
        /* Sets initial information for groaning. */
        int x = 0; // acts as the length of the groan.
        groanLocation = transform.position;
        groanLocation.y += 1.5f; // sets the groan location to approximately spuds throat.
        /******************************************/

        /* Iterates across the length of the groan. */
        do
        {
            x++;
            //Debug.Log("Groan!");
            yield return new WaitForSeconds(.2f);
        } while (x < 5);
        /********************************************/

        groaning = false; // closes out the groan state by ending the groaning.
    }
}
