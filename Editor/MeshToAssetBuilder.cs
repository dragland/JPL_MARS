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
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: buildTiles
	---------------------------------
	This function builds Unity tiles from the meshes imported.
	*/
	[MenuItem ("Edit/Build Tiles")]
	static void buildTiles(){
		foreach (FileInfo obj in importMeshes("Assets/Mesh/")) {
			addToAssetBundle(obj);
		}
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
	static void addToAssetBundle(FileInfo obj){
		Object mesh = AssetDatabase.LoadAssetAtPath("Assets" + obj.FullName.Substring(Application.dataPath.Length), typeof(GameObject));
		GameObject clone = Instantiate (mesh, Vector3.zero, Quaternion.identity) as GameObject;
		clone.name = mesh.name;
		AssetDatabase.CreateAsset(clone, "Assets/Bundles/tile+0-1");
		//AssetDatabase.RemoveUnusedAssetBundleNames();
	}
}
