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
    /* Percent of large asteroids */
    public int LargeAsteroidPercent
    {
        get
        {
            if(AsteroidDenstiy == 0)
            {
                return 0; 
            }
            else
            {
                return (AsteroidDenstiy * (LargeAsteroidPercent / 100));
            }
        }
        set
        {
            LargeAsteroidPercent = value; 
        }
    }
    /* Percent of medium asteroids */
    public int MedAsteroidPercent
    {
        get
        {
            if(AsteroidDenstiy == 0)
            {
                return 0; 
            }
            else
            {
                return (AsteroidDenstiy * (MedAsteroidPercent / 100));
            }
        }
        set
        {
            MedAsteroidPercent = value; 
        }
    }


}
