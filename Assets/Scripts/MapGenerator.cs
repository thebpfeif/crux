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
                //GameObject newAsteroid = (GameObject)Instantiate(Asteroid);
                //newAsteroid.transform.parent = cell.transform; 
                //newAsteroid.transform.localPosition = asteroidPosition;

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

    private void populateCell( GameObject cell, GameObject item, bool[,] tracker, int count )
    {
        int iteration = 0;
        Vector2 position;
        CircleCollider2D col = item.GetComponent<CircleCollider2D>();
        /* Get diameter of game object */
        int diameter = Mathf.CeilToInt(col.radius) * 2; 

        while (iteration < count)
        {
            /* Keep looking for an empty space */
            do
            {
                /* Find a random, relative x and y coordinate */
                position.x = Random.Range(0, cellLength);
                position.y = Random.Range(0, cellLength);

            } while (!isSpaceFree(tracker, (int)position.x, (int)position.y, diameter));

            /* Mark the spot as occupied */
            tracker[(int)position.x, (int)position.y] = true;
            iteration++; 

            /* instantiate and place the asteroid */
            GameObject newItem = (GameObject)Instantiate(item);
            newItem.transform.parent = cell.transform;
            newItem.transform.localPosition = position;
        }
    }

    private bool isSpaceFree( bool[,] buffer, int pos_x, int pos_y, int diameter )
    {
        if (diameter == 1 )
        {
            return buffer[pos_x, pos_y] == false;
        }
        else if( diameter == 2 )
        {
            /* check bounds */ 
            /* Note: The lower left cell is always the anchor, 
             * so we can assume we'll never fall out of bounds 
             * below 0.                                      */
            if( pos_x + 1 >= cellLength || pos_y + 1 >= cellLength )
            {
                return false; 
            }

            /* if any of the spaces are occupied, space unavailable */ 
            else if( buffer[pos_x, pos_y] == true || buffer[pos_x + 1, pos_y] == true
                || buffer[pos_x, pos_y + 1] == true || buffer[ pos_x + 1, pos_y + 1] == true )
            {
                return false;
            }
            else
            {
                return true; 
            }
        }
        else if( diameter == 3 )
        {
            /* check bounds */ 
            if( pos_x + 2 >= cellLength || pos_y + 2 >= cellLength )
            {
                return false; 
            }

            /* if any of the spaces are occupied, space unavailable */
            else if (buffer[pos_x, pos_y] == true || buffer[pos_x + 1, pos_y] == true || buffer[pos_x + 2, pos_y]
                || buffer[pos_x, pos_y + 1] == true || buffer[pos_x + 1, pos_y + 1] == true || buffer[pos_x + 2, pos_y + 1] == true 
                || buffer[pos_x, pos_y + 2] == true || buffer[pos_x + 1, pos_y + 2] == true || buffer[pos_x + 2, pos_y + 2] == true )
            {
                return false; 
            }
            else
            {
                return true; 
            }
        }
        else
        {
            Debug.Log("Diameter length not supported");
            return false; 
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
