using UnityEngine;
using System.Collections;

public class MouseInputController : MonoBehaviour, IInputController {

	public Camera uiCamera;
	public CameraMoveController cameraMoveController;


	bool mouseDown { get { return Input.GetMouseButtonDown(0); } }
	bool mouseHold { get { return Input.GetMouseButton(0); } }
	bool mouseUp { get { return Input.GetMouseButtonUp(0); } }
	bool mouseRightDown { get { return Input.GetMouseButtonDown(1); } }
	bool rPressed { get { return Input.GetKey(KeyCode.R); } }
	bool rotatePressed { get { return (rPressed || mouseRightDown); } }
	bool mouseMoved { get { return Input.GetAxis("Mouse X") > 0f || Input.GetAxis("Mouse Y") > 0f; } }

	bool _uiLock = false;
	public bool uiLock { get { return _uiLock; } }

	bool _holdingDrop = false;
	public bool holdingDrop { get { return _holdingDrop; } }

	DropController _currentDropController;
	public DropController currentDropController { get { return _currentDropController; } }

	Tile _currentHoverTile;
	public Tile currentHoverTile { get { return _currentHoverTile; } }

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
			//MoveFrame();
		}

		if (_uiLock && mouseHold) {
			CheckTileHit();

			if (_holdingDrop && rotatePressed) {
				_currentDropController.RotateDrop();
			}
		}

		if (mouseUp && _holdingDrop) {
			CheckTileHit();
			_currentDropController.CommitDrop();
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

	bool CheckTileHit () {
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit)) {
			GameObject hitObj = hit.collider.gameObject;
			TileView tileView = hitObj.GetComponent<TileView>();
			if (tileView != null) {
				_currentHoverTile = tileView.tile;
			} else {
				_currentHoverTile = null;
			}
		} else {
			_currentHoverTile = null;
		}
		
		
		return _currentHoverTile != null;
	}

	void SelectObj (GameObject obj) {
	
	}

	void Reset () {
		_uiLock = false;
		_holdingDrop = false;
		_currentDropController = null;
	}

}
