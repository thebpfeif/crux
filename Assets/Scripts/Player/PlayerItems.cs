using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerItems : MonoBehaviour {

    private float itemCount;
    public Text countText; 

	// Use this for initialization
	void Start () {
        itemCount = 0;
        SetCountText(); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        /* If we encounter an item, pick it up */
        if (collider.tag == "ItemPickup")
        {
            itemCount++;
            SetCountText();
        }
    }

    void SetCountText ()
    {
        countText.text = "Item Count: " + itemCount.ToString();
    }
}
