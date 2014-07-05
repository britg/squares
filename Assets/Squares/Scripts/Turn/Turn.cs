using UnityEngine;
using System.Collections;

[System.Serializable]
public class Turn {

	public int id = 1;
	public Player player;
	public float nextTurnTime = 10f;
	public float timeElapsed = 0f;

	public Turn (Player _player, int turnNum) {
		player = _player;
		id = turnNum;
	}

}
