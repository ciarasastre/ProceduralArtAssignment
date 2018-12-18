using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour {

    Vector2 look;
    Vector2 smooth; //Smooths down movement of mouse
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    GameObject controller;
    
	void Start ()
    {
        controller = this.transform.parent.gameObject;
	}
	
	void Update ()
    {
        //Controlls mouse movement
        var mouseM = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseM = Vector2.Scale(mouseM, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        //smoothing vector slows movement
        smooth.x = Mathf.Lerp(smooth.x, mouseM.x, 1f / smoothing); //Prevents it from jolting from 1 movement to another
        smooth.y = Mathf.Lerp(smooth.y, mouseM.y, 1f / smoothing);
        look += smooth; // Apply to controller


        transform.localRotation = Quaternion.AngleAxis(-look.y, Vector3.right); //-look.y Inverts it to look up and down
        //When hes turning whole "body" turn with him
        controller.transform.localRotation = Quaternion.AngleAxis(look.x, controller.transform.up);
    }
}
