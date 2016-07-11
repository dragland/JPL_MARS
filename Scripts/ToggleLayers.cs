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
public class ToggleLayers : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public int layerIndex = 0;
	public Material[] layers;
	public Renderer[] rend;
	public Material blank;
	/* ****************  GLOBAL OBJECTS  ****************  */
	Material[] layersTempCopy;
	/*********************************************************************
	                             BOOT
	*********************************************************************/
	void Start () {
		layersInit();
	}

	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	void Update () {
		resetCheck();
		setLayerIndex();
		manipulateLayer();
		renderLayer();
	}

	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: renderLayer
	---------------------------------
	This function renders the selected dataset layer with
	the correct options.
	*/
	void renderLayer(){
		layersTempCopy[layers.Length - 1] = layers[layerIndex];
		for (int i = 0; i < rend.Length; i++) {
			rend[i].materials = layersTempCopy;
		}
	}

	/**********************************************************************
	                              HELPERS 
	*********************************************************************/
	/*
	function: resetCheck
	---------------------------------
	This function returns the user to the start position.
	*/
	void resetCheck(){
		if (Input.GetButtonDown ("Start_Button")) {
		}
	}

	/*
	function: layersInit
	---------------------------------
	This function dynamicaly allocates the modified layer array, because the .materials
	function returns a copy, so the entire materials array must be overwriten to change
	the layers.
	*/
	void layersInit(){
		layersTempCopy = new Material[layers.Length];
		for(int i = 0; i < layersTempCopy.Length; i++){
			if (layersTempCopy[i] == null) layersTempCopy[i] = blank;
		}
	}

	/*
	function: setLayerIndex
	---------------------------------
	This function sets the dataset layer index that is currently 
	being manipulated.
	*/
	void setLayerIndex(){
		if (Input.GetButtonDown ("R_Bumper")) {
			layerIndex++;
			if (layerIndex >= layers.Length) layerIndex = 0;
		}
		if (Input.GetButtonDown ("L_Bumper")) {
			layerIndex--;
			if (layerIndex < 0) layerIndex = layers.Length - 1;
		}
	}

	/*
	function: manipulateLayer
	---------------------------------
	This function manipulates the dataset layer that is currently selected.
	*/
	void manipulateLayer(){
	}
}

