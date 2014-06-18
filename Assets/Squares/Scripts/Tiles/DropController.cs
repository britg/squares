using UnityEngine;
using System.Collections;

public class DropController : MonoBehaviour {

	public Drop drop;

	public UISprite sprite {
		get {
			return gameObject.GetComponent<UISprite>();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RotateDrop() {
		drop.Rotate();
		iTween.RotateBy (gameObject, new Vector3(0f, 0f, -0.25f), 0.3f);
	}

	void UpdateSprite () {
	}
}
