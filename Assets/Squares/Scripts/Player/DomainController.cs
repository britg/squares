using UnityEngine;
using System.Collections;

public class DomainController : GameController {

	public Domain domain;

	void Start () {
		domain = new Domain(player);
		NotificationCenter.AddObserver(this, Notifications.TileOwnershipChange);
	}

	void OnTileOwnershipChange () {
		RefreshDomain();
	}

	void RefreshDomain () {
		int previous = domain.tilesOwned;
		domain.ParseTiles(tileCollection);
		if (previous != domain.tilesOwned) {
			NotificationCenter.PostNotification(this, Notifications.BlockOwnershipChange);
		}
	}

}
