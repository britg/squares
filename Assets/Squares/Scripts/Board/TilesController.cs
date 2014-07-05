using UnityEngine;
using System.Collections;

public class TilesController : GameController {

	public GameObject tilePrefab;
	public Vector2 baseBlockDimensions;
	public Vector2 boardDimensions;
	public Vector2 completeDimensions { get { return Vector2.Scale(boardDimensions, baseBlockDimensions); } }
	public float tileWidth = 1f;
	public float tileSpacing;

	new public TileCollection tileCollection;

	// Use this for initialization
	void Start () {
		StartTiles();
		CreateBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartTiles () {
		tileCollection = new TileCollection(completeDimensions, baseBlockDimensions);

		Tile playerOneTile = tileCollection.TileAt(1, 1);
		playerOneTile.SetPlayer(player);
		player.homeTile = playerOneTile;
		player.domain = new Domain();

		Tile playerTwoTile = tileCollection.TileAt(boardDimensions.x-2, boardDimensions.y-2);
		playerTwoTile.SetPlayer(opponent);
		opponent.homeTile = playerTwoTile;
		opponent.domain = new Domain();
	}

	void CreateBoard () {
		int cols = tileCollection.tiles.GetLength(0);
		int rows = tileCollection.tiles.GetLength(1);
		for (int col = 0; col < cols; col++) {
			for (int row = 0; row < rows; row++) {
				Tile tile = tileCollection.tiles[col, row];
				CreateTile(tile);
			}
		}

	}

	void CreateTile (Tile tile) {
		GameObject tileObj = (GameObject)Instantiate(tilePrefab);
		tileObj.transform.parent = transform;
		tileObj.GetComponent<TileView>().Initialize(tile, this);
	}

}
