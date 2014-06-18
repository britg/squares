using UnityEngine;
using System.Collections;

public interface IInputController {

	bool holdingDrop { get; }
	bool uiLock { get; }

	DropController currentDropController { get; }
}
