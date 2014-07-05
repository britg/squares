using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public enum InputMode {
		Mouse,
		Controller,
		Touch
	}

	public static InputMode inputMode = InputMode.Mouse;

	protected Player currentPlayer {
		get { return turnController.turnPlayer; }
	}

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

	protected Player ownerForType (OwnerType type) {
		if (type == OwnerType.Player) {
			return player;
		}
		return opponent;
	}

	protected Player enemyTo (Player _player) {
		if (_player == player) {
			return opponent;
		} else {
			return player;
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

	GameObject _boardObject;
	protected GameObject boardObject {
		get {
			if (_boardObject == null) {
				_boardObject = GameObject.Find("Board");
			}
			return _boardObject;
		}
	}

	GameObject _gameObject;
	protected GameObject gameStateObject {
		get {
			if (_gameObject == null) {
				_gameObject = GameObject.Find("Game");
			}
			return _gameObject;
		}
	}

	TilesController _tilesController;
	protected TilesController tilesController {
		get { 
			if (_tilesController == null) {
				_tilesController = boardObject.GetComponent<TilesController>(); 
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

	GameObject _dropQueueObjPlayer;
	GameObject _dropQueueObjOpponent;
	protected GameObject dropQueueObjForOnwer (OwnerType ownerType) {
		return dropQueueObjForOwner(ownerForType(ownerType));
	}
	protected GameObject dropQueueObjForOwner (Player owner) {
		if (owner.ownerType == OwnerType.Player) {
			if (_dropQueueObjPlayer == null) {
				_dropQueueObjPlayer = GameObject.Find ("Drop Queue " + owner.boardSide);
			}
			return _dropQueueObjPlayer;
		}
		if (_dropQueueObjOpponent == null) {
			_dropQueueObjOpponent = GameObject.Find ("Drop Queue " + owner.boardSide);
		}
		return _dropQueueObjOpponent;
	}

	DropQueueController _dropQueueControllerPlayer;
	DropQueueController _dropQueueControllerOpponent;
	protected DropQueueController dropQueueControllerForOwner (OwnerType ownerType) {
		return dropQueueControllerForOwner(ownerForType(ownerType));
	}
	protected DropQueueController dropQueueControllerForOwner (Player owner) {
		if (player.ownerType == OwnerType.Player) {
			if (_dropQueueControllerPlayer == null) {
				_dropQueueControllerPlayer = dropQueueObjForOwner(owner).GetComponent<DropQueueController>();
			}
			return _dropQueueControllerPlayer;
		}

		if (_dropQueueControllerOpponent == null) {
			_dropQueueControllerOpponent = dropQueueObjForOwner(owner).GetComponent<DropQueueController>();
		}
		return _dropQueueControllerOpponent;

	}

	TurnController _turnController;
	protected TurnController turnController {
		get {
			if (_turnController == null) {
				_turnController = gameStateObject.GetComponent<TurnController>();
			}
			return _turnController;
		}
	}

	SquaresController _squaresController;
	protected SquaresController squaresController {
		get {
			if (_squaresController == null) {
				_squaresController = GameObject.Find("Squares").GetComponent<SquaresController>();
			}
			return _squaresController;
		}
	}


}
