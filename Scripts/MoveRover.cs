/*
   Jet Ppropulsion Laboratory
   Virtual Reality for Mars Rovers | Summer 2016
   Davy Ragland | dragland@stanford.edu
   Victor Ardulov | victorardulov@gmail.com
   Oleg Pariser | Oleg.Pariser@jpl.nasa.gov
*/

/*********************************************************************
                           SETUP
*********************************************************************/
using UnityEngine;
using System.Collections;

/*********************************************************************
                           CLASS
*********************************************************************/
public class MoveRover : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public WheelCollider[] Wheels;
	public GameObject[] WheelMeshes;
	public float MaximumWheelTorque = 70;
	public float MaxReverseTorque   = -50;
	public float TorqueJerk         = 10;
	public float ReverseTorque      = 10;
	public float NaturalResistance  = 20;
	public float SteerSpeed         = 60;
	/* ****************  GLOBAL OBJECTS  ****************  */
	private float currentTorque;
	private float currentSteerAngle;
	private float[] rotationValue = {0f, 0f, 0f, 0f, 0f, 0f};
	private Vector3 startPos;
	/*********************************************************************
	                             BOOT
	*********************************************************************/
	void Start () {
		roverInit();
	}
	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	public void Update(){
		resetCheck();
		getTorque();
		getSteerAngle();
		driveRover();
	}
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: driveRover
	---------------------------------
	This function transforms the rover to the new position.
	*/
	void driveRover(){
		foreach (WheelCollider wheel in Wheels) {
			wheel.motorTorque = currentTorque;
		}
		Wheels [0].steerAngle = currentSteerAngle;
		Wheels [1].steerAngle = currentSteerAngle;
		Wheels [4].steerAngle = -currentSteerAngle;
		Wheels [5].steerAngle = -currentSteerAngle;
		for (int i = 0; i < WheelMeshes.Length; i++) {
			WheelMeshes[i].transform.rotation = Wheels[i].transform.rotation * Quaternion.Euler(rotationValue[i], Wheels[i].steerAngle, 0);
			rotationValue[i] += Wheels[i].rpm * (360 / 60) * Time.deltaTime;
		}
	}
	/**********************************************************************
	                           HELPERS
	*********************************************************************/
	/*
	function: roverInit
	---------------------------------
	This function initializes the rover to the start position.
	*/
	void roverInit(){
		startPos = transform.position;
	}

	/*
	function: resetCheck
    ---------------------------------
    This function returns the user to the start position.
	*/
	void resetCheck(){
		if (Input.GetButtonDown ("Start_Button")) {
			transform.position = startPos;
		}
	}

	/*
	function: getTorque();
	---------------------------------
	This function gets the current torque that will be applied to the rover.
	*/
	void getTorque(){
		if (Input.GetAxis("Menu_Y") > 0) {
			if (currentTorque < MaximumWheelTorque) {
				currentTorque += TorqueJerk * Time.deltaTime * Input.GetAxis("Menu_Y");
			} else {
				currentTorque = MaximumWheelTorque;
			}
		} else if (Input.GetAxis ("Menu_Y") < 0) {
			if(currentTorque > MaxReverseTorque){
				currentTorque -= NaturalResistance * Time.deltaTime * Input.GetAxis("Menu_Y") + ReverseTorque;
			} else {
				currentTorque = MaxReverseTorque;
			}
		} else {
			if(currentTorque > 0.5f){
				currentTorque -= NaturalResistance * Time.deltaTime;
			} else if(currentTorque < -0.5f) {
				currentTorque += NaturalResistance * Time.deltaTime;
			} else{
				currentTorque = 0;
			}
		}
	}

	/*
	function: getSteerAngle();
	---------------------------------
	This function gets the current steer angle that will be applied to the rover.
	*/
	void getSteerAngle(){
		if (Input.GetAxis ("Menu_X") != 0) {
			currentSteerAngle = Input.GetAxis("Menu_X") * SteerSpeed;     
		} else {
			if(currentSteerAngle > 0.5){
				currentSteerAngle -= 20 * Time.deltaTime;
			} else if( currentSteerAngle < -0.5){
				currentSteerAngle += 20 * Time.deltaTime;
			} else {
				currentSteerAngle = 0;
			}
		}
	}

}