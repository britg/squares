using UnityEngine;
using System.Collections;

public class ZoomController : MonoBehaviour {

	public GameObject camera;
	public float multiplier = 1f;
	public float maxOut = 1000f;
	public float maxIn = 10f;
	public float bounceTime = 1f;

	float bounceAmount = 10f;

	Transform anchor;

	float scrollAmount { get { return Input.GetAxis("Mouse ScrollWheel") * multiplier; } }
	bool zoomingOut { get { return scrollAmount > 0; } }
	bool zoomingIn { get { return scrollAmount < 0; } }
	float currentDistance { get { return Vector3.Distance(camera.transform.position, anchor.position); } }
	bool tooClose { get { return currentDistance < maxIn; } }
	bool tooFar { get { return currentDistance > maxOut; } }
	bool outOfBounds { get { return tooFar || tooClose; } }


	void Start () {
		anchor = transform;
	}
	
	void Update () {

		if (tooClose) {
			BounceOut();
		} else if (tooFar) {
			BounceIn();
		} else {
			DetectInput();
		}

	}

	void DetectInput () {
		if ((zoomingIn || zoomingOut) && !outOfBounds) {
			Zoom();
		}
	}

	void Zoom () {
		Vector3 newPos = Vector3.MoveTowards(camera.transform.position, anchor.position, scrollAmount);
		camera.transform.position = newPos;
	}

	void BounceOut () {
		bounceAmount = (maxIn - currentDistance)*2;
		Vector3 outPos = Vector3.MoveTowards(camera.transform.localPosition, Vector3.zero, -bounceAmount);
		iTween.MoveTo(camera, iTween.Hash("position", outPos, "time", bounceTime, "isLocal", true));
	}
	
	void BounceIn () {
		bounceAmount = (currentDistance - maxOut)*2;
		Vector3 inPos = Vector3.MoveTowards(camera.transform.localPosition, Vector3.zero, bounceAmount);
		iTween.MoveTo(camera, iTween.Hash("position", inPos, "time", bounceTime, "isLocal", true));
	}

}
