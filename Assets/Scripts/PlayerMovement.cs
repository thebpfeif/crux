using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float RotateVelocity;
    public float ForwardVelocity;  

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
        float torque;

        /* We don't apply an impulse along the horizontal, so zero it out */
        impulse.x = 0; 

        /* get player input */ 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        /* normalize since we only care about direction, not length */ 
        Vector2 direction = new Vector2(x, y).normalized;

        transform.Rotate(0, 0, direction.x * RotateVelocity);

        if( direction.y != 0 )
        {
        transform.position += transform.up * Time.deltaTime * ForwardVelocity * direction.y;
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
}
