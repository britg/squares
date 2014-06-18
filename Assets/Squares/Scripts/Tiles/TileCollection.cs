using UnityEngine;
using System.Collections;

public class TileCollection {

	public Tile[,] tiles;
	public int Rank { get { return tiles.Rank; } }
	public Tile bottomLeft { get { return BottomLeft(); } }
	public Tile topRight { get { return TopRight(); } }

	public TileCollection (Vector2 dimensions) {
		tiles = new Tile[(int)dimensions.x, (int)dimensions.y];
		for (int x = 0; x < dimensions.x; x++) {
			for (int y = 0; y < dimensions.y; y++) {
				Tile tile = new Tile(new Vector2(x, y));
				tiles[x, y] = tile;
			}
		}
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
