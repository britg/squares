using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Square {

	public Player owner;
	public List<Tile> tiles;
	public Turn createdOn;

	public string identifier { get { return Identifier(); } }

	public Square (Player _owner, List<Tile> _tiles) {
		owner = _owner;
		tiles = _tiles;
		SetTileStateFull();
	}

	void SetTileStateFull () {
		foreach (Tile tile in tiles) {
			tile.state = Tile.State.Full;
		}
	}

	void SetTileStateHalf () {
		foreach (Tile tile in tiles) {
			tile.state = Tile.State.Half;
		}
	}

	void SetTileStateEmpty () {
		foreach (Tile tile in tiles) {
			tile.RemoveOwner();
		}
	}

	string Identifier () {
		return owner.id.ToString() + tiles[0].key;
	}

	public void Use () {
		owner.ReclaimSquare(this);
//		SetTileStateHalf();
	}

}
