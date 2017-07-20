using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public  GameObject          CellGO;         /* Cell Prefab                  */

    public List<GameObject> SmallAsteroids;
    
    public  int                 MapHeight;      /* Map height, in cells         */ 
    public  int                 MapWidth;       /* Map width, in cells          */
                                                /* cell side length (square)    */
    private List<GameObject>    cells = new List<GameObject>();
                                                /* List of map cells            */
    private bool[,] tracker;
                                                /* tracks free space in cells   */
    private int                 cellLength = 100;
                                                /* length of each cell (square) */

    void Start ()
    {
        generateCells();
        generateAsteroids(); 

    }
	
	void Update ()
    {
		
	}

    private void generateAsteroids()
    {
        CellProperties properties;
        int asteroidCount;
        //BRP TODO: Calculate these at runtime
        int largeAsteroidSize = 9; 
        int medAsteroidSize = 4;
        int smallAsteroidSize = 1;

        foreach( GameObject cell in cells )
        {
            /* reset the tracker for new cell */ 
            tracker = new bool[cellLength, cellLength];

            /* Get the cell position */
            Vector2 cellPosition = cell.transform.position;

            /* Get the cell properties */
            properties = cell.GetComponent<CellProperties>();

            /* calculate the counts of each type of asteroid */ 
            asteroidCount = Mathf.FloorToInt((properties.LargeAsteroidPercent * properties.AvailableCellCount) / largeAsteroidSize);

            /* populate with large asteroids first */
            //populateCell(cell, LargeAsteroid, asteroidCount);
            asteroidCount = Mathf.FloorToInt((properties.MedAsteroidPercent * properties.AvailableCellCount) / medAsteroidSize);

            /* populate with medium asteroids */
            //populateCell(cell, MedAsteroid, asteroidCount);
            asteroidCount = Mathf.FloorToInt((properties.SmallAsteroidPercent * properties.AvailableCellCount) / smallAsteroidSize);

            /* small asteroids last */
            //populateCell(cell, SmallAsteroid, asteroidCount);
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
                properties.AsteroidDenstiy = Random.Range(0, 50);

                /* Set random large and medium asteroid ratios */
                properties.LargeAsteroidPercent = Random.Range(0, 100);

                properties.MedAsteroidPercent = Random.Range(0, 100 - (int)(properties.LargeAsteroidPercent * 100));

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
        float scale = item.transform.lossyScale.x;
        float scaledRadius = col.radius * scale; 
        /* Get diameter of game object */
        if(scaledRadius < 1.0f)
        {
            diameter = 1; 
        }
        else
        {
            diameter = Mathf.CeilToInt(col.radius * scale * 2); 
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

            /* offset the x and y axis by the radius of the object
             * to ensure its anchored by the lower left position */
            position += new Vector2(scaledRadius, scaledRadius);

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
}
