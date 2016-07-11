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
public class updateHUD : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public GameObject rotationText;
	public GameObject positionText;
	public GameObject dataText;
	public Camera camera;
	public GameObject Reticle;
	public GameObject Mesh;
	public Texture2D dataSet;
	/* ****************  GLOBAL OBJECTS  ****************  */
	Text text_1, text_2, text_3;
	/*********************************************************************
	                             BOOT
	 *********************************************************************/
	void Start () {
		findCanvasStrings();
		toggleHUD();
	}
	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	void Update () {
		if (Input.GetButtonDown("rightShift")) toggleHUD();
		updateRotHUD();
		updatePosHUD();
		updateDataHUD();
	}
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: updateRotHUD
	---------------------------------
	This function calculates and displays the user orientation onto the HUD.
	*/
	void updateRotHUD(){
		int asimuth = ((int)(Camera.main.transform.rotation.eulerAngles.y) + 90) % 360;
		int elevation = (int)(Camera.main.transform.rotation.eulerAngles.x) ;
		if (elevation > 0 && elevation <= 180) elevation = -1 * elevation;
		else if (elevation <= 360 && elevation > 180) elevation = 360 - elevation;
		text_1.text = "Sol 1056\nAZ = " + asimuth + "°\nEL = " + elevation + "°";
	}

	/*
	function: updatePosHUD
	---------------------------------
	This function calculates and displays the user position onto the HUD.
	*/
	void updatePosHUD(){
		string myX = (-1 * (Camera.main.transform.position.x)).ToString("0.00");
		string myY = (Camera.main.transform.position.z).ToString("0.00");
		string myZ = (-1 * (Camera.main.transform.position.y)).ToString("0.00");
		text_2.text = "X = " + myX + "\nY = " + myY + "\nZ = " + myZ;
	}

	/*
	function: updateDataHUD
	---------------------------------
	This function calculates and displays the data froms the surface
	of a mesh onto the HUD.
	*/
	void updateDataHUD(){
		Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);
		text_3.text = "No Mesh Detected";
		RaycastHit hit;
		if (!Physics.Raycast(Camera.main.transform.position, fwd, out hit)) return;
		Reticle.transform.position = hit.point + (Vector3.up * .2f);
		Reticle.transform.LookAt(hit.point - hit.normal);

		float scale = .025f * hit.distance;
		Reticle.transform.localScale = new Vector3(scale, scale, 1);

		string myX = (-1 * (hit.point.x)).ToString("0.00");
		string myY = (hit.point.z).ToString("0.00");
		string myZ = (-1 * (hit.point.y)).ToString("0.00");
		string range = (hit.distance).ToString("0.00");
		string RGBA = "temporary";//getRGBA(hit.textureCoord.x, hit.textureCoord.y);
		string elevation = getElevation(hit.textureCoord.x, hit.textureCoord.y);
		text_3.text = "X = " + myX + "\nY = " + myY + "\nZ = " + myZ + "\nrange = " + range + "\nRGBA :" + RGBA + "\nelevation :" + elevation;
	}

	/**********************************************************************
	                           HELPERS
	*********************************************************************/
	/*
	function: findCanvasStrings
	---------------------------------
	This function gets the child Text components of the canvas. 
	*/
	void findCanvasStrings(){
		text_1 = rotationText.GetComponent<Text>();
		text_2 = positionText.GetComponent<Text>();
		text_3 = dataText.GetComponent<Text>();
	}

	/*
	function: toggleHUD
	---------------------------------
	This function toggles the HUD visablity.
	*/
	void toggleHUD(){
		text_1.enabled = !text_1.enabled;
		text_2.enabled = !text_2.enabled;
		text_3.enabled = !text_3.enabled;
		Reticle.GetComponent<MeshRenderer>().enabled = !Reticle.GetComponent<MeshRenderer>().enabled;
	}	
	/*
	function: getRGBA
	---------------------------------
	This function returns the RGBA value of the visable layer being pointed at
	by the reticule.
	*/
	string getRGBA(float textureX, float textureY){
		int layerIndex  = Mesh.GetComponent<ToggleLayers>().layerIndex;
		Texture2D layer = Mesh.GetComponent<ToggleLayers>().layers[layerIndex].mainTexture as Texture2D;
		int layerX = Mathf.FloorToInt(textureX * layer.width);
		int layerY = Mathf.FloorToInt(textureY * layer.height);
		int pixelR = Mathf.FloorToInt(layer.GetPixel(layerX, layerY).r * 255);
		int pixelG = Mathf.FloorToInt(layer.GetPixel(layerX, layerY).g * 255);
		int pixelB = Mathf.FloorToInt(layer.GetPixel(layerX, layerY).b * 255);
		int pixelA = Mathf.FloorToInt(layer.GetPixel(layerX, layerY).a * 255);
		return pixelR.ToString() + ":" + pixelG.ToString() +  ":" + pixelB.ToString() + ":" + pixelA.ToString();
	}

	/*
	function: getElevation
	---------------------------------
	This function returns the grayscaled hard coded elevation value of this mesh
	*/
	string getElevation(float textureX, float textureY){
		int layerX = Mathf.FloorToInt(textureX * dataSet.width);
		int layerY = Mathf.FloorToInt(textureY * dataSet.height);
		float elevation = Mathf.FloorToInt(dataSet.GetPixel(layerX, layerY).grayscale * 255);
		return elevation.ToString();
	}
}