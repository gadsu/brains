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
	public Vector3 	rotSens = new Vector3(1.5f, 0.75f, 1f);
	public float baseSens = 1f;

	public 	Vector3	pivotOffsetStart = 	new Vector3(0,1f,0);
	private Vector3 pivotOffset = 		new Vector3(0,1f,0);	// Y-position (height) difference between camera and positionTarget, computed on start.
	private Vector3 truePivotGoal = 	new Vector3(0,1f,0);
	private Vector3 pivotOffsetGoal = 	new Vector3(0,1f,0);

	public 	Vector3	camOffsetStart = 	new Vector3(0,0,0);
	private Vector3 camOffset = 		new Vector3(0,0,0);
	private Vector3 trueCamGoal = 		new Vector3(0,0,0);
	private Vector3 camOffsetGoal = 	new Vector3(0,0,0);

	private Vector3 rotDelta = Vector3.zero;		// Amount of rotation. Input.getAxis multiplied by sensitivity.
	private Quaternion aim;
	private float aimRatio = 0f;
	private Vector2 axisDamper = new Vector2(3f, 3f);
	private Vector2 input = Vector2.zero;
	public GameObject cam;
	private Camera camCamera;

	public float targetFOV;
	public float defaultFOV = 60f;
	private float trueTargetFOV;
	public float maxFOVTweak = 25f;

	public float zDistanceStart = 1.38f;
	public float camPushRayStartLength = 2f;
	private float camPushRayLength = 0f;
	private float zDistanceMin = 0.1f;
	private float zDistanceMax = 10f;
	public int zDistanceInvert = -1;
	public float zDistanceGoal;
	private float trueZDistanceGoal;
	private int mvState;

	public int shoulderState = 0;

	public Vector3 tempLookAtRayPoint = Vector3.zero;


	//********************************************************************************************************
    private void Start()
    {
		Cursor.visible = false;
		targetFOV = defaultFOV;
		camCamera = GetComponentInChildren<Camera>();
		zDistanceMax = zDistanceStart;
		zDistanceGoal = zDistanceStart;
		camOffsetStart = cam.transform.localPosition;
		camOffsetGoal = camOffsetStart;

    }

    private void Update()
    {
		if(shoulderState == 0)
			shoulderState = (Input.GetKeyDown(KeyCode.Q)) ? -1 : (Input.GetKeyDown(KeyCode.E)) ? 1 : shoulderState;
		else
			shoulderState = (Input.GetKeyDown(KeyCode.Q)) ? 0 : (Input.GetKeyDown(KeyCode.E)) ? 0 : shoulderState;
		
		pivotOffset = positionTarget.transform.position;
		pivotOffset.y = transform.position.y - positionTarget.transform.position.y;
		pivotOffsetGoal = pivotOffsetStart;

		mvState = (int)positionTarget.GetComponent<Player>().mState;
		if (mvState == 5) {
			pivotOffsetGoal.y = pivotOffsetStart.y -1f;
		}

		// Set camera 3D position, with height offset.
		truePivotGoal.y = Mathf.Lerp(pivotOffset.y, pivotOffsetGoal.y, Time.deltaTime * 2);
		//truePivotOffset.x = Mathf.Lerp(pivotOffset.x, pivotOffsetGoal.x, Time.deltaTime);
		//truePivotOffset.z = Mathf.Lerp(pivotOffset.z, pivotOffsetGoal.z, Time.deltaTime);
		transform.position = new Vector3(
			positionTarget.transform.position.x, 
			positionTarget.transform.position.y + truePivotGoal.y,
			positionTarget.transform.position.z
		);

		// Set inputs
		input.x = Input.GetAxis ("Mouse X");
		input.y = Input.GetAxis ("Mouse Y");

		// Cut out the vertical input if horizontal input is larger (this is nice, trust me)
		// TODO: Make this nicer?
		if (Mathf.Abs (Input.GetAxis ("Mouse X")) > Mathf.Abs (Input.GetAxis ("Mouse Y"))) axisDamper.y = 0f;
		else axisDamper.y = 3f;

		// Set rotation deltas
		rotDelta.x += Mathf.Clamp(input.x / 10, -1, 1) * rotSens.x * baseSens * axisDamper.x * 2;
		rotDelta.y += Mathf.Clamp(input.y / 10, -1, 1) * rotSens.y * baseSens * axisDamper.y * 2;

		// Limit vertical angle
		rotDelta.y = Mathf.Clamp(rotDelta.y, minVerticalAngle, maxVerticalAngle);

		// Slight widening of FOV for high/low angles
		trueTargetFOV = targetFOV + Mathf.Clamp(((Mathf.Pow(Mathf.Abs(rotDelta.y), 2)) / 200) - 8, 0, maxFOVTweak);

		// Slight widening of FOV for crawling
		if (mvState == 5) {
			trueTargetFOV += 5f;
		}

		// Lerp to FOV target
		camCamera.fieldOfView = Mathf.Lerp (camCamera.fieldOfView, trueTargetFOV,  Time.deltaTime * 8);

		//TODO: Grab movement states for height change

		// Set camera angle. DO THE THING!
		aim = Quaternion.Euler(-rotDelta.y, rotDelta.x, 0);
		transform.rotation = aim;

	}

	//********************************************************************************************************
	private void LateUpdate() {
		RaycastHit hit;
		Vector3 dir = Vector3.back;
		Vector3 pos = cam.transform.position;

		trueZDistanceGoal = zDistanceGoal;
		camPushRayLength = camPushRayStartLength;
		// Make sure the raycast isn't too long or short
		//Mathf.Clamp(camPushRayLength, 0.1f, camPushRayStartLength);

		// Check for intersection
		if (Physics.Raycast(pos, transform.TransformDirection(dir), out hit, camPushRayLength))
		{
			Debug.DrawRay(pos, transform.TransformDirection(dir) * hit.distance, Color.yellow);

		}
		else
		{
			Debug.DrawRay(pos, transform.TransformDirection(dir) * camPushRayLength, Color.green);
		}

		// Limit z distance
		Mathf.Clamp(trueZDistanceGoal, zDistanceMin, zDistanceMax);

		// Set z distance. DO THE THINGY
		Vector3 derp = cam.transform.localPosition;
		cam.transform.localPosition = new Vector3(derp.x, derp.y, trueZDistanceGoal * zDistanceInvert);



		camOffset = cam.transform.localPosition;
		camOffsetGoal = camOffsetStart;
		if (shoulderState != 0) {
			camOffsetGoal.x = camOffsetGoal.x * shoulderState;
		} else
			camOffsetGoal.x = 0;

		trueCamGoal.y = Mathf.Lerp(camOffset.y, camOffsetGoal.y, Time.deltaTime * 4);
		trueCamGoal.x = Mathf.Lerp(camOffset.x, camOffsetGoal.x, Time.deltaTime * 4);
		cam.transform.localPosition = new Vector3(
			trueCamGoal.x,
			trueCamGoal.y,
			cam.transform.localPosition.z
		);


	}
}