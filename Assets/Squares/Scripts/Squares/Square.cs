using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Square {

	public enum State {
		Full,
		Half,
		Used
	}

	public Player owner;
	public List<Tile> tiles;
	public Square.State state;
	public Turn createdOn;

	public string identifier { get { return Identifier(); } }

	public Square (Player _owner, List<Tile> _tiles) {
		owner = _owner;
		tiles = _tiles;
		state = Square.State.Full;
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

	public void Use (Turn onTurn) {
		owner.ReclaimSquare(this);
		createdOn = onTurn;
		if (state == Square.State.Full) {
			state = Square.State.Half;
		} else {
			if (state == Square.State.Half) {
				state = Square.State.Used;
				SetTileStateEmpty();
			}
		}
//		SetTileStateHalf();
	}

}
