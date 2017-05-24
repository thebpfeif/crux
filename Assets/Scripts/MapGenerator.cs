using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public GameObject CellGO;           /* Cell Prefab                  */
    public GameObject SmallAsteroid;    /* Small Asteroid Prefab        */
    public GameObject MedAsteroid;      /* Medium Asteroid Prefab       */
    public GameObject LargeAsteroid;    /* Large Asteroid Prefab        */ 
    public int MapHeight;               /* Map height, in cells         */ 
    public int MapWidth;                /* Map width, in cells          */
    private int cellLength = 100;       /* cell side length (square)    */ 
    private List<GameObject> cells = new List<GameObject>();
                                        /* List of map cells            */ 
    private bool[,] tracker;

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

        //CircleCollider2D collider = Asteroid.GetComponent<CircleCollider2D>();

        foreach( GameObject cell in cells )
        {
            tracker = new bool[cellLength, cellLength];
            /* Get the cell position */
            Vector2 cellPosition = cell.transform.position;

            /* Get the cell properties */
            properties = cell.GetComponent<CellProperties>();

            /* calculate the counts of each type of asteroid
             * based on properties */
            //BRP TEMP: Trying one type of asteroid each for now

            /* populate with large asteroids first */
            populateCell(cell, LargeAsteroid, 1);

            /* populate with medium asteroids */
            populateCell(cell, MedAsteroid, 1);

            /* small asteroids last */
            populateCell(cell, SmallAsteroid, 1);
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

                /* Set random large and medium asteroid ratios */
                properties.LargeAsteroidPercent = Random.Range(0, 100);
                properties.MedAsteroidPercent = Random.Range(0, properties.LargeAsteroidPercent);

                /* Place the cell in the world space, save it to list */ 
                newCell.transform.position = new Vector2(x * cellLength, y * cellLength);
                newCell.transform.parent = transform; 
                cells.Add(newCell);
                
            }
        }
    }

    private void populateCell(GameObject cell, GameObject item, int count)
    {
        int iteration = 0;
        int diameter; 
        Vector2 position;
        CircleCollider2D col = item.GetComponent<CircleCollider2D>();
        /* Get diameter of game object */
        if(col.radius < 1.0f)
        {
            diameter = 1; 
        }
        else
        {
            diameter = Mathf.CeilToInt(col.radius) * 2; 
        }

        while (iteration < count)
        {
            /* Keep looking for an empty space */
            do
            {
                /* Find a random, relative x and y coordinate */
                position.x = Random.Range(0, cellLength);
                position.y = Random.Range(0, cellLength);

            } while (!isSpaceFree((int)position.x, (int)position.y, diameter));

            /* Mark the spot as occupied */
            fillSpace((int)position.x, (int)position.y, diameter);
            iteration++; 

            /* instantiate and place the asteroid */
            GameObject newItem = (GameObject)Instantiate(item);
            newItem.transform.parent = cell.transform;
            newItem.transform.localPosition = position;
        }
    }

    private bool isSpaceFree(int pos_x, int pos_y, int diameter)
    {
        /* check bounds */ 
        /* Note: this check assumes that the anchor position 
         * is in the lower left corner  */ 
        if(pos_x + (diameter - 1) >= cellLength || pos_y + (diameter - 1) >= cellLength)
        {
            return false; 
        }

        for(int i = 0; i < diameter; i++)
        {
            for(int j = 0; j < diameter; j++)
            {
                /* if cell is occupied, space unavailable */ 
                if(tracker[pos_x + i, pos_y + j] == true)
                {
                    return false; 
                }
            }
        }

        /* space is available */
        return true; 
    }

    private void fillSpace(int pos_x, int pos_y, int diameter)
    {
        for(int i = 0; i < diameter; i++)
        {
            for(int j = 0; j < diameter; j++)
            {
                tracker[pos_x + i, pos_y + j] = true; 
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
