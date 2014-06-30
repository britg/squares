using UnityEngine;
using System.Collections;

public class TileView : MonoBehaviour {

	public static Hashtable prefabCache = new Hashtable();

	public TilesController tilesController;

	public Tile tile;

	public void Initialize (Tile _tile, TilesController _tilesController) {
		tilesController = _tilesController;
		SetTile(_tile);
		NotificationCenter.AddObserver(this, Notifications.TileStateChange);
	}

	public void SetTile (Tile _tile) {
		tile = _tile;
		SetPosition();
		DrawTileState();
	}

	void SetPosition () {
		transform.position = PositionForTile(tile);
	}

	void OnTileStateChange () {
		DrawTileState();
	}

	void DrawTileState () {
		ClearTile();
		GameObject prefab = PrefabForTileState();

		if (prefab != null) {
			GameObject stateObj = (GameObject)Instantiate(prefab);
			stateObj.transform.parent = transform;
			stateObj.transform.localPosition = new Vector3(0f, 0f, TileView.zIndexForLayer(Tile.Layer.Color));
		}
	}

	void ClearTile () {
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}
	}

	GameObject PrefabForTileState () {
		string color = tile.color;
		string state = tile.state.ToString();
		string resource = color + " Tile " + state;
		GameObject prefab;

		if (TileView.prefabCache[resource] != null) {
			prefab = (GameObject)TileView.prefabCache[resource];
		} else {
			prefab = (GameObject)Resources.Load(resource);
			TileView.prefabCache[resource] = prefab;
		}

		return prefab;
	}

	Vector3 PositionForTile (Tile tile) {
		return PositionForTile(tile, Tile.Layer.Base);
	}
	
	Vector3 PositionForTile (Tile tile, Tile.Layer layer) {
		float y =  tile.row + tile.row * tilesController.tileSpacing;
		float x =  tile.col + tile.col * tilesController.tileSpacing;
		return new Vector3(x, y, TileView.zIndexForLayer(layer));
	}
	
	public static float zIndexForLayer (Tile.Layer layer) {
		float zIndex = 0f;
		switch (layer) {
		case Tile.Layer.Base:
			zIndex = 0f;
			break;
		case Tile.Layer.Color:
			zIndex = -0.01f;
			break;
		case Tile.Layer.Occupant:
			zIndex = -0.02f;
			break;
		}
		return zIndex;
	}

}
