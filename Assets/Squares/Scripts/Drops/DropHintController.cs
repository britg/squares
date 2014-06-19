using UnityEngine;
using System.Collections;

public class DropHintController : GameController {

	Tile previousHoverTile;
	Drop.Rotation previousDropRotation;

	Tile[] previousHintTiles;
	Tile[] hintTiles;

	DropValidator dropValidator;

	bool wasHoldingDrop = false;

	// Use this for initialization
	void Start () {
		InvokeRepeating("PollInput", 0.1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void PollInput () {
		if (!inputController.holdingDrop) {
			if (wasHoldingDrop) {
				wasHoldingDrop = false;
				Unhint();
				NotificationCenter.PostNotification(this, Notifications.TileStateChange);
			}
			return;
		}
		wasHoldingDrop = true;

		bool hoverTileChanged = previousHoverTile != inputController.currentHoverTile;
		bool dropRotationChanged = previousDropRotation != inputController.currentDropController.drop.rotation;

		if (hoverTileChanged) {
			Debug.Log ("Hover tile changed");
		}

		if (dropRotationChanged) {
			Debug.Log ("Drop rotation changed");
		}

		if (hoverTileChanged || dropRotationChanged) {
			Hint();
		}

		previousHoverTile = inputController.currentHoverTile;
		previousDropRotation = inputController.currentDropController.drop.rotation;
	}

	void Hint () {
		Unhint();

		if (inputController.currentHoverTile == null) {
			return;
		}

		dropValidator = new DropValidator(tileCollection);
		Drop drop = inputController.currentDropController.drop;

		if (dropValidator.ValidDrop(drop, inputController.currentHoverTile, player)) {
			Tile[] dropTiles = dropValidator.GetTilesForDrop(drop, inputController.currentHoverTile);
			foreach(Tile tile in dropTiles) {
				tile.Hint(player);
			}
		}

		NotificationCenter.PostNotification(this, Notifications.TileStateChange);
	}

	void Unhint () {
		Tile[] tiles = tileCollection.allTiles();
		foreach (Tile tile in tiles) {
			tile.Unhint();
		}
	}


}
