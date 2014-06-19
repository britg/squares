using UnityEngine;
using System.Collections;

public class DropController : GameController  {

	DropTrayController playTrayController {
		get { return GameObject.Find("Play Tray").GetComponent<DropTrayController>(); }
	}

	UISprite sprite {
		get { return GetComponent<UISprite>(); }
	}

	public Drop drop;
	public int trayPosition;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetDrop (Drop _drop) {
		drop = _drop;
		UpdateSprite();
	}

	public void RotateDrop () {
		// Need to decouple the tween rotation here
		if (iTween.Count(gameObject) < 1) {
			drop.Rotate();
			iTween.RotateBy (gameObject, new Vector3(0f, 0f, -0.25f), 0.3f);
		}
	}

	public void CommitDrop () {
		DropValidator dropValidator = new DropValidator(tileCollection);

		Tile[] dropTiles = dropValidator.ValidDropTiles(drop, inputController.currentHoverTile, player);

		if (dropTiles != null) {
			foreach(Tile tile in dropTiles) {
				tile.SetOwner(player);
			}
			NotificationCenter.PostNotification(this, Notifications.DropUsed);
			Destroy(gameObject);
		} else {
			MoveToPlayablePosition();
		}
	}

	public void MoveToPlayablePosition () {
		iTween.MoveTo(gameObject, iTween.Hash("isLocal", true, 
		                                      "position", playTrayController.playableDropPosition, 
		                                      "time", 0.5f));
	}

	public void UpdateSprite () {
		sprite.spriteName = drop.pattern.ToString().ToLower();
	}

}
