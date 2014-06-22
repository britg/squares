using UnityEngine;
using System.Collections;

[System.Serializable]
public class Player : ITileOccupant {

	public string name;
	public int id;
	public Tile.Color color;
	public int attack = 0;
	public int life = 20;


}
