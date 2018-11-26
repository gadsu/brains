using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperator : MonoBehaviour
{
    public GameObject positionTarget;				// Position to follow.
	public GameObject lookTarget;					// Object to look at.
    //private Vector3 targetPos, pos, dist, camTarget, camRotation;

	public float maxVerticalAngle = 60f;
	public float minVerticalAngle = -70f;
	public Vector3 	rotSens = new Vector3(1f, 1f, 1f);
	private float 	pivotHeightOffset = 1f;			// Y-position (height) difference between camera and positionTarget, computed on start.

	private Vector3 rotDelta = Vector3.zero;		// Amount of rotation. Input.getAxis multiplied by sensitivity.
	//private Vector3 rotDeltasMax = Vector3.zero;	// Largest rotDelta.
	//private Vector3 rotGoal = Vector3.zero;		// Goals for rotation.
	//private Vector3 rotGoalProg = Vector3.zero;	// Ratio of progress to goal.
	//private Vector3 rot = Vector3.zero;			// Current camera rotation.
	private Quaternion aim;
	private float aimRatio = 0f;
	private Vector2 axisDamper = new Vector2(3f, 3f);
	private Vector2 input = Vector2.zero;
	public Camera cam;
	public float targetFOV;
	public float defaultFOV = 60f;
	private float trueTargetFOV;
	public float maxFOVTweak = 25f;
	//public ACharacter m_getPlayer;


	//********************************************************************************************************
    private void Start()
    {
		Cursor.visible = false;
		pivotHeightOffset = transform.position.y - positionTarget.transform.position.y;
		cam = GetComponentInChildren<Camera>();
		targetFOV = defaultFOV;
    }

    private void Update()
    {
		// Set camera 3D position, with height offset.
		transform.position = new Vector3(
			positionTarget.transform.position.x, 
			positionTarget.transform.position.y + pivotHeightOffset, 
			positionTarget.transform.position.z
		);

		// Set inputs
		input.x = Input.GetAxis ("Mouse X");
		input.y = Input.GetAxis ("Mouse Y");

		// Cut out the vertical input if horizontal input is larger (this is nice, trust me)
		// TODO: Make this nicer
		if (Mathf.Abs (Input.GetAxis ("Mouse X")) > Mathf.Abs (Input.GetAxis ("Mouse Y"))) axisDamper.y = 0f;
		else axisDamper.y = 3f;

		// Set rotation deltas
		rotDelta.x += Mathf.Clamp(input.x / 10, -1, 1) * rotSens.x * axisDamper.x * 2;
		rotDelta.y += Mathf.Clamp(input.y / 10, -1, 1) * rotSens.y * axisDamper.y * 2;

		// Limit vertical angle
		rotDelta.y = Mathf.Clamp(rotDelta.y, minVerticalAngle, maxVerticalAngle);

		// Slight widening of FOV for high/low angles
		trueTargetFOV = targetFOV + Mathf.Clamp(((Mathf.Pow(Mathf.Abs(rotDelta.y), 2)) / 200) - 8, 0, maxFOVTweak);

		// Lerp to FOV target
		cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, trueTargetFOV,  Time.deltaTime * 5);

		//TODO: Grab movement states for height change

		// Set camera angle. DO THE THING!
		aim = Quaternion.Euler(-rotDelta.y, rotDelta.x, 0);
		transform.rotation = aim;


		//********************************************************************************************************

		// Get deltas from player inputs. TODO make this separate function
		/*
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
		}*/

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

		//-------------------------------------------------------------------------------------------
		//transform.RotateAround (positionTarget.transform.position, Vector3.up, rotDelta.x * Time.deltaTime);
		//transform.RotateAround (positionTarget.transform.position, Vector3.left, rotDelta.y * Time.deltaTime);
		//Mouse_YOutput = Input.GetAxis("Mouse Y");
		//transform.RotateAround(Vector3.up, new Vector3(-(Input.GetAxis("Mouse Y")), 0f, 0f), rotSens.y *Time.deltaTime * 100);

		//transform.RotateAround(targetPos, new Vector3(0f, Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")), m_rate*Time.deltaTime);

		//camTarget = new Vector3 (0f, 0f, 0f);//Input.GetAxis ("Mouse X"), 0f);
		//transform.RotateAround (targetPos, Vector3.SmoothDamp (camRotation, camTarget, ref velocity, smoothTime), m_rate * Time.deltaTime * x_sens);
	}
}