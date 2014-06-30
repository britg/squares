using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DomainController : GameController {

	public Vector2 squareSize;
	public GameObject squarePrefab;

	void Start () {
		NotificationCenter.AddObserver(this, Notifications.TileOwnershipChange);
	}

	void OnTileOwnershipChange () {
		Debug.Log ("Tile ownership change");
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
		ResetToHalf(player);
		SquareDetector detector = new SquareDetector(tileCollection, squareSize);
		Hashtable squares = detector.Squares(player);
		currentPlayer.domain.blocksOwned = squares.Count;

		foreach(string key in squares.Keys) {
			CreateSquare((List<Tile>)squares[key]);
		}

	}

	void CreateSquare (List<Tile> tiles) {
		Square square = new Square(currentPlayer, tiles);
		GameObject squareObj = (GameObject)Instantiate(squarePrefab);
		SquareController squareController = squareObj.GetComponent<SquareController>();
		squareController.square = square;
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
