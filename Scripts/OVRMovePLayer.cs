/*
   Jet Propulsion Laboratory
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
public class OVRMovePLayer : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public float transformVelocity = 0.1f;
	public float rotateVelocity    = 1.0f;
	public int hoverHeight         = 1;
	/* ****************  GLOBAL OBJECTS  ****************  */
	private Vector3 startPos;
	private Quaternion startRot;
	/*********************************************************************
	                             BOOT
	 *********************************************************************/
	void Start () {
		playerInit();
	}

	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	void Update () {
		resetCheck();
		if (Input.GetButton ("X_Button")) rotateAround(); 
		else {
			transformPlayer(getTravelSpeed());
			rotatePlayer();
		}
	}

	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: transformPlayer
    ---------------------------------
    This function updates the position of the player based 
    on the inputs from the user.
	*/
	void transformPlayer(int fastMode){
		transform.position += Camera.main.transform.TransformDirection(Vector3.forward) * transformVelocity * fastMode * Input.GetAxis("Vertical_move"); 
		transform.position += Camera.main.transform.TransformDirection(Vector3.left) * transformVelocity * fastMode * Input.GetAxis("Horizontal_move");
		transform.position += Vector3.up * transformVelocity * fastMode * Input.GetAxis("Altitude_move");
		float offset = getHeight();
		if (offset != 0 && offset < hoverHeight) {
			float surfaceHeight = transform.position.y - offset;
			transform.position = Vector3.Scale(transform.position, new Vector3(1, 0, 1));
			transform.position += Vector3.up * (surfaceHeight + hoverHeight);
		}
	}

	/*
    function: rotatePlayer
    ---------------------------------
    This function updates the orientation of the player based 
    on the inputs from the user.
    */
	void rotatePlayer(){
		transform.Rotate(Vector3.down, rotateVelocity * Input.GetAxis("Horizontal_turn"), Space.World);
		transform.Rotate(Camera.main.transform.TransformDirection(Vector3.left), rotateVelocity * Input.GetAxis("Vertical_turn"), Space.World);
	}

	/**********************************************************************
	                           HELPERS
	 *********************************************************************/
	/*
	function: playerInit
    ---------------------------------
    This function initializes the user to the start position.
	*/
	void playerInit(){
		startPos = Camera.main.transform.position;
		startRot = Camera.main.transform.rotation;
		transform.position = startPos;
		transform.rotation = startRot;
	}

	/*
	function: resetCheck
    ---------------------------------
    This function returns the user to the start position.
	*/
	void resetCheck(){
		if (Input.GetButtonDown ("Start_Button")) {
			Debug.Log ("Going to " + startPos);
			transform.position = startPos;
			transform.rotation = startRot;
		}
	}

	/*
	function: getTravelSpeed
	---------------------------------
	This function returns either a 1 or a 5, which is then
	multiplied to the travel transformVelocity, allowing the user to
	switch between two speeds.
	*/
	int getTravelSpeed(){
		if (Input.GetButton ("leftShift"))
			return 50;
		return 1;
	}

	/*
	function: rotateAround
	---------------------------------
	This function rotates the camera around the reticle, while 
	maintaining the global coordinate system.
	*/
	void rotateAround(){
		Vector3 fwd = Camera.main.transform.TransformDirection (Vector3.forward);
		RaycastHit hit;
		if (!Physics.Raycast (Camera.main.transform.position, fwd, out hit)) return;
		transform.RotateAround (hit.point, Vector3.up, Input.GetAxis("Horizontal_move"));
	}

	/*
	function: getHeight
	---------------------------------
	This function returns The Z offset of the user from the surface of 
	the mesh underneath.
	*/
	float getHeight(){
		RaycastHit hit;
		if (!Physics.Raycast(Camera.main.transform.position, Vector3.down, out hit)) return 0.0f;
		return hit.distance;
	}
}