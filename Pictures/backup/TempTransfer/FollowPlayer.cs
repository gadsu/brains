using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject positionTarget;			// Position to follow.
	public GameObject lookTarget;				// Object to look at.
    //private Vector3 targetPos, pos, dist, camTarget, camRotation;

	public float 	rotationMaxUp = 0.6f;
	public float 	rotationMaxDown = 0.2f;
	public Vector3 	rotSens = new Vector3(2f, 0.5f, 1f);
	private float 	pivotHeightOffset = 1f;		// Y-position (height) difference between camera and positionTarget, computed on start.

	private Vector3 rotDelta = Vector3.zero;	// Amount of rotation. Input.getAxis multiplied by sensitivity.
	private Vector3 rotDeltasMax = Vector3.zero;// Largest rotDelta.
	private Vector3 rotGoal = Vector3.zero;		// Goals for rotation.
	private Vector3 rotGoalProg = Vector3.zero;	// Ratio of progress to goal.
	private Vector3 rot = Vector3.zero;	// Current camera rotation.

    private void Start()
    {
		Cursor.visible = false;
		pivotHeightOffset = transform.position.y - positionTarget.transform.position.y;
    }

    private void Update()
    {
		rot = transform.rotation.eulerAngles;

		// Set camera 3D position, with height offset.
		transform.position = new Vector3(
			positionTarget.transform.position.x, 
			positionTarget.transform.position.y + pivotHeightOffset, 
			positionTarget.transform.position.z
		);

		// Get deltas from player inputs. TODO make this separate function
		rotDelta = new Vector3(
			Input.GetAxis("Mouse X") * rotSens.x,
			Input.GetAxis("Mouse Y") * rotSens.y,
			0f
		);

		// Check maximums: If current delta is bigger than max delta, it becomes max delta.
		if (Mathf.Abs(rotDelta.x) > Mathf.Abs(rotDeltasMax.x) || Mathf.Sign(rotDelta.x) != Mathf.Sign(rotDeltasMax.x)) {
			rotDeltasMax.x = rotDelta.x;
		}
		if (Mathf.Abs(rotDelta.y) > Mathf.Abs(rotDeltasMax.y)) {
			rotDeltasMax.y = rotDelta.y;
		}
		if (Mathf.Abs(rotDelta.z) > Mathf.Abs(rotDeltasMax.z)) {
			rotDeltasMax.z = rotDelta.z;
		}

		// Set rotation goal.
		rotGoal = new Vector3(
			rot.x + rotDeltasMax.x,
			rot.y + rotDeltasMax.y,
			rot.z + rotDeltasMax.z
		);

		if (Mathf.Abs(rot.x - rotGoal.x) < 0.01f) {
			rotDeltasMax.x = 0f;
			rotGoal.x = rot.x;
		}

		// Set goal progress.
		/*rotGoalProg = new Vector3(
			rotGoal.x / rot.x,
			rotGoal.y - rot.y,
			rotGoal.z - rot.z
		);*/

		/*transform.Rotate(new Vector3(
			Mathf.Lerp(rot.x, rotGoal.x, 0.5f),
			0,//Mathf.Lerp(rot.y, rotGoal.y, rotGoalProg.y + 0.05f),
			0//Mathf.Lerp(rot.z, rotGoal.z, rotGoalProg.z + 0.05f) //TODO check axis equivalency
		));*/
		//transform.RotateAround(positionTarget.transform.position, transform.position, Mathf.Lerp (rot.x, rotGoal.x, 0.5f*Mathf.Abs(rot.x - rotGoal.x)*Mathf.Sign(rotDelta.x) ));


		/*
		if (Mathf.abs(x_delta - x_delta_max) > ) {
			x_delta_max = x_delta;
		}
		goal.x = transform.rotation.y + x_delta_max;
		goalProgress.x = goal.x / transform.rotation.y;
		transform.Rotate(new Vector3 (0f, Mathf.Lerp(transform.rotation.y, transform.rotation.y + x_delta, goalProgress.x + 0.05f), 0f));
		if (Mathf.Abs(goalProgress.x) > 0.99f) {
			x_delta_max = 0f;
		}*/





		//--------------------------------------------
		//Mouse_XOutput = Input.GetAxis("Mouse X");
		transform.RotateAround(transform.position, new Vector3(0f, Input.GetAxis("Mouse X"), 0f), rotSens.x);
		//Mouse_YOutput = Input.GetAxis("Mouse Y");
		transform.Rotate(new Vector3(-(Input.GetAxis("Mouse Y")), 0f, 0f) *rotSens.y);

		//transform.RotateAround(targetPos, new Vector3(0f, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")), m_rate*Time.deltaTime);

		//camTarget = new Vector3 (0f, 0f, 0f);//Input.GetAxis ("Mouse X"), 0f);
		//transform.RotateAround (targetPos, Vector3.SmoothDamp (camRotation, camTarget, ref velocity, smoothTime), m_rate * Time.deltaTime * x_sens);
	}
}