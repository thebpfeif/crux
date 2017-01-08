using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
	}

    private void Move(Vector2 direction)
    {
        //Get ship's current position 
        Vector2 cur_pos = transform.position;

        cur_pos += direction * speed * Time.deltaTime;

        transform.position = cur_pos; 
    }

    public Vector2 getPosition()
    {
        return (transform.position);
    }
}
