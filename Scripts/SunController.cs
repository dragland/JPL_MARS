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
	public string time = "SOL 1056 (2015=07-27 044609 UTC)";
	/* ****************  GLOBAL OBJECTS  ****************  */
	private Light sun;
	private XmlDocument xmlDoc;
	private Color tint;
	private float intensity;
	private Vector3 location;
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
		getData();
		updateSunPosition();
		updateSunLight();
	}
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: getData
	---------------------------------
	This function updates the variables with the new values.
	*/
	void getData(){
	}

	/*
	function: updateSunPosition
	---------------------------------
	This function updates the location of the light source with respect
	to the Mars coordinate system.
	*/
	void updateSunPosition(){
		sun.transform.transform.position = location;
		transform.LookAt (new Vector3 (0, 0, 0));
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
			float x = 0, y = 0, z = 0, r = 0, b = 0, g = 0;
			foreach (XmlNode child in children){
				if(child.Name == "x") x = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "y") y = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "z") z  = XmlConvert.ToSingle(child.InnerText);

				if(child.Name == "r") r = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "g") g = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "b") b = XmlConvert.ToSingle(child.InnerText);

				if(child.Name == "i") intensity = XmlConvert.ToSingle(child.InnerText);
			}
			location = new Vector3 (x, y, z);
			tint = new Color(r, g, b);
		}
	}

	/*
	function: sunInit
	---------------------------------
	This function initializes the direction light as the Sun.
	*/
	void sunInit(){
		sun = this.GetComponent<Light>();

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
