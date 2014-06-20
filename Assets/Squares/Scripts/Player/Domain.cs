using UnityEngine;
using System.Collections;

[System.Serializable]
public class Domain {

	public Player player;
	public int tilesOwned = 0;
	public int blocksOwned = 0;

	Hashtable blockOwnership;

	public Domain (Player _player) {
		player = _player;
	}

	public void ParseTiles (TileCollection tileCollection) {
		Debug.Log ("Parsing tiles for domain");
		tilesOwned = 0;
		blocksOwned = 0;
		blockOwnership = new Hashtable();

		foreach (Tile tile in tileCollection.allTiles()) {

			int blockX = Mathf.FloorToInt(tile.pos.x / tileCollection.blockSize.x);
			int blockY = Mathf.FloorToInt(tile.pos.y / tileCollection.blockSize.y);
			string block = blockX.ToString() + "," + blockY.ToString();

			if (tile.owner == player) {
				tilesOwned++;
				if (blockOwnership[block] == null) {
					blockOwnership[block] = true;
					blocksOwned += 1;
					Debug.Log ("I own block " + block);
				}
			} else {
				if (blockOwnership[block] != null && (bool)blockOwnership[block] == true) {
					blocksOwned -= 1;
					Debug.Log ("I don't own block " + block);
				}
				blockOwnership[block] = false;
			}
		}
	}


}
