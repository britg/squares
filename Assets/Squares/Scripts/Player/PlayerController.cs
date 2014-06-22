using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Player player;
	
	// Use this for initialization
	void Start () {
		NotificationCenter.AddObserver(this, Notifications.BlockOwnershipChange);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnBlockOwnershipChange () {

	}


}
