using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {

	public GameObject tilePrefab;
	public Vector2 boardDimensions;
	public float tileSpacing;

	// Use this for initialization
	void Start () {
		DrawBoard();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DrawBoard () {

		int rows = Mathf.RoundToInt(boardDimensions.x);
		int cols = Mathf.RoundToInt(boardDimensions.y);

		for (int row = 0; row <= rows; row++) {
			for (int col = 0; col <= cols; col++) {
				DrawTile(row, col);
			}
		}

	}

	void DrawTile (int row, int col) {
		GameObject thisTile = (GameObject)Instantiate(tilePrefab, new Vector3(row + row*tileSpacing, col + col*tileSpacing, 0), Quaternion.identity);
		thisTile.transform.parent = transform;
	}
}
