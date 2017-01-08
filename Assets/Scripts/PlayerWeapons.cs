using UnityEngine;
using System.Collections;

public class PlayerWeapons : MonoBehaviour {

    public GameObject LaserGO; 
    public GameObject CannonFrontLeft;
    public GameObject CannonFrontRight; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        /* fire lasers when 'E' is pressed*/ 
        if(Input.GetKeyDown(KeyCode.E))
        {
            /*instantiate front lasers*/
            GameObject laserleft = (GameObject)Instantiate(LaserGO);
            GameObject laserright = (GameObject)Instantiate(LaserGO);

            /* initialize laser positions*/
            laserleft.transform.position = CannonFrontLeft.transform.position;
            laserright.transform.position = CannonFrontRight.transform.position; 
        }


	}
}
