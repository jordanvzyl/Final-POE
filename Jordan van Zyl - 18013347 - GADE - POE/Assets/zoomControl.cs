using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomControl : MonoBehaviour {

    public float zoomSize = 51.2f;
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        transform.position = pos;


		if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(zoomSize > 5)
            {
                zoomSize -= 1;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(zoomSize < 55)
            {
                zoomSize += 1;
            }
        }
        GetComponent<Camera>().orthographicSize = zoomSize;
    }
}
