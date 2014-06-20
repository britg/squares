using UnityEngine;
using System.Collections;

public class DropController : GameController  {

	UISprite sprite {
		get { return GetComponent<UISprite>(); }
	}

	public Drop drop;

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
			dropQueueController.DropUsed(drop);
			NotificationCenter.PostNotification(this, Notifications.TileOwnershipChange);
		} else {
			AnimateToQueuePosition();
		}

		NotificationCenter.PostNotification(this, Notifications.TileStateChange);
	}

	public void MoveToPlayablePosition () {
		iTween.MoveTo(gameObject, iTween.Hash("isLocal", true, 
		                                      "position", dropQueueController.playableDropPosition, 
		                                      "time", 0.5f));
	}

	public void UpdateSprite () {
		sprite.spriteName = drop.pattern.ToString().ToLower();
	}
	
	public void AnimateToQueuePosition () {
		float y = drop.currentQueuePosition * dropQueueController.dropSpacing;
		Vector3 pos = dropQueueController.playableDropPosition + new Vector3(0f, y, 0f);
		iTween.MoveTo (gameObject, iTween.Hash("position", pos, "isLocal", true, "time", 0.5f));
	}

	public void Destroy() {
		Destroy (gameObject);
	}
}
