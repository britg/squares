using UnityEngine;
using System.Collections;

public class TurnIndicatorView : GameController {

	public UILabel playerLabel;
	public UILabel opponentLabel;

	void Start () {
		NotificationCenter.AddObserver(this, Notifications.TurnChange);
	}

	void OnTurnChange () {
		if (currentPlayer == player) {
			playerLabel.color = Color.blue;
			opponentLabel.color = Color.grey;
		} else {
			playerLabel.color = Color.grey;
			opponentLabel.color = Color.green;
		}
	}

}
