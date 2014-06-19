using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public enum InputMode {
		Mouse,
		Controller,
		Touch
	}

	public static InputMode inputMode = InputMode.Mouse;

	GameObject _playerObject;
	protected GameObject playerObject {
		get { 
			if (_playerObject == null) {
				_playerObject = GameObject.Find("Player"); 
			}
			return _playerObject;
		}
	}

	PlayerController _playerController;
	protected PlayerController playerController {
		get { 
			if (_playerController == null) {
				_playerController = playerObject.GetComponent<PlayerController>(); 
			}
			return _playerController;
		}
	}

	Player _player;
	protected Player player {
		get { 
			if (_player == null) {
				_player = playerController.player; 
			}
			return _player;
		}
	}

	GameObject _opponentObject;
	protected GameObject opponentObject {
		get { 
			if (_opponentObject == null) {
				_opponentObject = GameObject.Find("Opponent"); 
			}
			return _opponentObject;
		}
	}

	OpponentController _opponentController;
	protected OpponentController opponentController {
		get { 
			if (_opponentController == null) {
				_opponentController = opponentObject.GetComponent<OpponentController>(); 
			}
			return _opponentController;
		}
	}

	Player _opponent;
	protected Player opponent {
		get { 
			if (_opponent == null) {
				_opponent = opponentController.opponent;
			}
			return _opponent;
		}
	}

	IInputController _inputController;
	protected IInputController inputController {
		get {
			if (_inputController == null) {
				if (inputMode == InputMode.Mouse) {
					_inputController = playerObject.GetComponent<MouseInputController>();
				}
			}

			return _inputController;
		}
	}

	TilesController _tilesController;
	protected TilesController tilesController {
		get { 
			if (_tilesController == null) {
				_tilesController = GameObject.Find("Board").GetComponent<TilesController>(); 
			}
			return _tilesController;
		}
	}

	TileCollection _tileCollection;
	protected TileCollection tileCollection {
		get { 
			if (_tileCollection == null) {
				_tileCollection = tilesController.tileCollection; 
			}
			return _tileCollection;
		}
	}
}
