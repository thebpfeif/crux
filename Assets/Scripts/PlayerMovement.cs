using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public Vector2 Accel;
    public float Dampener;
    public float MaxVelocity;

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

        if( input.x != 0)
        {
            /* calculate the thrust                     */
            /* Thrust = mass * acceleration * direction */
            thrust.x = (input.x * rb2d.mass * Accel.x);

            /* calculate the torque to be applied */
            torque = (thrust.x * Time.deltaTime);

            /* apply torque to the object */
            rb2d.AddTorque(torque);
        }

        if (input.y != 0)
        {
            /* calculate the thrust                     */
            /* Thrust = mass * acceleration * direction */
            thrust.y = (input.y * rb2d.mass * Accel.y);

            /* calculate the change in momentum (impulse) to be applied */
            /* momentum = mass * velocity */
            impulse.y = (thrust.y * Time.deltaTime);

            /* apply the impulse to the object */
            if( rb2d.velocity.y < MaxVelocity )
            {
            rb2d.AddRelativeForce(impulse);
            }
        }

        /* If there's no player input, bring ship to a stop */
        else if (rb2d.velocity.x != 0.0f || rb2d.velocity.y != 0.0f)
        {
            /* create a reductive force to slow down the ship */
            thrust = (-rb2d.velocity * rb2d.mass * Dampener);

            /* calculate the change in momentum (impulse) to be applied */
            /* momentum = mass * velocity */
            impulse = (thrust * Time.deltaTime);

            /* apply the impulse to the object */
            rb2d.AddForce(impulse * Time.deltaTime);

            /* If the ship is within a certain threshold, 
             * just bring it to a stop                    */
            if (rb2d.velocity.x > -0.10 && rb2d.velocity.x < 0.10)
            {
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }
            if (rb2d.velocity.y > -0.10 && rb2d.velocity.y < 0.10)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            }
        }
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
            //DestroyObject(gameObject);
        }
    }
}
