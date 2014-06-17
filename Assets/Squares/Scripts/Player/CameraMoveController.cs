using UnityEngine;
using System.Collections;

public class CameraMoveController : MonoBehaviour {

	public float multiplier = 1f;
	public bool inverted = false;

	int sign { get { return inverted ? 1 : -1; } }

	void Start () {
	}

	void Update () {
	}

	public void MoveFrame (Vector3 delta) {
		delta = delta * sign * multiplier;
		Vector3 pos = transform.position;
		Quaternion converted = new Quaternion(transform.rotation.x, -transform.rotation.y, 0f, transform.rotation.w);
		Vector3 rotated = converted * delta;
		pos.x += rotated.x;
		pos.y += rotated.y;
		transform.position = pos;
	}

}
