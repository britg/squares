using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropQueue {

	Player owner;
	public List<Drop> dropList;
	int dropCount;
	int sequenceId = 0;

	public DropQueue (int numDrops, Player _owner) {
		owner = _owner;
		dropCount = numDrops;
		SeedDrops();
	}

	
	void SeedDrops () {
		dropList = new List<Drop>();
		for (int i = 0; i < dropCount; i++) {
			AddDrop(i);
		}
	}
	
	public void AddDrop (int position) {
		Drop drop = Drop.RandomDrop();
		drop.owner = owner;
		drop.currentQueuePosition = position;
		drop.sequenceId = sequenceId;
		dropList.Add(drop);
		sequenceId++;
	}

	public void UseDrop (Drop drop) {
		int i = dropList.IndexOf(drop);
		dropList[i] = null;
		drop = null;
		FillIn();
	}

	void FillIn() {
		int missing = 0;
		List<Drop> newList = new List<Drop>();
		for (int i = 0; i < dropList.Count/*dropCount*/; i++) {
			Drop drop = dropList[i];

			if (drop == null) {
				missing++;
				continue;
			}

			drop.currentQueuePosition -= missing;
			newList.Add(drop);
		}

		dropList = newList;

		for (int n = 0; n < missing; n++) {
//			AddDrop(dropList.Count);
		}

	}
	
}
