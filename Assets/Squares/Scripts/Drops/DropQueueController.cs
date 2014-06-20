using UnityEngine;
using System.Collections;

public class DropQueueController : MonoBehaviour {

	Vector3 initialDropPosition;
	public Vector3 playableDropPosition;
	public float dropSpacing = 100f;
	public int initialDropCount;

	public GameObject dropPrefab;

	DropQueue dropQueue;

	// Use this for initialization
	void Start () {
		GetInitialDropPosition();
		dropQueue = new DropQueue(initialDropCount);
		NotificationCenter.AddObserver(this, Notifications.DropUsed);
		Invoke("RenderDrops", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GetInitialDropPosition () {
		var bounds = NGUIMath.CalculateAbsoluteWidgetBounds(transform);
		initialDropPosition.y = bounds.center.y / 2f + 100;
	}

	void RenderDrops () {
		foreach(Drop drop in dropQueue.dropList) {
			DropController dropController = ControllerForDrop(drop);
			dropController.AnimateToQueuePosition();
		}
	}

	DropController ControllerForDrop (Drop drop) {
		GameObject dropObject = GameObject.Find("Drop " + drop.sequenceId);
		if (dropObject == null) {
			dropObject = CreateObjectForDrop(drop);
		}
		return dropObject.GetComponent<DropController>();
	}

	GameObject CreateObjectForDrop (Drop drop) {
		GameObject dropObj = NGUITools.AddChild(gameObject, dropPrefab);
		dropObj.name = drop.name;
		dropObj.transform.localPosition = initialDropPosition + new Vector3(0f, dropSpacing*drop.currentQueuePosition, 0f);
		DropController dropController = dropObj.GetComponent<DropController>();
		dropController.SetDrop(drop);
		return dropObj;
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

	public void DropUsed (Drop drop) {
		dropQueue.UseDrop(drop);
		DropController dropController = ControllerForDrop(drop);
		dropController.Destroy();
		RenderDrops();
	}

}
