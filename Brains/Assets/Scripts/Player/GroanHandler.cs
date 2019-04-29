using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroanHandler : MonoBehaviour {
    private float _currentAmount, _groanSpeed;
    private readonly float _groanRate = 7f;
    private Vector3 _groanTransformScale;

    public GameObject groanMeter;
    private Image _colorControl;

    [HideInInspector]
    public Vector3 groanLocation;
    [HideInInspector]
    public bool groaning;
    public float crawlPenalty = 1f;

    public bool pizza = false;

    private void Awake()
    {
        /* Initializing 'simple' data variables. */
        _currentAmount = 0f;
        _groanTransformScale = Vector3.zero;
        _groanTransformScale.y = 1f;
        _groanTransformScale.z = 1f;
        groaning = false;
        groanMeter = GameObject.Find("Groan Meter Fill");
        _colorControl = groanMeter.GetComponent<Image>();
        /*****************************************/
    }

    public void SetGroanSpeed(int mvState, float playDead)
    {
        if (playDead != 1f)
        {
            _groanSpeed = 1 / ((1f - playDead) * (Mathf.Abs(mvState - 5f) + _groanRate)); // manipulates the groan speed by the movement state and speed.

            if(mvState == 0)
                _groanSpeed = 1 / ((1f - playDead) * (Mathf.Abs(mvState - 15f) + _groanRate));
        }
        else
            _groanSpeed = 0f;

        _groanSpeed *= .5f;

        if (mvState == 5)
        {
            // @paul: Wanted to tweak the crawling rate, not sure where in the formula to tweak it.
            _groanSpeed *= crawlPenalty;
        }
    }

    public bool UpdateGroanAmount()
    {
        /* Updates the current amount both data-wise and visual-wise. */
        _currentAmount += (
            _groanSpeed * Time.deltaTime);

        _groanTransformScale.x = _currentAmount;
        groanMeter.transform.localScale = _groanTransformScale;

        _colorControl.color = new Color(
            _colorControl.color.r, _colorControl.color.g, _colorControl.color.b,_groanTransformScale.x);
        /**************************************************************/

        return (_currentAmount > 1f) ? true : false; // tells us that it is time to groan.. or not.
    }

    public void Groan()
    {
        /* Sets the states for groaning. */
        _currentAmount = 0f;
        if (!pizza)
            GetComponent<Player>().spudSounds.Play("Groan",
                UnityEngine.Random.Range(0f, .15f) + .85f,
                UnityEngine.Random.Range(0f, .15f) + .85f);
        else
            GetComponent<Player>().spudSounds.Play("Pizza",
                UnityEngine.Random.Range(0f, .15f) + .85f,
                UnityEngine.Random.Range(0f, .15f) + .85f);

        groaning = true;
        /*********************************/

        StartCoroutine(Groaning()); // Starts the groan
    }

    private IEnumerator Groaning()
    {
        /* Sets initial information for groaning. */
        groanLocation = transform.position;
        groanLocation.y += 1.5f; // sets the groan location to approximately spuds throat.

        /******************************************/

        /* Iterates across the length of the groan. */
        if (pizza)
        {
            yield return new WaitForSeconds(GetComponent<Player>().spudSounds.GetSound("Pizza").clip.length);
        }
        else
            yield return new WaitForSeconds(
                GetComponent<Player>().spudSounds.GetSound("Groan").clip.length);

        groaning = false; // closes out the groan state by ending the groaning.
    }
}
//(UnityEngine.Random.Range(0f, 3f) * 10f + 70f) / 100f