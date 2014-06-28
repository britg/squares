using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TakeOverDetector {

	TileCollection tileCollectionReference;
	Player currentPlayer;

	List<Tile> takeOverTiles;
	
	int parseDirection = 1;
	
	public TakeOverDetector (TileCollection tileCollection) {
		tileCollectionReference = tileCollection;
	}
	
	public List<Tile> TakeOverTiles (Player _currentPlayer) {
		currentPlayer = _currentPlayer;
		takeOverTiles = new List<Tile>();
		
		ParseTiles();
		
		return takeOverTiles;
	}
	
	void ParseTiles () {
		parseDirection = (currentPlayer.homeTile == tileCollectionReference.bottomLeft ? 1 : -1);
		
		if (parseDirection == 1) {
			ParseFromBottomLeft();
		} else {
			ParseFromTopRight();
		}
	}
	
	void ParseFromBottomLeft () {
		for(int x = 0; x < tileCollectionReference.tiles.GetLength(0); x++) {
			for(int y = 0; y < tileCollectionReference.tiles.GetLength(1); y++) {
				ParseTileAt(x, y);
			}
		}
	}
	
	void ParseFromTopRight () {
		for(int x = tileCollectionReference.tiles.GetLength(0)-1; x >= 0; x--) {
			for(int y = tileCollectionReference.tiles.GetLength(1)-1; y >= 0; y--) {
				ParseTileAt(x, y);
			}
		}
	}
	
	void ParseTileAt (int x, int y) {
		Tile tile = tileCollectionReference.TileAt(new Vector2(x, y));
		if (ValidStartTile(tile)) {
			List<Tile> square = DetectTakeOverAtTile(tile);
			if (square != null) {
				takeOverTiles.AddRange(square);
			}
		}
	}

	bool ValidStartTile (Tile tile) {
		return tile != null
				&& tile.owner == currentPlayer;
	}

	List<Tile> DetectTakeOverAtTile (Tile tile) {
		List<Tile> possibleTakeover = new List<Tile>();
		possibleTakeover.Add(tile);
		possibleTakeover.Add(tileCollectionReference.TileAt(tile.pos.x, tile.pos.y+parseDirection));
		possibleTakeover.Add(tileCollectionReference.TileAt(tile.pos.x+parseDirection, tile.pos.y+parseDirection));
		possibleTakeover.Add(tileCollectionReference.TileAt(tile.pos.x+parseDirection, tile.pos.y));

		int owned = 0;
		int enemyOwned = 0;
		foreach(Tile check in possibleTakeover) {
			if (check == null || check.owner == null) {
				return null;
			}

			if (check.owner == currentPlayer) {
				owned++;
			} else {
				enemyOwned++;
			}
		}

		if (owned >= 2 && enemyOwned >= 1) {
			return possibleTakeover;
		} else {
			return null;
		}
	}
}
