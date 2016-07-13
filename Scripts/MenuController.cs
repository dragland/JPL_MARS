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
using UnityEngine.UI;

/*********************************************************************
                           CLASS
*********************************************************************/
public class MenuController : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public GameObject titleText;
	public GameObject controlsText;
	public Image background;
	/* ****************  GLOBAL OBJECTS  ****************  */
	private Text text_1;
	private Text text_2;
	/*********************************************************************
	                             BOOT
	 *********************************************************************/
	void Start () {
		findCanvasStrings();
	}
	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	void Update () {
		if (Input.GetButtonDown("Menu_Button")) toggleHUD();
	}
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/**********************************************************************
	                           HELPERS
	 *********************************************************************/	
	/*
	function: findCanvasStrings
	---------------------------------
	This function gets the child Text components of the canvas. 
	*/
	void findCanvasStrings(){
		text_1 = titleText.GetComponent<Text>();
		text_2 = controlsText.GetComponent<Text>();
	}

	/*
	function: toggleHUD
	---------------------------------
	This function toggles the HUD visablity.
	*/
	void toggleHUD(){
		text_1.enabled = !text_1.enabled;
		text_2.enabled = !text_2.enabled;
		background.enabled = !background.enabled;
	}
}