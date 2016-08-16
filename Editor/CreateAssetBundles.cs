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
using UnityEditor;
/*********************************************************************
                           CLASS
*********************************************************************/
public class CreateAssetBundles : MonoBehaviour {
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: buildAllAssetBundles
	---------------------------------
	This function creates all the asset bundles in the project.
	*/
	[MenuItem ("Assets/Build AssetBundles")]
	static void buildAllAssetBundles(){
		BuildPipeline.BuildAssetBundles ("Assets/Bundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
	}

	/*
	function: getNames
	---------------------------------
	This function returns all the asset bundles in the project.
	*/
	[MenuItem ("Assets/Get AssetBundle names")]
	static void getNames(){
		var names = AssetDatabase.GetAllAssetBundleNames();
		foreach (var name in names) Debug.Log ("AssetBundle: " + name);
	}
}
