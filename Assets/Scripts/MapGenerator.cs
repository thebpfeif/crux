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
                                    /* List of map cells            */ 

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
        CellProperties properties; 
        int occupancyCount; 
        bool[,] cellOccupancy;

        CircleCollider2D collider = Asteroid.GetComponent<CircleCollider2D>();

        foreach( GameObject cell in cells )
        {
            /* Get the cell position */
            Vector2 cellPosition = cell.transform.position;

            /* Get the cell properties */
            properties = cell.GetComponent<CellProperties>();

            /* Clear out the array */
            cellOccupancy = new bool[cellLength, cellLength];
            occupancyCount = 0;

            while( occupancyCount <= properties.AsteroidDenstiy )
            {
                /* Keep looking for an empty space */ 
                do
                {
                    /* Find a random, relative x and y coordinate */
                    asteroidPosition.x = Random.Range(0, cellLength);
                    asteroidPosition.y = Random.Range(0, cellLength);

                } while (cellOccupancy[(int)asteroidPosition.x, (int)asteroidPosition.y] == true);

                /* Mark the spot as occupied */
                cellOccupancy[(int)asteroidPosition.x, (int)asteroidPosition.y] = true;
                occupancyCount++;

                /* instantiate and place the asteroid */ 
                GameObject newAsteroid = (GameObject)Instantiate(Asteroid);
                newAsteroid.transform.parent = cell.transform; 
                newAsteroid.transform.localPosition = asteroidPosition;

            }


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

                /* Give it a random asteroid density (for now) */
                properties.AsteroidDenstiy = Random.Range(0, 100);

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
