using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOperator : MonoBehaviour
{
    public GameObject positionTarget;				// Position to follow.
    private GameObject _truePositionTarget;
    public GameObject lookTarget;					// Object to look at.
    private GameObject _trueLookTarget;
    public bool doCinematicMode = false;
    private bool _doTrackObject = false;
    //private Vector3 targetPos, pos, dist, camTarget, camRotation;

	public float maxVerticalAngle = 60f;
	public float minVerticalAngle = -70f;
	public Vector3 	rotSens = new Vector3(1.5f, 0.75f, 1f);
	public float baseSens = 1f;
    private float _baseSensStart = 1f;
    private float _distToTLookTarget = 0f;
    public bool doFirstPerson = false;

	private Vector3	_pivotOffsetStart = 	new Vector3(0,1f,0);
	private Vector3 _pivotOffset = 		new Vector3(0,1f,0);	// Y-position (height) difference between camera and positionTarget, computed on start.
	private Vector3 _truePivotGoal = 	new Vector3(0,1f,0);
	private Vector3 _pivotOffsetGoal = 	new Vector3(0,1f,0);

    public Vector3 camOffsetStart =     new Vector3(0,0,0);
    public Quaternion camRotStart;
    private Vector3 _camOffset = 		new Vector3(0,0,0);
	private Vector3 _trueCamGoal = 		new Vector3(0,0,0);
	public Vector3 camOffsetGoal =  	new Vector3(0,0,0);

	private Vector3 _rotDelta = Vector3.zero;		// Amount of rotation. Input.getAxis multiplied by sensitivity.
	private Quaternion _aim;
	private float _aimRatio = 0f;
	private Vector2 _axisDamper = new Vector2(3f, 3f);
	private Vector2 _input = Vector2.zero;
	public GameObject cam;
	private Camera _camCamera;

	private float _targetFOV;
	public float defaultFOV = 60f;
	private float _trueTargetFOV;
	public float maxFOVTweak = 25f;

	public float zDistanceStart = 1.38f;
	public float camPushRayStartLength = 2f;
	private float _camPushRayLength = 0f;
	private float _zDistanceMin = 0.1f;
	private float _zDistanceMax = 10f;
	public int zDistanceInvert = -1;
	public float zDistanceGoal;
	private float _trueZDistanceGoal;
	private int _mvState;
    public float shoulderBumpUp= 0.3f;
    private Quaternion _defaultCamRot;
    private GameStateHandler _gstate;

	public int shoulderState = 0;

	public Vector3 tempLookAtRayPoint = Vector3.zero;


	//********************************************************************************************************
    private void Start()
    {
		Cursor.visible = false; ////////////////////////////////////////
		_targetFOV = defaultFOV;
		_camCamera = GetComponentInChildren<Camera>();
		_zDistanceMax = zDistanceStart;
		zDistanceGoal = zDistanceStart;
		camOffsetStart = cam.transform.localPosition;
        camOffsetStart.y = 0.25f; // sort of a hack. <_<
        camRotStart = cam.transform.localRotation;
        camOffsetGoal = camOffsetStart;
        _defaultCamRot = cam.transform.rotation;
        _baseSensStart = baseSens;
        _gstate = GameObject.Find("GameStateController").GetComponent<GameStateHandler>();

        /*if (!positionTarget)
            positionTarget = GameObject.Find("GP_Spud");
        }*/

    }



    private void Update()
    {
        /*if (gstate.m_currentState == GameStateHandler.GameState.Paused)
        {
            baseSens = 0.5f;
            Debug.Log("wskjuhig");
        }*/
        //else if(gstate.m_currentState == GameStateHandler.GameState.Lost || GameStateHandler.GameState.Won)
        // Sens control TODO port to options menu
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (baseSens >= 2)
            {
                baseSens--;
                Debug.Log("Decreasing sens to " + baseSens);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            if (baseSens <= 15)
            {
                baseSens++;
                Debug.Log("Increasing sens to " + baseSens);
            }
        }

        // ******* Playdead tracking
        _truePositionTarget = positionTarget;
        if (positionTarget.GetComponent<Player>())
        {
            Player m_player = positionTarget.GetComponent<Player>();
            if (m_player.playDead == 1)
            {
                _doTrackObject = true;
                _trueLookTarget = m_player.limbToLookAt;
                //truePositionTarget = m_player.limbToLookAt;
                _distToTLookTarget = Vector3.Distance(cam.transform.position, _trueLookTarget.transform.position);
            }
            else
            {
                _doTrackObject = false;
                _trueLookTarget = lookTarget;
            }
        }

        if (!doCinematicMode)
        {
            if (Input.GetButtonDown("FPerson"))
            {
                doFirstPerson = !doFirstPerson;
            }
            if (!_doTrackObject && !doFirstPerson)
            {
                if (shoulderState == 0)
                    shoulderState = (Input.GetButtonDown("LShoulder")) ? -1 : (Input.GetButtonDown("RShoulder")) ? 1 : shoulderState;
                else shoulderState = (Input.GetButtonDown("RShoulder")) ? 0 : (Input.GetButtonDown("LShoulder")) ? 0 : shoulderState;
            }
            if(doFirstPerson)
            {
                shoulderState = 0;
                zDistanceGoal = -0.4f;
            }
            else { zDistanceGoal = zDistanceStart; }




            //******* PIVOT and CONTAINER POSITION
            _pivotOffset = _truePositionTarget.transform.position;
            _pivotOffset.y = transform.position.y - _truePositionTarget.transform.position.y;
            _pivotOffsetGoal = _pivotOffsetStart;

            _mvState = (int)positionTarget.GetComponent<Player>().MState;
            if (_mvState == 5)
            {
                _pivotOffsetGoal.y = _pivotOffsetStart.y - 0.75f; // how far to drop during crawl
            }

            // Set camera 3D position, with height offset.
            _truePivotGoal.y = Mathf.Lerp(_pivotOffset.y, _pivotOffsetGoal.y, Time.deltaTime * 2);
            //truePivotOffset.x = Mathf.Lerp(pivotOffset.x, pivotOffsetGoal.x, Time.deltaTime);
            //truePivotOffset.z = Mathf.Lerp(pivotOffset.z, pivotOffsetGoal.z, Time.deltaTime);
            transform.position = new Vector3(
                _truePositionTarget.transform.position.x,
                _truePositionTarget.transform.position.y + _truePivotGoal.y,
                _truePositionTarget.transform.position.z
            );




            // ************** ROTATION
            // Set inputs
            if (!_doTrackObject)
            {
                _input.x = Input.GetAxis("HorizontalCam");
                _input.y = Input.GetAxis("VerticalCam");

                // Cut out the vertical input if horizontal input is larger (this is nice, trust me)
                if (Mathf.Abs(Input.GetAxis("Mouse X")) > Mathf.Abs(Input.GetAxis("Mouse Y"))) _axisDamper.y = 0f;
                else _axisDamper.y = 3f;

                // Set rotation deltas
                _rotDelta.x += Mathf.Clamp(_input.x / 10, -1, 1) * rotSens.x * baseSens * _axisDamper.x * 2;
                _rotDelta.y += Mathf.Clamp(_input.y / 10, -1, 1) * rotSens.y * baseSens * _axisDamper.y * 2;

                // Limit vertical angle
                _rotDelta.y = Mathf.Clamp(_rotDelta.y, minVerticalAngle, maxVerticalAngle);

                // Set camera angle. DO THE THING!
                _aim = Quaternion.Euler(-_rotDelta.y, _rotDelta.x, 0);
                transform.rotation = _aim;
            }
        }


        // ********** FIELD-OF-VIEW
        if (_doTrackObject)
        {
            _targetFOV = defaultFOV + 10f - _distToTLookTarget/2;
            Mathf.Clamp(_targetFOV, 15f, defaultFOV + 10f);
        }
        else if(doFirstPerson)
        {
            // Boost FOV for first-person
            _targetFOV = defaultFOV + 10f;
        }
        else _targetFOV = defaultFOV;

        // Slight widening of FOV for high/low angles
        _trueTargetFOV = _targetFOV + Mathf.Clamp(((Mathf.Pow(Mathf.Abs(_rotDelta.y), 2)) / 200) - 8, 0, maxFOVTweak);

        // Slight widening of FOV for crawling
        if (_mvState == 5)
        {
            _trueTargetFOV += 5f;
        }

        // Lerp to FOV target
        _camCamera.fieldOfView = Mathf.Lerp(_camCamera.fieldOfView, _trueTargetFOV, Time.deltaTime * 8);

    }

	//********************************************************************************************************
	private void LateUpdate() {
        Vector3 pos = cam.transform.position;

        if (!_doTrackObject)
        {
            cam.transform.localRotation = camRotStart;

            //********** Z CAMERA OFFSET
            RaycastHit hit;
            Vector3 dir = Vector3.back;
            _trueZDistanceGoal = zDistanceGoal;
            _camPushRayLength = camPushRayStartLength;
            // Make sure the raycast isn't too long or short
            //Mathf.Clamp(camPushRayLength, 0.1f, camPushRayStartLength);

            // Check for intersection
            if (Physics.Raycast(pos, transform.TransformDirection(dir), out hit, _camPushRayLength))
            {
                Debug.DrawRay(pos, transform.TransformDirection(dir) * hit.distance, Color.yellow);

            }
            else
            {
                Debug.DrawRay(pos, transform.TransformDirection(dir) * _camPushRayLength, Color.green);
            }

            // Limit z distance
            Mathf.Clamp(_trueZDistanceGoal, _zDistanceMin, _zDistanceMax);

            // Set z distance. DO THE THINGY
            Vector3 derp = cam.transform.localPosition;
            cam.transform.localPosition = new Vector3(derp.x, derp.y, _trueZDistanceGoal * zDistanceInvert);






            //******** X AND Y CAMERA OFFSET
            _camOffset = cam.transform.localPosition;
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

            _trueCamGoal.y = Mathf.Lerp(_camOffset.y, camOffsetGoal.y, Time.deltaTime * 4);
            _trueCamGoal.x = Mathf.Lerp(_camOffset.x, camOffsetGoal.x, Time.deltaTime * 4);
            cam.transform.localPosition = new Vector3(
                _trueCamGoal.x,
                _trueCamGoal.y,
                cam.transform.localPosition.z
            );

        }

        if(_doTrackObject){
            cam.transform.LookAt(_trueLookTarget.transform.position);
        }
    }
}