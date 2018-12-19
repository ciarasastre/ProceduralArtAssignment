using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMovement : MonoBehaviour {

    public Mesh mesh;
    public Material material;
    public float twist = 20;
    public float maxRotationSpeed = 20;
    private float rotationSpeed;

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
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
