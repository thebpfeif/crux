using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject CellGO;       /* Cell Prefab                  */
    public int MapHeight;           /* Map height, in cells         */ 
    public int MapWidth;            /* Map width, in cells          */
    private int cellLength = 100;   /* cell side length (square)  */ 
    private List<GameObject> cells = new List<GameObject>(); 

    // Use this for initialization
    void Start () {
        generateCells();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void generateCells()
    {
        for(int x = 0; x < MapWidth; x++)
        {
            for(int y = 0; y < MapHeight; y++)
            {
                GameObject newCell = (GameObject)Instantiate(CellGO);
                newCell.transform.position = new Vector2(x * cellLength, y * cellLength);
                newCell.transform.parent = transform; 
                cells.Add(newCell);
                
            }
        }
    }

    private void drawCell(Vector3[] positions)
    {
        LineRenderer lr = gameObject.AddComponent<LineRenderer>();

        lr.startColor = Color.black;
        lr.endColor = Color.black;
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        lr.numPositions = 5;
        lr.SetPositions(positions);
        lr.useWorldSpace = true; 
    }
}
