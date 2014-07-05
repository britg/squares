using UnityEngine;
using System.Collections;

public class SquareView : MonoBehaviour {

	public SquareController squareController {
		get {
			return transform.parent.gameObject.GetComponent<SquareController>();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
