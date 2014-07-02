using UnityEngine;
using System.Collections;

public class SquareController : MonoBehaviour {

	public Square square;

	public void Refresh () {
		transform.position = new Vector3(square.tiles[0].col, square.tiles[0].col, Game.squareZ);
	}
}
