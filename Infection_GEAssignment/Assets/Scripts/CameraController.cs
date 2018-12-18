using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float speed = 10.0f;

	// Use this for initialization
	void Start ()
    {
        //Takes cursor away to view game
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        //This moves the body
        float vert = Input.GetAxis("Vertical") * speed;
        float hor = Input.GetAxis("Horizontal") * speed;
        float up = Input.GetAxis("Jump") * speed; // Controls up and down

        vert *= Time.deltaTime;
        hor *= Time.deltaTime;
        up *= Time.deltaTime;

        transform.Translate(hor, up, vert);

        //Get your cursor back
        if(Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
