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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
/*********************************************************************
                           CLASS
*********************************************************************/
public class ImportData : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public TextAsset xmlFile;
	/* ****************  GLOBAL OBJECTS  ****************  */
	private XmlDocument xmlDoc;
	private LineRenderer lr;
	/*********************************************************************
	                             BOOT
	*********************************************************************/
	void Start () {
		loadData();
	}
	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	void Update () {
		displayData ();
	}
	/**********************************************************************
	                           FUNCTIONS
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
		lr = gameObject.GetComponent<LineRenderer>();
		lr.SetVertexCount (attributes.Count);
		int vertexIndex = 0;
		foreach( XmlNode node in attributes){
			lr.SetColors(Color.green, Color.green);
			lr.SetWidth(0.3f,0.3f);
			XmlNodeList children = node.ChildNodes;
			float x = 0, y = 0, z = 0;
			foreach (XmlNode child in children){
				if(child.Name == "x") x = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "y") y = XmlConvert.ToSingle(child.InnerText);
				if(child.Name == "z") z  = XmlConvert.ToSingle(child.InnerText);
			}
			Vector3 vertexPosition = new Vector3((-1*x), (-1 * z) + 0.4f, y);
			lr.SetPosition(vertexIndex, vertexPosition);
			vertexIndex++;
		}
	}
	/**********************************************************************
	                           HELPERS 
	*********************************************************************/
	/*
	function: displayData
	---------------------------------
	This function displays or removes the loaded datapoints.
	*/
	void displayData(){
		if (Input.GetButton ("L_Bumper")) lr.enabled = !lr.enabled;
	}
}

