using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMovement : MonoBehaviour {

    public Mesh mesh;
    public Material material;
    public float twist = 20;
    public float maxRotationSpeed = 20;
    private float rotationSpeed;
    
    public Transform target;
    public float speed;

    float speed2 = 5f;
    //adjust this to change how high it goes
    float height = 0.5f;

    // Use this for initialization
    void Start ()
    {
        //Add Meshes to create virus material and mesh from blender
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        //Start spinning
        //This prevents it from jolting
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        transform.Rotate(Random.Range(-twist, twist), 0f, 0f);
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        //Allows it to spin
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        //transform.position += Vector3.forward * Time.deltaTime;

        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void VirusStandBy()
    {
        //get the Virus current position
        Vector3 pos = transform.position;

        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed2);

        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, newY, pos.z) * height;
    }
}
