using UnityEngine;
using System.Collections;

[System.Serializable]
public class Drop {

	public enum Pattern {
		I,
		J,
		L,
		O,
		S,
		T,
		Z
	}

	public enum Rotation {
		Default,
		Up,
		Right,
		Down
	}

	public Drop.Pattern pattern;
	public Drop.Rotation rotation = Drop.Rotation.Default;

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
		// TEMP
		return TileCountForZ();
	}

	public static int TileCountForZ () {
		return 4;
	}

	public static Vector2[] OffsetsForPattern (Drop.Pattern pattern, Drop.Rotation rotation) {
		// TEMP
		return OffsetsForZ(rotation);
	}

	public static Vector2[] OffsetsForZ (Drop.Rotation rotation) {

		switch (rotation) {
		case Rotation.Default:
			return OffsetsForZDefault();
		case Rotation.Up:
			return OffsetsForZUp();
		case Rotation.Right:
			return OffsetsForZRight();
		case Rotation.Down:
			return OffsetsForZDown();
		default:
				break;
		}

		return OffsetsForZDefault();
	}

	public static Vector2[] OffsetsForZDefault () {
		return new Vector2[4] { new Vector2(0, 0),
								new Vector2(1, 0),
								new Vector2(0, 1),
								new Vector2(-1, 1) };
	}

	public static Vector2[] OffsetsForZUp () {
		return new Vector2[4] { new Vector2(0, 0),
								new Vector2(1, 0),
								new Vector2(0, -1),
								new Vector2(1, 1) };
	}

	public static Vector2[] OffsetsForZRight () {
		return new Vector2[4] { new Vector2(-1, 0),
								new Vector2(0, 0),
								new Vector2(0, -1),
								new Vector2(1, -1) };
	}

	public static Vector2[] OffsetsForZDown () {
		return new Vector2[4] { new Vector2(0, 1),
								new Vector2(0, 0),
								new Vector2(-1, 0),
								new Vector2(-1, -1) };
	}

}
