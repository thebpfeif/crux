using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

    GameObject Playership; 

    float speed_bullet;
    const float max_travel_dist = 20.0f;  

	// Use this for initialization
	void Start ()
    {
        Playership = GameObject.FindWithTag("PlayerShip");
        speed_bullet = 5.0f;     
	}
	
	// Update is called once per frame
	void Update ()
    {
        /* Get current position */
        Vector2 position = transform.position;

        /* compute laser's new position */
        //BRP TODO: Right now this just assumes we are shooting along the y-axis 
        //Need to make it shoot relative to the gun's position on the ship. 
        position = new Vector2(position.x, position.y + (speed_bullet * Time.deltaTime));

        /*update the laser's position*/
        transform.position = position;

        /*check to see if the laser has reached its max distance. If so, destroy.*/
        Vector2 shipPosition = Playership.GetComponent<PlayerMovement>().getPosition();

        if( Mathf.Abs( shipPosition.y - position.y ) >= max_travel_dist
          || Mathf.Abs( shipPosition.x - position.x ) >= max_travel_dist )
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        /* If laser collides with anything other 
         * than player ship object (initial position), 
         * then destroy object                          */ 
        if( collider.tag != "PlayerShip")
        {
            DestroyObject(gameObject);
        }
    }
}
