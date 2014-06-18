using UnityEngine;
using System.Collections;

public class DropHintController : GameController {

	Tile previousHoverTile;
	Tile currentHoverTile;

	TilesController tilesController {
		get { return gameObject.GetComponent<TilesController>(); }
	}

	TileCollection tileCollection {
		get { return tilesController.tileCollection; }
	}

	DropValidator dropValidator;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (inputController.holdingDrop && OverTile()) {
			if (previousHoverTile != currentHoverTile) {
				Hint();
			}
			previousHoverTile = currentHoverTile;
		}
	}

	bool OverTile () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit)) {
			GameObject hitObj = hit.collider.gameObject;
			TileView tileView = hitObj.GetComponent<TileView>();
			if (tileView != null) {
				currentHoverTile = tileView.tile;
			} else {
				currentHoverTile = null;
			}
		} else {
			currentHoverTile = null;
		}
		

		return currentHoverTile != null;
	}

	void Hint () {
		dropValidator = new DropValidator(tileCollection);

		Drop drop = inputController.currentDropController.drop;
		Debug.Log (drop.pattern + " on " + currentHoverTile);

		if (!dropValidator.ValidDrop(drop, currentHoverTile, player)) {
			Debug.Log ("Drop not valid");
			return;
		}

		Debug.Log ("Drop is valid!");

	}


}
