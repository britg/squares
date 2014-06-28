using UnityEngine;
using System.Collections;

public class DropController : GameController  {

	DropQueueController dropQueueController {
		get {
			return dropQueueControllerForOwner(drop.owner);
		}
	}

	UISprite sprite {
		get { return GetComponent<UISprite>(); }
	}

	public Drop drop;
	public Player owner {
		get { return drop.owner; }
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToggleDraggable () {
		if (currentPlayer != owner) {
			Destroy(GetComponent<UIDragObject>());
		} else {
			if (GetComponent<UIDragObject>() != null) {
				return;
			}
			UIDragObject dragController = gameObject.AddComponent<UIDragObject>();
			dragController.target = transform;
			dragController.dragEffect = UIDragObject.DragEffect.None;
		}
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

		Tile[] dropTiles = dropValidator.ValidDropTiles(drop, inputController.currentHoverTile, currentPlayer);

		if (dropTiles != null) {
			foreach(Tile tile in dropTiles) {
				tile.SetOwner(currentPlayer);
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
