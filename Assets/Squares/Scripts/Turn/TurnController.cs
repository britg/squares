using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnController : GameController {

	public float turnTime;
	public Turn currentTurn;

	public TurnView turnView;

	[System.NonSerialized]
	public Player turnPlayer;

	Hashtable turns;

	// Use this for initialization
	void Start () {
		turns = new Hashtable();
		Invoke ("ChangeTurn", 1f);
	}

	public void ChangeTurn () {

		SetTurnPlayer();
		NextTurn();

		NotificationCenter.PostNotification(this, Notifications.TurnChange);
	}

	public void SetTurnPlayer () {

		if (currentPlayer == null || currentPlayer == opponent) {
			turnPlayer = player;
		} else {
			turnPlayer = opponent;
		}
	}

	public void NextTurn () {
		List<Turn> playerTurns = (List<Turn>)turns[currentPlayer.id];
		int nextTurn = 1;
		if (playerTurns == null) {
			playerTurns = new List<Turn>();
		} else {
			Turn lastTurnForPlayer = playerTurns[playerTurns.Count - 1];
			nextTurn = lastTurnForPlayer.id + 1;
		}
		currentTurn = new Turn(currentPlayer, nextTurn);
		playerTurns.Add(currentTurn);
		turns[currentPlayer.id] = playerTurns;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
