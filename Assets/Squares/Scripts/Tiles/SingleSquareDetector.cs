using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SingleSquareDetector {

	TileCollection tileCollectionReference;
	Player currentPlayer;
	
	Hashtable squares;
	List<Tile>usedTiles;

	int parseDirection = 1;

	public SingleSquareDetector (TileCollection tileCollection) {
		tileCollectionReference = tileCollection;
	}
	
	public Hashtable Squares (Player _currentPlayer) {
		currentPlayer = _currentPlayer;
		squares = new Hashtable();
		usedTiles = new List<Tile>();

		ParseTiles();
		
		return squares;
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
		if (ValidTile(tile)) {
			List<Tile> square = DetectSquareAtTile(tile);
			if (square != null) {
				usedTiles.AddRange(square);
				squares[tile.key] = square;
			}
		}
	}

	bool ValidTile (Tile tile) {
		return tile != null
				&& tile.owner == currentPlayer 
				&& !usedTiles.Contains(tile) 
				&& tile.state == Tile.State.Half;
	}

	List<Tile> DetectSquareAtTile (Tile tile) {
		List<Tile> possibleSquare = new List<Tile>();
		possibleSquare.Add(tile);
		possibleSquare.Add(tileCollectionReference.TileAt(tile.pos.x, tile.pos.y+parseDirection));
		possibleSquare.Add(tileCollectionReference.TileAt(tile.pos.x+parseDirection, tile.pos.y+parseDirection));
		possibleSquare.Add(tileCollectionReference.TileAt(tile.pos.x+parseDirection, tile.pos.y));

		foreach(Tile check in possibleSquare) {
			if (!ValidTile(check)) {
				return null;
			}
		}

		return possibleSquare;
	}
	

}
