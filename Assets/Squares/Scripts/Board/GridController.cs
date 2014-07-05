using UnityEngine;
using System.Collections;
using Vectrosity;

public class GridController : GameController {

	public Color minorLineColor = Color.grey;
	public Color majorLineColor = new Color(0.9f, 0.9f, 0.9f);

	public bool drawGrid = false;

	float height {
		get { return tilesController.completeDimensions.y * (tilesController.tileWidth + tilesController.tileSpacing); }
	}

	float width {
		get { return tilesController.completeDimensions.x * (tilesController.tileWidth + tilesController.tileSpacing); }
	}

	float xSpacing {
		get { return tilesController.tileSpacing/2f + tilesController.tileWidth; }
	}

	float ySpacing {
		get { return tilesController.tileSpacing/2f + tilesController.tileWidth; }
	}


	// Use this for initialization
	void Start () {
		if (drawGrid) {
			DrawGrid();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DrawGrid () {
		DrawRows();
		DrawColumns();
	}

	void DrawRows () {
		for (int i = 1; i < tilesController.completeDimensions.y; i++) {
			DrawRow(i);
		}
	}

	void DrawRow (int i) {
		float offset = tilesController.tileWidth/2f;
		float y = (i*ySpacing) + (i-1)*tilesController.tileSpacing/2f - offset;
		DrawLine(new Vector3(-offset, y, 0f), new Vector3(width-offset, y, 0f), i, (int)tilesController.baseBlockDimensions.y);
	}

	void DrawColumns () {
		for (int i = 1; i < tilesController.completeDimensions.x; i++) {
			DrawColumn(i);
		}
	}

	void DrawColumn (int i) {
		float offset = tilesController.tileWidth/2f;
		float x = (i*xSpacing) + (i-1)*tilesController.tileSpacing/2f - offset;
		DrawLine (new Vector3(x, -offset, 0f), new Vector3(x, height-offset, 0f), i, (int)tilesController.baseBlockDimensions.x);
	}

	void DrawLine (Vector3 from, Vector3 to, int i, int baseBlock) {
		VectorLine line = VectorLine.SetLine3D(minorLineColor, from, to);
		line.drawTransform = gameObject.transform;
		if (i%baseBlock == 0) {
//			line.SetWidth(1f, 0);
			line.SetColor(majorLineColor, 0);
			line.Draw3D();
		}
	}

}
