using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DomainController : GameController {

	public Domain domain;

	void Start () {
		domain = new Domain(player);
		NotificationCenter.AddObserver(this, Notifications.TileOwnershipChange);
	}

	void OnTileOwnershipChange () {
		Debug.Log ("Tile ownership change");
		RefreshDomain();
		DetectSingleSquares(currentPlayer);
		DetectTakeover(currentPlayer);
		NotificationCenter.PostNotification(this, Notifications.TileStateChange);
	}

	void RefreshDomain () {
		int previous = domain.tilesOwned;
		domain.ParseTiles(tileCollection);
		if (previous != domain.tilesOwned) {
			NotificationCenter.PostNotification(this, Notifications.BlockOwnershipChange);
		}
	}

	void DetectSingleSquares (Player player) {
		ResetToHalf(player);
		SingleSquareDetector detector = new SingleSquareDetector(tileCollection);
		Hashtable squares = detector.Squares(player);
		domain.blocksOwned = squares.Count;

		foreach(string key in squares.Keys) {
			List<Tile> squareTiles = (List<Tile>)squares[key];
			foreach (Tile tile in squareTiles) {
				tile.state = Tile.State.Full;
			}
		}

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
			DetectSingleSquares(player);
			DetectSingleSquares(enemyTo(player));
		}
	}

}
