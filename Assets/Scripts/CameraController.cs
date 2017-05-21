using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset; 

	// Use this for initialization
	void Start ()
    {
        offset = transform.position - player.transform.position; 
	}
	
	// LateUpdate is called once per frame but not until all other 
    // items have been processed in Update() 
	void LateUpdate ()
    {
        transform.position = player.transform.position + offset;

        float y = Input.mouseScrollDelta.y;
        zoomCamera(y);
	}

    void zoomCamera(float input)
    {
        float currSize = Camera.main.orthographicSize; 

        if(input == 0)
        {
            return; 
        }

        else if(input >= 1)
        {
            currSize -= 1;     
        }

        else
        {
            currSize += 1; 
        }

        currSize = Mathf.Clamp(currSize, 0, 100);

        Camera.main.orthographicSize = currSize; 
    }
}
