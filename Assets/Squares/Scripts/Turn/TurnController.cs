using UnityEngine;
using System.Collections;

public class TurnController : MonoBehaviour {

	public float turnTime;
	public Turn currentTurn;

	public TurnView turnView;

	// Use this for initialization
	void Start () {
		currentTurn = new Turn(0, turnTime);
		NotificationCenter.AddObserver(this, Notifications.Tick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void NextTurn () {
		currentTurn = new Turn(currentTurn.id + 1, turnTime);
		NotificationCenter.PostNotification(this, Notifications.Turn);
		UpdateDisplay();
	}

	void OnTick () {
//		UpdateTurn (1f);
	}

	void UpdateTurn (float timeElapsed) {
		currentTurn.timeElapsed += timeElapsed;
		if (currentTurn.timeRemaining <= 0f) {
			NextTurn();
		}
		UpdateDisplay ();
	}

	void UpdateDisplay () {
		turnView.Refresh(currentTurn);
	}

}
