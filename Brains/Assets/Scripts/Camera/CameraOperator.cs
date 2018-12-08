using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperator : MonoBehaviour
{
    public GameObject positionTarget;				// Position to follow.
    private GameObject truePositionTarget;
    public GameObject lookTarget;					// Object to look at.
    private GameObject trueLookTarget;
    public bool doCinematicMode = false;
    private bool doTrackObject = false;
    //private Vector3 targetPos, pos, dist, camTarget, camRotation;

	public float maxVerticalAngle = 60f;
	public float minVerticalAngle = -70f;
	public Vector3 	rotSens = new Vector3(1.5f, 0.75f, 1f);
	public float baseSens = 1f;
    private float distToTLookTarget = 0f;
    public bool doFirstPerson = false;

	private Vector3	pivotOffsetStart = 	new Vector3(0,1f,0);
	private Vector3 pivotOffset = 		new Vector3(0,1f,0);	// Y-position (height) difference between camera and positionTarget, computed on start.
	private Vector3 truePivotGoal = 	new Vector3(0,1f,0);
	private Vector3 pivotOffsetGoal = 	new Vector3(0,1f,0);

    public Vector3 camOffsetStart =     new Vector3(0,0,0);
    public Quaternion camRotStart;
    private Vector3 camOffset = 		new Vector3(0,0,0);
	private Vector3 trueCamGoal = 		new Vector3(0,0,0);
	public Vector3 camOffsetGoal =  	new Vector3(0,0,0);

	private Vector3 rotDelta = Vector3.zero;		// Amount of rotation. Input.getAxis multiplied by sensitivity.
	private Quaternion aim;
	private float aimRatio = 0f;
	private Vector2 axisDamper = new Vector2(3f, 3f);
	private Vector2 input = Vector2.zero;
	public GameObject cam;
	private Camera camCamera;

	private float targetFOV;
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
    public float shoulderBumpUp= 0.3f;
    private Quaternion defaultCamRot;

	public int shoulderState = 0;

	public Vector3 tempLookAtRayPoint = Vector3.zero;


	//********************************************************************************************************
    private void Start()
    {
		Cursor.visible = false; ////////////////////////////////////////
		targetFOV = defaultFOV;
		camCamera = GetComponentInChildren<Camera>();
		zDistanceMax = zDistanceStart;
		zDistanceGoal = zDistanceStart;
		camOffsetStart = cam.transform.localPosition;
        camOffsetStart.y = 0.25f; // sort of a hack. <_<
        camRotStart = cam.transform.localRotation;
        camOffsetGoal = camOffsetStart;
        defaultCamRot = cam.transform.rotation;
        /*if (!positionTarget)
            positionTarget = GameObject.Find("GP_Spud");
        }*/

    }



    private void Update()
    {
        // ******* Playdead tracking
        truePositionTarget = positionTarget;
        if (positionTarget.GetComponent<Player>())
        {
            Player m_player = positionTarget.GetComponent<Player>();
            if (m_player.m_playDead == 1)
            {
                doTrackObject = true;
                trueLookTarget = m_player.limbToLookAt;
                //truePositionTarget = m_player.limbToLookAt;
                distToTLookTarget = Vector3.Distance(cam.transform.position, trueLookTarget.transform.position);
            }
            else
            {
                doTrackObject = false;
                trueLookTarget = lookTarget;
            }
        }

        if (!doCinematicMode)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                doFirstPerson = !doFirstPerson;
            }
            if (!doTrackObject && !doFirstPerson)
            {
                if (shoulderState == 0)
                    shoulderState = (Input.GetKeyDown(KeyCode.Q)) ? -1 : (Input.GetKeyDown(KeyCode.E)) ? 1 : shoulderState;
                else shoulderState = (Input.GetKeyDown(KeyCode.Q)) ? 0 : (Input.GetKeyDown(KeyCode.E)) ? 0 : shoulderState;
            }
            if(doFirstPerson)
            {
                shoulderState = 0;
                zDistanceGoal = -0.4f;
            }
            else { zDistanceGoal = zDistanceStart; }




            //******* PIVOT and CONTAINER POSITION
            pivotOffset = truePositionTarget.transform.position;
            pivotOffset.y = transform.position.y - truePositionTarget.transform.position.y;
            pivotOffsetGoal = pivotOffsetStart;

            mvState = (int)positionTarget.GetComponent<Player>().mState;
            if (mvState == 5)
            {
                pivotOffsetGoal.y = pivotOffsetStart.y - 0.75f; // how far to drop during crawl
            }

            // Set camera 3D position, with height offset.
            truePivotGoal.y = Mathf.Lerp(pivotOffset.y, pivotOffsetGoal.y, Time.deltaTime * 2);
            //truePivotOffset.x = Mathf.Lerp(pivotOffset.x, pivotOffsetGoal.x, Time.deltaTime);
            //truePivotOffset.z = Mathf.Lerp(pivotOffset.z, pivotOffsetGoal.z, Time.deltaTime);
            transform.position = new Vector3(
                truePositionTarget.transform.position.x,
                truePositionTarget.transform.position.y + truePivotGoal.y,
                truePositionTarget.transform.position.z
            );




            // ************** ROTATION
            // Set inputs
            if (!doTrackObject)
            {
                input.x = Input.GetAxis("Mouse X");
                input.y = Input.GetAxis("Mouse Y");

                // Cut out the vertical input if horizontal input is larger (this is nice, trust me)
                if (Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Mouse Y"))) axisDamper.y = 0f;
                else axisDamper.y = 3f;

                // Set rotation deltas
                rotDelta.x += Mathf.Clamp(input.x / 10, -1, 1) * rotSens.x * baseSens * axisDamper.x * 2;
                rotDelta.y += Mathf.Clamp(input.y / 10, -1, 1) * rotSens.y * baseSens * axisDamper.y * 2;

                // Limit vertical angle
                rotDelta.y = Mathf.Clamp(rotDelta.y, minVerticalAngle, maxVerticalAngle);

                // Set camera angle. DO THE THING!
                aim = Quaternion.Euler(-rotDelta.y, rotDelta.x, 0);
                transform.rotation = aim;
            }
        }


        // ********** FIELD-OF-VIEW
        if (doTrackObject)
        {
            targetFOV = 60f + (0.1f * distToTLookTarget);
        }
        else if(doFirstPerson)
        {
            // Boost FOV for first-person
            targetFOV = defaultFOV + 10f;
        }
        else targetFOV = defaultFOV;

        // Slight widening of FOV for high/low angles
        trueTargetFOV = targetFOV + Mathf.Clamp(((Mathf.Pow(Mathf.Abs(rotDelta.y), 2)) / 200) - 8, 0, maxFOVTweak);

        // Slight widening of FOV for crawling
        if (mvState == 5)
        {
            trueTargetFOV += 5f;
        }

        // Lerp to FOV target
        camCamera.fieldOfView = Mathf.Lerp(camCamera.fieldOfView, trueTargetFOV, Time.deltaTime * 8);

    }

	//********************************************************************************************************
	private void LateUpdate() {
        Vector3 pos = cam.transform.position;

        if (!doTrackObject)
        {
            cam.transform.localRotation = camRotStart;

            //********** Z CAMERA OFFSET
            RaycastHit hit;
            Vector3 dir = Vector3.back;
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






            //******** X AND Y CAMERA OFFSET
            camOffset = cam.transform.localPosition;
            camOffsetGoal = camOffsetStart;
            if (shoulderState != 0)
            {
                camOffsetGoal.x = camOffsetGoal.x * shoulderState;
                camOffsetGoal.y = camOffsetStart.y;
            }
            else
            {
                camOffsetGoal.x = 0;
                camOffsetGoal.y = camOffsetStart.y + shoulderBumpUp;
            }
            if (doFirstPerson)
            {
                camOffsetGoal.y = 0.25f;
            }

            trueCamGoal.y = Mathf.Lerp(camOffset.y, camOffsetGoal.y, Time.deltaTime * 4);
            trueCamGoal.x = Mathf.Lerp(camOffset.x, camOffsetGoal.x, Time.deltaTime * 4);
            cam.transform.localPosition = new Vector3(
                trueCamGoal.x,
                trueCamGoal.y,
                cam.transform.localPosition.z
            );

        }

        if(doTrackObject){
            cam.transform.LookAt(trueLookTarget.transform.position);
        }
    }
}