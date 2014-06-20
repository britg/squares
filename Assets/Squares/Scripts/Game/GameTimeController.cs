using UnityEngine;
using System.Collections;

public class GameTimeController : MonoBehaviour {

	public void OnIncreaseGameSpeed () {
		print("Increase speed");
		GameTime.IncreaseSpeed();
	}

	public void OnDecreaseGameSpeed () {
		print("Descrease speed");
		GameTime.DecreaseSpeed();
	}
}
