using UnityEngine;
using System.Collections;

public class SquareValidator {

	TileCollection tileCollectionReference;
	Player currentPlayer;

	Hashtable squares;
	string currentKey;

	public SquareValidator (TileCollection tileCollection) {
		tileCollectionReference = tileCollection;
	}

	public void Squares (Player _currentPlayer) {
		currentPlayer = _currentPlayer;
		squares = new Hashtable();

		for(int x = 0; x < tileCollectionReference.tiles.GetLength(0); x++) {
			for(int y = 0; y < tileCollectionReference.tiles.GetLength(1); y++) {
				ParseTileAt(x, y);
			}
		}
	}

	void ParseTileAt (int x, int y) {
		Tile tile = tileCollectionReference.TileAt(new Vector2(x, y));
		if (tile.owner == currentPlayer) {
			if (currentKey == null) {
				StartSquareAtTile(tile);
			} else {
			}
		} else {
			currentKey = null;
		}
	}

	void StartSquareAtTile (Tile tile) {
		currentKey = tile.key;
		squares[currentKey] = iTween.Hash("startTile", tile, 
		                                  "workingTile", tile, 
		                                  "dim", 1);
	}

}
