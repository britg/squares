using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquareDetector {

	TileCollection tileCollectionReference;
	Player currentPlayer;

	Vector2 squareSize;
	
	Hashtable squares;
	List<Tile>usedTiles;

	int parseDirection = 1;

	public SquareDetector (TileCollection tileCollection, Vector2 _squareSize) {
		tileCollectionReference = tileCollection;
		squareSize = _squareSize;
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
			Square square = DetectSquareAtTile(tile);
			if (square != null) {
				usedTiles.AddRange(square.tiles);
				squares[tile.key] = square;
			}
		}
	}

	bool ValidTile (Tile tile) {
		return tile != null
				&& tile.owner == currentPlayer 
				&& tile.occupant == null
				&& !usedTiles.Contains(tile) 
				&& (tile.state == Tile.State.Half || tile.occupant == currentPlayer);
	}

	Square DetectSquareAtTile (Tile tile) {
		List<Tile> possibleSquare = new List<Tile>();
//		possibleSquare.Add(tile);
		for(int x = 0; x < (int)squareSize.x; x++) {
			for (int y = 0; y < (int)squareSize.y; y++) {
				int testX = (int)tile.pos.x + (parseDirection*x);
				int testY = (int)tile.pos.y + (parseDirection*y);
				possibleSquare.Add(tileCollectionReference.TileAt(testX, testY));
			}
		}

		foreach(Tile check in possibleSquare) {
			if (!ValidTile(check)) {
				return null;
			}
		}

		Square square = new Square(currentPlayer, possibleSquare);

		return square;
	}
	

}
