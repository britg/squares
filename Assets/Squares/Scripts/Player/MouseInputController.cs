using UnityEngine;
using System.Collections;

public class MouseInputController : MonoBehaviour, IInputController {

	public Camera uiCamera;
	public CameraMoveController cameraMoveController;


	bool mouseDown { get { return Input.GetMouseButtonDown(0); } }
	bool mouseHold { get { return Input.GetMouseButton(0); } }
	bool mouseUp { get { return Input.GetMouseButtonUp(0); } }
	bool mouseRightDown { get { return Input.GetMouseButtonDown(1); } }

	bool _uiLock = false;
	public bool uiLock { get { return _uiLock; } }

	bool _holdingDrop = false;
	public bool holdingDrop { get { return _holdingDrop; } }

	DropController _currentDropController;
	public DropController currentDropController { get { return _currentDropController; } }

	Vector3 lastPos;

	void Update () {
		DetectInput();
	}

	void DetectInput () {

		if (mouseDown) {
			_uiLock = CheckUIHit();
			if (_uiLock) {
				return;
			}

			// move
			StartMove();

		}

		if (!_uiLock && mouseHold) {
			// Pass through to move controller
			MoveFrame();
		}

		if (_uiLock && mouseHold && _holdingDrop && mouseRightDown) {
			Debug.Log ("Rotate tile");
			_currentDropController.RotateDrop();
		}

		if (mouseUp) {
			Reset();
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
		bool didHit = Physics.Raycast(ray, out hit);

		if (didHit) {
			_currentDropController = hit.collider.gameObject.GetComponent<DropController>();
			if (_currentDropController != null) {
				_holdingDrop = true;
			}
		}

		return didHit;
	}

	void SelectObj (GameObject obj) {
	
	}

	void Reset () {
		_uiLock = false;
		_holdingDrop = false;
		_currentDropController = null;
	}

}
