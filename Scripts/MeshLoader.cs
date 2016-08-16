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
public class MeshLoader : MonoBehaviour {
	/* ****************  EDITABLE CONSTANTS  ****************  */
	public string URL = "http://137.78.208.219/bundles/";
	public GameObject player;
	public GameObject startMesh;
	public int tileSize = 50000;
	public int tileArraySize = 4;
	/* ****************  GLOBAL OBJECTS  ****************  */
	public struct tile_t{
		public Vector2 address;
		public GameObject mesh;
	}
	tile_t[] oldTiles;
	tile_t[] newTiles;
	/*********************************************************************
	                             BOOT
	*********************************************************************/
	void Start(){
		oldTiles = new tile_t[tileArraySize];
		newTiles = new tile_t[tileArraySize];
		oldTiles[0].address = getCurrTile(player.transform.position);
		oldTiles[0].mesh = startMesh;
	}
	/*********************************************************************
	                             MAIN
	 *********************************************************************/
	void Update(){
		getTiles();
		renderTiles();
		deleteTiles();
		cleanTiles();
	}
	/**********************************************************************
	                           FUNCTIONS
	 *********************************************************************/
	/*
	function: getTiles
	---------------------------------
	This function get the tiles to be rendered.
	*/
	void getTiles(){
		Vector3 currPosition = player.transform.position;
		Vector2 currTile = getCurrTile(currPosition);
		int currSection = getCurrSection(currPosition, currTile);
		calculateNewTiles(currTile, currSection);
	}

	/*
	function: renderTiles
	---------------------------------
	This function renders the tiles in the newTile array.
	*/
	void renderTiles(){
		for (int i = 0; i < tileArraySize; i++) {
			if(isNotIn(i, newTiles, oldTiles)){
				string URLPath = calculateTilePath(newTiles[i].address);
				StartCoroutine(loadBundle(i, URLPath, newTiles[i].address));
			}
		}
	}

	/*
	function: deleteTiles
	---------------------------------
	This function deletes the tiles in the oldTile array.
	*/
	void deleteTiles(){
		for (int i = 0; i < tileArraySize; i++) {
			if(isNotIn(i, oldTiles, newTiles) || ((i != 0) && (oldTiles[i].address == oldTiles[0].address))){
				Destroy(oldTiles[i].mesh);
			}
		}
	}

	/*
	function: cleanTiles
	---------------------------------
	This function cleans up the newTiles and oldTile arrays for the next iteration.
	*/
	void cleanTiles(){
		for (int i = 0; i < tileArraySize; i++) {
			oldTiles[i].address = newTiles[i].address;
			oldTiles[i].mesh = newTiles[i].mesh;
		}
	}

	/**********************************************************************
	                               HELPERS 
	*********************************************************************/
	/*
	function: getCurrTile
	---------------------------------
	This function calculates the current tile index.
	*/
	Vector2 getCurrTile(Vector3 currPosition){
		Vector2 tile;
		tile.x = (int)(currPosition.x / (tileSize / 2));
		tile.y = (int)(currPosition.z / (tileSize / 2)); 
		return tile;
	}

	/*
	function: getCurrSection
	---------------------------------
	This function calculates the current section of the tile.
	*/
	int getCurrSection(Vector3 currPosition, Vector2 currTile){
		int x = (int)currPosition.x - (int)(currTile.x * tileSize);
		int y = (int)currPosition.z - (int)(currTile.y * tileSize);
		int offset = tileSize / 6;
		if(x > offset && y > offset) return 3;
		if(x > offset && y < (offset * -1)) return 9;
		if(x < (offset * -1) && y > offset) return 1;
		if(x < (offset * -1) && y < (offset * -1)) return 7;
		if(y > offset ) return 2;
		if(x > offset) return 6;
		if(y < (offset * -1)) return 8;
		if(x < (offset * -1)) return 4;
		return 5;
	}	

	/*
	function: calculateNewTiles
	---------------------------------
	This function calculates the new tiles to be rendered.
	*/
	void calculateNewTiles(Vector2 currTile, int currSection){
		newTiles[0].address = currTile;
		newTiles[1].address = currTile;
		newTiles[2].address = currTile;
		newTiles[3].address = currTile;
		if (currSection == 2) {
			newTiles[1].address.y += 1;
		}
		if (currSection == 4) {
			newTiles[1].address.x -= 1;
		}
		if (currSection == 6) {
			newTiles[1].address.x += 1;
		}
		if (currSection == 8) {
			newTiles[1].address.y -= 1;
		}
		if (currSection == 1) {
			newTiles[1].address.x -= 1;
			newTiles[2].address.x -= 1;
			newTiles[2].address.y += 1;
			newTiles[3].address.y += 1;
		}
		if (currSection == 3) {
			newTiles[1].address.x += 1;
			newTiles[2].address.x += 1;
			newTiles[2].address.y += 1;
			newTiles[3].address.y += 1;
		}
		if (currSection == 7) {
			newTiles[1].address.x -= 1;
			newTiles[2].address.x -= 1;
			newTiles[2].address.y -= 1;
			newTiles[3].address.y -= 1;
		}
		if (currSection == 9) {
			newTiles[1].address.x += 1;
			newTiles[2].address.x += 1;
			newTiles[2].address.y -= 1;
			newTiles[3].address.y -= 1;
		}
	}
		
	/*
	function: isNotIn
	---------------------------------
	This function returns true if an indexed eliment of the first array is not in the 2nd array.
	*/
	bool isNotIn(int index, tile_t[] array_1, tile_t[] array_2){
		for(int i = 0; i < tileArraySize; i++){
			if (array_1[index].address == array_2[i].address) return false;
		}
		return true;
	}

	/*
	function: calculateTilePath
	---------------------------------
	This function calculates the tile name to be added to the URL path.
	*/
	string calculateTilePath(Vector2 tile){
		string path = URL + "tile";
		int x = (int)tile.x;
		int y = (int)tile.y;
		path = appendIntToString(x, path);
		path = appendIntToString(y, path);
		return path;
	}	

	/*
	function: appendIntToString
	---------------------------------
	This function calculates the string from an int.
	*/
	string appendIntToString(int i, string path){
		if (i > -1) path += '+';
		path += i.ToString();
		return path;
	}

	/*
	function: loadBundle
	---------------------------------
	This function loads the asset bundle from an outside source.
	*/
	public IEnumerator loadBundle(int index, string path, Vector2 tile){
		WWW www = new WWW(path);
		yield return www;
		AssetBundle bundle = www.assetBundle;
		www.Dispose();
		StartCoroutine(renderBundle(index, tile, bundle));
	}

	/*
	function: renderBundle
	---------------------------------
	This function renders the asset bundle from an outside source.
	*/
	public IEnumerator renderBundle(int index, Vector2 tile, AssetBundle bundle){
		foreach (var asset in bundle.LoadAllAssets()) {
			GameObject newMesh = (GameObject)Instantiate(asset, new Vector3 (tile.x * tileSize, 0, tile.y * tileSize), new Quaternion());
			yield return newMesh;
			newMesh.transform.parent = gameObject.transform;
			newTiles[index].mesh = newMesh;
		}
		bundle.Unload(false);
	}
}