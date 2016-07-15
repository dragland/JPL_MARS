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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

/*********************************************************************
                           CLASS
*********************************************************************/
public class SunController : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public TextAsset xmlFile;
	/* ****************  GLOBAL OBJECTS  ****************  */
	private Light sun;
	private XmlDocument xmlDoc;
	private Color tint;
	private float intensity;
	private Vector3 rotation;
	/*********************************************************************
	                             BOOT
	*********************************************************************/
	void Start () {
		sunInit();
		loadData();
	}

	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	void Update () {
		resetCheck();
		updateSunRotation();
		updateSunLight();
	}
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: updateSunRotation
	---------------------------------
	This function updates the rotation of the light source with respect
	to the Mars coordinate system.
	*/
	void updateSunRotation(){
		sun.transform.eulerAngles = rotation;
	}

	/*
	function: updateSunLight
	---------------------------------
	This function updates the light of the light source with respect
	to the color and intensity of the sunlight.
	*/
	void updateSunLight(){
		sun.color = tint;
		sun.intensity = intensity;
	}
		
	/**********************************************************************
	                              HELPERS 
	*********************************************************************/
	/*
	function: sunInit
	---------------------------------
	This function initializes the direction light as the Sun.
	*/
	void sunInit(){
		sun = this.GetComponent<Light>();
	}

	/*
	function: loadData
	---------------------------------
	This function loads the data from an external file.
	*/
	void loadData(){
		xmlDoc  = new XmlDocument();
		xmlDoc.LoadXml (xmlFile.text);
		XmlNodeList attributes = xmlDoc.GetElementsByTagName ("annotation");
		foreach( XmlNode node in attributes){
			XmlNodeList children = node.ChildNodes;
			float r = 0, b = 0, g = 0, SOLAR_AZIMUTH = 0, SOLAR_ELEVATION = 0;
			foreach (XmlNode child in children){
				if(child.Name == "RED") r = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "GREEN") g = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "BLUE") b = XmlConvert.ToSingle(child.InnerText);

				if(child.Name == "INTENSITY") intensity = XmlConvert.ToSingle(child.InnerText);

				if(child.Name == "SOLAR_AZIMUTH") SOLAR_AZIMUTH = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "SOLAR_ELEVATION") SOLAR_ELEVATION = XmlConvert.ToSingle(child.InnerText);
			}
			rotation = new Vector3 (SOLAR_ELEVATION, SOLAR_AZIMUTH + 90, 0);
			tint = new Color(r, g, b);
		}
	}

	/*
	function: resetCheck
	---------------------------------
	This function returns the user to the start position.
	*/
	void resetCheck(){
		if (Input.GetButtonDown ("Start_Button")) {
		}
	}
}