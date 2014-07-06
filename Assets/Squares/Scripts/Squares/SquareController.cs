using UnityEngine;
using System.Collections;

public class SquareController : GameController {

	public Square square;

	public void AttemptUse () {
		Turn currentTurn = turnController.currentTurn;
		Turn squareTurn = square.createdOn;

		if (currentTurn.player != squareTurn.player) {
			Debug.Log ("Not my turn!");
			return;
		}

		if (currentTurn.id - squareTurn.id < squaresController.turnsUntilUsable) {
			Debug.Log ("Need to wait a turn!");
			return;
		}

		Use();
	}


	public void Refresh () {
		transform.position = new Vector3(square.tiles[0].col, square.tiles[0].row, Game.squareZ);
	}

	void Use () {
		Debug.Log ("Using square");
		square.Use(turnController.currentTurn);
		dropQueueControllerForOwner(square.owner).SquareUsed();
		NotificationCenter.PostNotification(this, Notifications.TileStateChange);
		if (square.state == Square.State.Used) {
			Destroy(gameObject);
		}
	}
}
