using UnityEngine;
using System.Collections;

public class TurnController : GameController {

	public float turnTime;
	public Turn currentTurn;

	public TurnView turnView;

	[System.NonSerialized]
	public Player turnPlayer;

	// Use this for initialization
	void Start () {
		currentTurn = new Turn(0, turnTime);
//		NotificationCenter.AddObserver(this, Notifications.Tick);
		Invoke ("ChangeTurn", 1f);
	}

	public void ChangeTurn () {
		if (currentPlayer == null || currentPlayer == opponent) {
			Debug.Log ("Player's turn");
			turnPlayer = player;
		} else {
			Debug.Log ("Opponent's Turn");
			turnPlayer = opponent;
		}

		NotificationCenter.PostNotification(this, Notifications.TurnChange);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
