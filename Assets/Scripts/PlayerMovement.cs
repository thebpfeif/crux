using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Vector2 accel; 

    private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>(); 
	}
	
	// Update is called once per frame
	void Update () {

        /* local variables */
        Vector2 impulse;
        Vector2 thrust;

        /* get player input */ 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        /* normalize since we only care about direction, not length */ 
        Vector2 direction = new Vector2(x, y).normalized;

        /* calculate the thrust                     */
        /* Thrust = mass * acceleration * direction */
        thrust.x = ( direction.x * rb2d.mass * accel.x );
        thrust.y = ( direction.y * rb2d.mass * accel.y );

        /* calculate the change in momentum (impulse) to be applied */
        /* momentum = mass * velocity */ 
        impulse.x = ( thrust.x * Time.deltaTime );
        impulse.y = ( thrust.y * Time.deltaTime );

        /* apply the impulse to the object */
        rb2d.AddForce( impulse );
	}

    public Vector2 getPosition()
    {
        return (transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Asteroid")
        {
            DestroyObject(gameObject);
        }
    }
}
