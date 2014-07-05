using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquaresController : GameController {

	public Vector2 squareSize;
	public GameObject squarePrefab;
	public int turnsUntilUsable = 1;

	List<GameObject> squareObjs;

	void Start () {
		squareObjs = new List<GameObject>();
		NotificationCenter.AddObserver(this, Notifications.TileOwnershipChange);
	}

	void OnTileOwnershipChange () {
		bool squaresDetected = DetectSquares(currentPlayer);
		if (squaresDetected) {
			NotificationCenter.PostNotification(this, Notifications.TileStateChange);
		}
	}

	bool DetectSquares (Player player) {
		bool newSquares = false;
		SquareDetector detector = new SquareDetector(tileCollection, squareSize);
		Hashtable squares = detector.Squares(player);

		foreach(string key in squares.Keys) {
			newSquares = true;
			GameObject newSquare = CreateSquare((Square)squares[key]);
			squareObjs.Add(newSquare);
		}

		return newSquares;
	}

	void ResetSquares () {
		foreach (GameObject square in squareObjs) {
			RemoveSquare(square);
		}
	}

	void RemoveSquare (GameObject square) {
		Destroy(square);
	}

	GameObject CreateSquare (Square square) {
		square.createdOn = turnController.currentTurn;
		GameObject squareObj = (GameObject)Instantiate(squarePrefab);
		squareObj.transform.parent = transform;
		SquareController squareController = squareObj.GetComponent<SquareController>();
		squareController.square = square;
		squareController.Refresh();

		return squareObj;
	}

}
