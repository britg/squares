using UnityEngine;
using System.Collections;

public class DropTrayController : MonoBehaviour {

	Vector3 initialDropPosition;
	public Vector3 playableDropPosition;
	public float dropSpacing = 100f;
	public int initialDropCount;

	public GameObject dropPrefab;

	// Use this for initialization
	void Start () {
		GetInitialDropPosition();
		Invoke("SeedDrop", 1f);
		NotificationCenter.AddObserver(this, Notifications.DropUsed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetInitialDropPosition () {
		var bounds = NGUIMath.CalculateAbsoluteWidgetBounds(transform);
		initialDropPosition.y = bounds.center.y / 2f + 100;
	}

	void SeedDrop () {
		for (int i = 0; i < initialDropCount; i++) {
			Drop drop = Drop.RandomDrop();
			AddDrop(drop, i);
		}

		AnimateToTrayPositions();
	}

	void AddDrop (Drop drop, int pos) {
		GameObject dropObj = NGUITools.AddChild(gameObject, dropPrefab);
		dropObj.transform.localPosition = initialDropPosition + new Vector3(0f, dropSpacing*pos, 0f);
		DropController dropController = dropObj.GetComponent<DropController>();
		dropController.SetDrop(drop);
		dropController.trayPosition = pos;
	}

	DropController[] DropControllers () {
		DropController[] controllers = new DropController[transform.childCount];
		int i = 0;
		foreach (Transform child in transform) {
			GameObject obj = child.gameObject;
			DropController dropController = obj.GetComponent<DropController>();
			controllers[i] = dropController;
			i++;
		}

		return controllers;
	}

	void AnimateToTrayPositions () {
		foreach (DropController dropController in DropControllers()) {
			Vector3 pos = playableDropPosition + new Vector3(0f, dropController.trayPosition*dropSpacing, 0f);
			iTween.MoveTo (dropController.gameObject, iTween.Hash("position", pos, "isLocal", true, "time", 0.5f));
		}
	}

	void OnDropUsed () {
		Backfill();
	}

	void Backfill () {
		foreach (DropController dropController in DropControllers()) {
			dropController.trayPosition -= 1;
		}

		AddDrop(Drop.RandomDrop(), initialDropCount - 1);

		AnimateToTrayPositions();
	}

}
