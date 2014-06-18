using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public enum InputMode {
		Mouse,
		Controller,
		Touch
	}

	public static InputMode inputMode = InputMode.Mouse;

	protected GameObject playerObject {
		get { return GameObject.Find("Player"); }
	}

	protected PlayerController playerController {
		get { return playerObject.GetComponent<PlayerController>(); }
	}

	protected Player player {
		get { return playerController.player; }
	}

	protected GameObject opponentObject {
		get { return GameObject.Find("Opponent"); }
	}

	protected OpponentController opponentController {
		get { return opponentObject.GetComponent<OpponentController>(); }
	}

	protected Player opponent {
		get { return opponentController.opponent; }
	}

	protected IInputController inputController {
		get {
			if (inputMode == InputMode.Mouse) {
				return playerObject.GetComponent<MouseInputController>();
			}

			return playerObject.GetComponent<MouseInputController>();
		}
	}
}
