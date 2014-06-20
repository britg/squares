using UnityEngine;
using System.Collections;

[System.Serializable]
public class Turn {

	public int id = 1;
	public float nextTurnTime = 10f;
	public float timeElapsed = 0f;

	public float timeRemaining {
		get { return nextTurnTime - timeElapsed; }
	}

	public Turn (int turnNum, float turnTime) {
		id = turnNum;
		nextTurnTime = turnTime;
	}


}
