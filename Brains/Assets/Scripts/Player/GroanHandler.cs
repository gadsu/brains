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
    }

    public bool UpdateGroanAmount()
    {
        /* Updates the current amount both data-wise and visual-wise. */
        _currentAmount += (_groanSpeed * Time.deltaTime);
        _groanTransformScale.x = _currentAmount;
        groanMeter.transform.localScale = _groanTransformScale;
        _colorControl.color = new Color(_colorControl.color.r, _colorControl.color.g, _colorControl.color.b,_groanTransformScale.x);
        /**************************************************************/

        return (_currentAmount > 1f) ? true : false; // tells us that it is time to groan.. or not.
    }

    public void Groan()
    {
        /* Sets the states for groaning. */
        _currentAmount = 0f;
        GetComponent<Player>().spudSounds.Play("Groan");
        groaning = true;
        /*********************************/

        StartCoroutine(Groaning()); // Starts the groan
    }

    private IEnumerator Groaning()
    {
        /* Sets initial information for groaning. */
        /*int x = 0;*/ // acts as the length of the groan.
        groanLocation = transform.position;
        groanLocation.y += 1.5f; // sets the groan location to approximately spuds throat.

        /******************************************/

        /* Iterates across the length of the groan. */
        yield return new WaitForSeconds(GetComponent<Player>().spudSounds.GetSound("Groan").clip.length);

        groaning = false; // closes out the groan state by ending the groaning.
    }
}
