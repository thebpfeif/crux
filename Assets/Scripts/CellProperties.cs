using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellProperties : MonoBehaviour {

    public Vector2 VertexBottomLeft { get; set; }
    public Vector2 VertexBottomRight { get; set; }
    public Vector2 VertexTopLeft { get; set; }
    public Vector2 VertexTopRight { get; set;}
    public Vector2 CellCoordinates { get; set; }
    public int AsteroidDenstiy { get; set; }
    public int AvailableCellCount
    {
        get
        {
            return Mathf.FloorToInt(( AsteroidDenstiy / 100.0f ) * cellCount);
        }
    }


    /* Percent of large asteroids */
    public float LargeAsteroidPercent
    {
        get
        {
            return ((float)AsteroidDenstiy / 100.0f) * largeAsteroidPercent;
        }
        set
        {
            largeAsteroidPercent = value / 100.0f; 
        }
    }
    /* Percent of medium asteroids */
    public float MedAsteroidPercent
    {
        get
        {
            return ((float)AsteroidDenstiy / 100.0f) * medAsteroidPercent;
        }
        set
        {
            medAsteroidPercent = value / 100.0f; 
        }
    }

    public float SmallAsteroidPercent
    {
        get
        {
            /* small asteroids get whatever is left over */ 
            float percent = 1.0f - (largeAsteroidPercent + medAsteroidPercent);
            percent = Mathf.Clamp(percent, 0.0f, 1.0f);
            return percent; 
        }
    }

    //BRP TODO: Derive this from LineRenderer class
    private int cellCount = 10000; 
    private float largeAsteroidPercent;
    private float medAsteroidPercent;

}
