using UnityEngine;
using System.Collections;

public class Tile {

	public enum Color {
		Blue,
		Green
	}

	public enum State {
		Empty,
		Half,
		Full,
		Hinted
	}

	public enum Layer {
		Base,
		Color,
		Occupant
	}

	public Player owner;
	public Tile.State state = Tile.State.Empty;
	public Vector2 pos;
	public ITileOccupant occupant;

	public int row { get { return (int)pos.y; } }
	public int col { get { return (int)pos.x; } }

	public Tile (Vector2 _pos) {
		pos = _pos;
	}

	public bool isEmpty {
		get {
			return unOwned && unOccupied;
		}
	}

	public bool unOwned {
		get {
			return owner == null;
		}
	}

	public bool unOccupied {
		get {
			return occupant == null;
		}
	}

	public string color {
		get {
			if (owner != null) {
				return owner.color.ToString();
			} else {
				return null;
			}
		}
	}

	public void SetPlayer (Player player) {
		owner = player;
		occupant = player;
		state = Tile.State.Full;
	}

	public override string ToString ()
	{
		return string.Format ("[Tile: row={0}, col={1}, isEmpty={2}, unOwned={3}, unOccupied={4}, color={5}]", row, col, isEmpty, unOwned, unOccupied, color);
	}
}
