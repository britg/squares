using UnityEngine;
using System.Collections;
using System.Reflection;

[System.Serializable]
public class Drop {

	public enum Pattern {
		I,
		J,
		L,
		O,
		S,
		T,
		Z,
		Dot
	}

	public enum Rotation {
		Default,
		Up,
		Right,
		Down
	}

	public Drop.Pattern pattern;
	public Drop.Rotation rotation = Drop.Rotation.Default;
	public int currentQueuePosition;
	public int sequenceId;
	public Player owner;

	public string name {
		get { return "Drop " + sequenceId + " " + owner.ownerType.ToString(); }
	}

	public static Drop RandomDrop () {
		Drop drop = new Drop();
		drop.pattern = Drop.RandomPattern();
		return drop;
	}

	public static Drop.Pattern RandomPattern () {
		var values = System.Enum.GetValues(typeof(Drop.Pattern));
		Drop.Pattern randomPattern = (Drop.Pattern)values.GetValue(Random.Range(0, values.Length));
		return randomPattern;
	}

	static Vector2 V (int x, int y) {
		return new Vector2(x, y);
	}

	public Vector2[] offsets {
		get {
			return Drop.OffsetsForPattern(pattern, rotation);
		}
	}

	public int tileCount {
		get {
			return Drop.TileCountForPattern(pattern);
		}
	}

	public void Rotate () {
		switch(rotation) {
		case Rotation.Default:
			rotation = Rotation.Up;
			break;
		case Rotation.Up:
			rotation = Rotation.Right;
			break;
		case Rotation.Right:
			rotation = Rotation.Down;
			break;
		case Rotation.Down:
			rotation = Rotation.Default;
			break;
		}
	}

	public static int TileCountForPattern (Drop.Pattern pattern) {
		return OffsetsForPattern(pattern).Length;
	}

	public static Vector2[] OffsetsForPattern(Drop.Pattern pattern) {
		return OffsetsForPattern(pattern, Drop.Rotation.Default);
	}

	public static Vector2[] OffsetsForPattern (Drop.Pattern pattern, Drop.Rotation rotation) {
		string p = pattern.ToString();
		string r = rotation.ToString();

		return (Vector2[])typeof(Drop).GetMethod("OffsetsFor" + p + r).Invoke(typeof(Drop), null);
	}

	// I

	public static Vector2[] OffsetsForIDefault () {
		return new Vector2[4] { V(0, 0), V(0, 1), V(0, 2), V(0, 3) };
	}
	public static Vector2[] OffsetsForIUp () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(2, 0), V(3, 0) };
	}
	public static Vector2[] OffsetsForIRight () {
		return new Vector2[4] { V(0, 0), V(0, 1), V(0, 2), V(0, 3) };
	}
	public static Vector2[] OffsetsForIDown () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(2, 0), V(3, 0) };
	}

	// J

	public static Vector2[] OffsetsForJDefault () {
		return new Vector2[4] { V(0, 0), V(-1, 0), V(0, 1), V(0, 2) };
	}
	public static Vector2[] OffsetsForJUp () {
		return new Vector2[4] { V(0, 0), V(0, 1), V(1, 0), V(2, 0) };
	}
	public static Vector2[] OffsetsForJRight () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(0, -1), V(0, -2) };
	}
	public static Vector2[] OffsetsForJDown () {
		return new Vector2[4] { V(0, 0), V(0, -1), V(-1, 0), V(-2, 0) };
	}

	// L

	public static Vector2[] OffsetsForLDefault () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(0, 1), V(0, 2) };
	}
	public static Vector2[] OffsetsForLUp () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(2, 0), V(0, -1) };
	}
	public static Vector2[] OffsetsForLRight () {
		return new Vector2[4] { V(0, 0), V(-1, 0), V(0, -1), V(0, -2) };
	}
	public static Vector2[] OffsetsForLDown () {
		return new Vector2[4] { V(0, 0), V(0, 1), V(-1, 0), V(-2, 0) };
	}

	// O

	public static Vector2[] OffsetsForODefault () {
		return new Vector2[4] { V(0, 0), V(0, 1), V(1, 1), V(1, 0) };
	}
	public static Vector2[] OffsetsForOUp () {
		return OffsetsForODefault();
	}
	public static Vector2[] OffsetsForORight () {
		return OffsetsForODefault();
	}
	public static Vector2[] OffsetsForODown () {
		return OffsetsForODefault();
	}

	// S

	public static Vector2[] OffsetsForSDefault () {
		return new Vector2[4] { V(0, 0), V(0, 1), V(1, 1), V(-1, 0) };
	}
	public static Vector2[] OffsetsForSUp () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(1, -1), V(0, 1) };
	}
	public static Vector2[] OffsetsForSRight () {
		return OffsetsForSDefault();
	}
	public static Vector2[] OffsetsForSDown () {
		return OffsetsForSUp();
	}

	// T

	public static Vector2[] OffsetsForTDefault () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(-1, 0), V(0, -1) };
	}
	public static Vector2[] OffsetsForTUp () {
		return new Vector2[4] { V(0, 0), V(0, 1), V(0, -1), V(-1, 0) };
	}
	public static Vector2[] OffsetsForTRight () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(-1, 0), V(0, 1) };
	}
	public static Vector2[] OffsetsForTDown () {
		return new Vector2[4] { V(0, 0), V(0, 1), V(0, -1), V(1, 0) };
	}

	// Z

	public static Vector2[] OffsetsForZDefault () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(0, 1), V(-1, 1) };
	}
	public static Vector2[] OffsetsForZUp () {
		return new Vector2[4] { V(0, 0), V(1, 0), V(0, -1), V(1, 1) };
	}
	public static Vector2[] OffsetsForZRight () {
		return OffsetsForZDefault();
	}
	public static Vector2[] OffsetsForZDown () {
		return OffsetsForZUp();
	}

	// Dot
	
	public static Vector2[] OffsetsForDotDefault () {
		return new Vector2[1] { V(0, 0) };
	}
	public static Vector2[] OffsetsForDotUp () {
		return OffsetsForZUp();
	}
	public static Vector2[] OffsetsForDotRight () {
		return OffsetsForZDefault();
	}
	public static Vector2[] OffsetsForDotDown () {
		return OffsetsForZDefault();
	}

}
