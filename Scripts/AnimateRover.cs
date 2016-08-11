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
public class AnimateRover : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public float animationSpeed = 1f;
	public bool animate = false;
	/* ****************  GLOBAL OBJECTS  ****************  */
	private Animator anim;
	/*********************************************************************
	                             BOOT
	 *********************************************************************/
	void Start () {
		animateInit();
	}
	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	void Update () {
		animateRover();
	}
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: animateRover
	---------------------------------
	This function animates the Rover.
	*/
	void animateRover(){
		anim.speed = 0;
		if (Input.GetButtonDown ("Y_Button")) animate = !animate;
		if (animate) anim.speed = animationSpeed;
	}
	/**********************************************************************
	                            HELPERS
	*********************************************************************/	
	/*
	function: animateInit
	---------------------------------
	This functioniInitializes the animator
	*/
	void animateInit(){
		anim = GetComponent<Animator>();
	}
}
