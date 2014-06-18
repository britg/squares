using UnityEngine;
using System.Collections;

public class DropValidator {

	TileCollection tileCollectionReference;

	public DropValidator(TileCollection tileCollection) {
		tileCollectionReference = tileCollection;
	}

	public bool ValidDrop (Drop drop, Tile startTile, Player player) {
		Tile[] tiles = GetTilesForDrop(drop, startTile);
		
		// All valid tiles?
		bool oneAdjacent = false;
		foreach (Tile tile in tiles) {
//			Debug.Log ("Checking tile " + tile);
			if (!TileDroppable(tile)) {
				return false;
			}

			if (!oneAdjacent) {
				oneAdjacent = HasOwnerAdjacent(tile, player);
			}
		}
		
		// Adjacent to at least one owned tile?
		return oneAdjacent;
	}
	
	Tile[] GetTilesForDrop (Drop drop, Tile startTile) {
		Tile[] tiles = new Tile[drop.tileCount];
		
		int i = 0;
		foreach (Vector2 offset in drop.offsets) {
			Vector2 coords = startTile.pos + offset;
//			Debug.Log ("Checking coords " + coords);
			Tile tile = tileCollectionReference.TileAt(coords);
			tiles[i] = tile;
			i++;
		}
		
		return tiles;
	}
	
	bool TileDroppable (Tile tile) {
		if (tile == null) {
			return false;
		}

		if (tile.state == Tile.State.Full || tile.state == Tile.State.Half) {
			return false;
		}

		return true;
	}

	bool HasOwnerAdjacent (Tile tile, Player player) {
		Tile[] tiles = tileCollectionReference.AdjacentTiles(tile);
		foreach (Tile adjacent in tiles) {
			if (adjacent == null) {
				continue;
			}

			if (adjacent.owner == player) {
				return true;
			}
		}

		return false;
	}
		
}
