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
using System.IO;
/*********************************************************************
                           CLASS
*********************************************************************/
public class MeshToAssetBuilder : MonoBehaviour {
	/*********************************************************************
	                             BOOT
	*********************************************************************/
	[MenuItem ("Edit/Build Tiles")]
	static void buildTiles(){
		parseMeshes(calcTileName());
		buildAllAssetBundles();
	}
	
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: calcTileName
	---------------------------------
	This function calculates the correct tile name for the imported dataset
	*/
	static string calcTileName(){
		return "tile+0-1";
	}

	/*
	function: parseMeshes
	---------------------------------
	This function parses through each of the new meshes 
	under the Assets/Mesh/ folder.
	*/
	static void parseMeshes(string name){
		foreach (FileInfo obj in importMeshes("Assets/Mesh/")) {
			string path = "Assets" + obj.FullName.Substring(Application.dataPath.Length);
			addToAssetBundle(path, name);
			renderPreview(path);
		}
	}

	/*
	function: buildAllAssetBundles
	---------------------------------
	This function creates all the asset bundles in the project.
	*/
	static void buildAllAssetBundles(){
		AssetDatabase.RemoveUnusedAssetBundleNames();
		BuildPipeline.BuildAssetBundles ("Assets/Bundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
	}

	/**********************************************************************
	                               HELPERS 
	*********************************************************************/
	/*
	function: importMeshes
	---------------------------------
	This function parces through the path,
	recursivly looking for OBJ files.
	*/
	static FileInfo[] importMeshes(string path){
		DirectoryInfo dir = new DirectoryInfo(path);
		return dir.GetFiles("*.obj", SearchOption.AllDirectories);
	}

	/*
	function: addToAssetBundle
	---------------------------------
	This function adds the mesh to its respective assetBundle.
	*/
	static void addToAssetBundle(string path, string name){
		AssetImporter assetImporter = AssetImporter.GetAtPath(path);
		assetImporter.assetBundleName = name;

	}

	/*
	function: renderPreview
	---------------------------------
	This function loads the mesh into the sceen for a visual preview of the tile.
	*/
	static void renderPreview(string path){
		Object mesh = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
		GameObject clone = Instantiate (mesh, Vector3.zero, Quaternion.identity) as GameObject;
		clone.name = mesh.name;
	}
}
