using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    //public float RotateVelocity;
    //public float forwardAccel;  
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
        Vector2 input; 
        float torque;

        /* We don't apply an impulse along the horizontal, so zero it out */
        impulse.x = 0;

        /* get player input */
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        /* normalize since we only care about direction, not length */
        input = input.normalized;

        /* calculate the thrust                     */
        /* Thrust = mass * acceleration * direction */
        thrust.x = (input.x * rb2d.mass * accel.x);
        thrust.y = (input.y * rb2d.mass * accel.y);

        /* calculate the change in momentum (impulse) to be applied */
        /* momentum = mass * velocity */
        impulse.y = (thrust.y * Time.deltaTime);

        /* calculate the torque to be applied */
        torque = (thrust.x * Time.deltaTime);

        /* apply the impulse to the object */
        rb2d.AddRelativeForce(impulse);

        /* apply torque to the object */
        rb2d.AddTorque(torque);

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Asteroid")
        {
            DestroyObject(gameObject);
        }
    }
}
