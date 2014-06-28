using UnityEngine;
using System.Collections;

public class DropValidator {

	TileCollection tileCollectionReference;

	public DropValidator(TileCollection tileCollection) {
		tileCollectionReference = tileCollection;
	}

	public bool ValidDrop (Drop drop, Tile startTile, Player player) {
		if (startTile == null) {
			return false;
		}

		return TestDropAtTile (drop, startTile, player);
	}

	public Tile[] ValidDropTiles (Drop drop, Tile startTile, Player player) {
		if (startTile == null) {
			return null;
		}

		// get list of possible start tiles - the inverse of the pattern tiles
		// On the first succcess, return it
		// need to save the tiles chosen.
		bool valid = false;
		Tile[] adjacentTiles = tileCollectionReference.AdjacentTiles(startTile);
		Tile[] testTiles = new Tile[adjacentTiles.Length+1];
		testTiles[0] = startTile;
		for (int i = 1; i <= adjacentTiles.Length; i++) {
			testTiles[i] = adjacentTiles[i-1];
		}

		foreach (Tile tile in testTiles) {
			if (tile == null) {
				continue;
			}
			valid = TestDropAtTile (drop, tile, player);
			if (valid) {
				return GetTilesForDrop(drop, tile);
			}
		}

		return null;
	}

	bool TestDropAtTile (Drop drop, Tile startTile, Player player) {

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
	
	public Tile[] GetTilesForDrop (Drop drop, Tile startTile) {
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

		if (tile.owner != null) {
			return false;
		}

		if (tile.occupant != null) {
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
