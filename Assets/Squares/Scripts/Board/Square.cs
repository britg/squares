using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Square {

	public Player owner;
	public List<Tile> tiles;

	public Square (Player owner, List<Tile> _tiles) {
		tiles = _tiles;
		SetTileStateFull();
	}

	void SetTileStateFull () {
		foreach (Tile tile in tiles) {
			tile.state = Tile.State.Full;
		}
	}

}
