using UnityEngine;
using System.Collections;

public class MouseInputController : MonoBehaviour {

	public Camera uiCamera;
	public CameraMoveController cameraMoveController;


	bool mouseDown { get { return Input.GetMouseButtonDown(0); } }
	bool mouseHold { get { return Input.GetMouseButton(0); } }
	bool mouseUp { get { return Input.GetMouseButtonUp(0); } }

	bool uiLock = false;

	Vector3 lastPos;

	void Update () {
		DetectInput();
	}

	void DetectInput () {

		if (mouseDown) {
			uiLock = CheckUIHit();
			if (uiLock) {
				return;
			}

			// move
			StartMove();

		}

		if (!uiLock && mouseHold) {
			// Pass through to move controller
			MoveFrame();
		}

		if (mouseUp) {
			uiLock = false;
		}
	}

	void StartMove () {
		lastPos = Input.mousePosition;
	}

	void MoveFrame () {
		Vector3 deltaPos = Input.mousePosition - lastPos;
		lastPos = Input.mousePosition;
		cameraMoveController.MoveFrame(deltaPos);
	}

	bool CheckUIHit () {
		Ray ray = uiCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		return Physics.Raycast(ray, out hit);
	}

	void SelectObj (GameObject obj) {
	
	}

}
