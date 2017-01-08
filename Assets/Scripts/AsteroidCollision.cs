using UnityEngine;
using System.Collections;

public class AsteroidCollision : MonoBehaviour {

    public GameObject ItemDropGO; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "PlayerLaser")
        {
            /*get position of asteroid before destroying*/
            Vector2 asteroid_pos = transform.position;

            /* Destroy asteroid */ 
            DestroyObject(gameObject);

            /*Generate items for the player to pick up*/
            GameObject item = (GameObject)Instantiate(ItemDropGO);

            /* Set item drop location equal to asteroid's position*/ 
            item.transform.position = asteroid_pos;

        }
    }
}
