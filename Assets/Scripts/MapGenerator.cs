using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject CellGO;       /* Cell Prefab                  */
    public GameObject Asteroid;     /* Asteroid Prefab              */ 
    public int MapHeight;           /* Map height, in cells         */ 
    public int MapWidth;            /* Map width, in cells          */
    private int cellLength = 100;   /* cell side length (square)    */ 
    private List<GameObject> cells = new List<GameObject>(); 

    // Use this for initialization
    void Start () {
        generateCells();
        generateAsteroids(); 

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void generateAsteroids()
    {
        Vector2 asteroidPosition;

        foreach( GameObject cell in cells )
        {
            /* Get the cell position */
            Vector2 cellPosition = cell.transform.position;

            /* primitive random placement */
            /* Find a random x and y coordinate */
            asteroidPosition.x = Random.Range(cellPosition.x, cellPosition.x + cellLength);
            asteroidPosition.y = Random.Range(cellPosition.y, cellPosition.y + cellLength);

            GameObject newAsteroid = (GameObject)Instantiate(Asteroid);
            newAsteroid.transform.position = asteroidPosition; 

        }
    }

    private void generateCells()
    {
        Vector2 coordinates; 
        for(int x = 0; x < MapWidth; x++)
        {
            for(int y = 0; y < MapHeight; y++)
            {
                coordinates = new Vector2(x, y);

                /* Create new cell object for the grid */ 
                GameObject newCell = (GameObject)Instantiate(CellGO);

                /* Set the cell coordinates in properties */
                CellProperties properties = newCell.GetComponent<CellProperties>();
                properties.CellCoordinates = coordinates;

                /* Place the cell in the world space, save it to list */ 
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
        lr.positionCount = 5;
        lr.SetPositions(positions);
        lr.useWorldSpace = true; 
    }
}
