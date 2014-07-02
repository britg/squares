using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DomainController : GameController {

	public Vector2 squareSize;
	public GameObject squarePrefab;

	List<GameObject> squareObjs;

	void Start () {
		squareObjs = new List<GameObject>();
		NotificationCenter.AddObserver(this, Notifications.TileOwnershipChange);
	}

	void OnTileOwnershipChange () {
		RefreshDomain();
		DetectSquares(currentPlayer);
		DetectTakeover(currentPlayer);
		NotificationCenter.PostNotification(this, Notifications.TileStateChange);
	}

	void RefreshDomain () {
		int previous = currentPlayer.domain.tilesOwned;
		currentPlayer.domain.ParseTiles(tileCollection);
		if (previous != currentPlayer.domain.tilesOwned) {
			NotificationCenter.PostNotification(this, Notifications.BlockOwnershipChange);
		}
	}

	void DetectSquares (Player player) {
		ResetSquares();
		ResetToHalf(player);
		SquareDetector detector = new SquareDetector(tileCollection, squareSize);
		Hashtable squares = detector.Squares(player);
		currentPlayer.domain.blocksOwned = squares.Count;

		foreach(string key in squares.Keys) {
			GameObject newSquare = CreateSquare((List<Tile>)squares[key]);
			squareObjs.Add(newSquare);
		}

	}

	void ResetSquares () {
		foreach (GameObject square in squareObjs) {
			RemoveSquare(square);
		}
	}

	void RemoveSquare (GameObject square) {
		Destroy(square);
	}

	GameObject CreateSquare (List<Tile> tiles) {
		Square square = new Square(currentPlayer, tiles);
		GameObject squareObj = (GameObject)Instantiate(squarePrefab);
		SquareController squareController = squareObj.GetComponent<SquareController>();
		squareController.square = square;
		squareController.Refresh();

		return squareObj;
	}

	void ResetToHalf (Player player) {
		foreach (Tile tile in tileCollection.allTiles()) {
			if (tile.owner == player && tile.occupant == null && tile.state == Tile.State.Full) {
				tile.state = Tile.State.Half;
			}
		}
	}

	void DetectTakeover (Player player) {
		TakeOverDetector detector = new TakeOverDetector(tileCollection);
		List<Tile> tiles = detector.TakeOverTiles(player);
		bool tookOverAtLeastOne = false;
		foreach(Tile tile in tiles) {
			if (tile.owner != player) {
				tile.RemoveOwner();
				tookOverAtLeastOne = true;
			}
		}

		if (tookOverAtLeastOne) {
			Debug.Log ("Took over one, detecting squares");
			DetectSquares(player);
			DetectSquares(enemyTo(player));
		}
	}

}
