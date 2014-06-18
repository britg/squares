using UnityEngine;
using System.Collections;

public class TileView : MonoBehaviour {

	public TilesController tilesController;

	Tile tile;

	public void Initialize (Tile _tile, TilesController _tilesController) {
		tilesController = _tilesController;
		SetTile(_tile);
	}

	public void SetTile (Tile _tile) {
		tile = _tile;
		SetPosition();
		DrawTileState();
	}

	void SetPosition () {
		transform.position = PositionForTile(tile);
	}

	void DrawTileState () {
		if (tile.unOwned) {
			ClearTile();
		} else {
			GameObject prefab = PrefabForTileState();
			GameObject stateObj = (GameObject)Instantiate(prefab);
			stateObj.transform.parent = transform;
			stateObj.transform.localPosition = Vector3.zero;
		}
	}

	void ClearTile () {
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}

	GameObject PrefabForTileState () {
		string color = tile.color.ToString();
		string state = tile.state.ToString();
		string resource = color + " Tile " + state;
		GameObject prefab = (GameObject)Resources.Load(resource);
		return prefab;
	}

	Vector3 PositionForTile (Tile tile) {
		return PositionForTile(tile, Tile.Layer.Base);
	}
	
	Vector3 PositionForTile (Tile tile, Tile.Layer layer) {
		float x =  tile.row + tile.row * tilesController.tileSpacing;
		float y =  tile.col + tile.col * tilesController.tileSpacing;
		return new Vector3(x, y, TileView.zIndexForLayer(layer));
	}
	
	public static float zIndexForLayer (Tile.Layer layer) {
		float zIndex = 0f;
		switch (layer) {
		case Tile.Layer.Base:
			zIndex = 0f;
			break;
		case Tile.Layer.Color:
			zIndex = 0.01f;
			break;
		case Tile.Layer.Occupant:
			zIndex = 0.02f;
			break;
		}
		return zIndex;
	}

}
