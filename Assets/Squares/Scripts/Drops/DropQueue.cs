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
		Renumerate();
	}
	
	public void AddDrop (int position, Drop drop) {
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

	}

	bool HasDot () {
		bool hasDot = false;
		dropList.ForEach(delegate (Drop drop) {
			if (drop.pattern == Drop.Pattern.Dot) {
				hasDot = true;
			}
		});

		return hasDot;
	}

	public void Renumerate () {
		Drop drop;
		int toFill = dropCount - dropList.Count;
		for (int n = 0; n < toFill; n++) {
			if (n == 0 && !HasDot()) {
				drop = Drop.Dot();
			} else {
				drop = Drop.RandomDrop();
			}
			AddDrop (dropList.Count, drop);
		}
	}
	
}
