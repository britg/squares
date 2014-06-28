using UnityEngine;
using System.Collections;

public class TileCollection {

	public Tile[,] tiles;
	public int Rank { get { return tiles.Rank; } }
	public Tile bottomLeft { get { return BottomLeft(); } }
	public Tile topRight { get { return TopRight(); } }

	public Vector2 blockSize;

	public TileCollection (Vector2 dimensions, Vector2 _blockSize) {
		blockSize = _blockSize;
		tiles = new Tile[(int)dimensions.x, (int)dimensions.y];
		for (int x = 0; x < dimensions.x; x++) {
			for (int y = 0; y < dimensions.y; y++) {
				Tile tile = new Tile(new Vector2(x, y));
				tiles[x, y] = tile;
			}
		}
	}

	public Tile[] allTiles () {
		Tile[] allTiles = new Tile[tiles.Length];
		int i = 0;
		for (int x = 0; x < tiles.GetLength(0); x++) {
			for (int y = 0; y < tiles.GetLength(1); y++) {
				allTiles[i] = tiles[x, y];
				i++;
			}
		}
		return allTiles;
	}

	public Tile TileAt(float x, float y) {
		return TileAt(new Vector2(x, y));
	}

	public Tile TileAt(int x, int y) {
		return TileAt(new Vector2(x, y));
	}

	public Tile TileAt (Vector2 coords) {
		if (OutOfBounds(coords)) {
			return null;
		}

		return tiles[(int)coords.x, (int)coords.y];
	}

	bool OutOfBounds (Vector2 coords) {
		if (coords.x < 0 || coords.x >= tiles.GetLength(0)) {
			return true;
		}

		if (coords.y < 0 || coords.y >= tiles.GetLength(1)) {
			return true;
		}

		return false;
	}

	public Tile[] AdjacentTiles (Tile tile) {
		Tile[] tiles = new Tile[4];
		tiles[0] = TileAt(tile.pos - new Vector2(-1, 0));
		tiles[1] = TileAt(tile.pos - new Vector2(1, 0));
		tiles[2] = TileAt(tile.pos - new Vector2(0, 1));
		tiles[3] = TileAt(tile.pos - new Vector2(0, -1));
		return tiles;
	}

	Tile TopLeft () {
		return tiles[0, tiles.GetLength(1)-1];
	}

	Tile BottomLeft () {
		return tiles[0, 0];
	}

	Tile TopRight () {
		return tiles[tiles.GetLength(0)-1, tiles.GetLength(1)-1];
	}

	Tile BottomRight () {
		return tiles[tiles.GetLength(0)-1, 0];
	}

}
