using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSquad : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public Transform target;
    //public Transform childTarget;
    private Vector3 pos;
    public float speed;

    // Use this for initialization
    void Start ()
    {
        //Add Meshes to create virus material and mesh from blender
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

    }
	
	// Update is called once per frame
	void Update ()
    {
        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    //Collisions
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cell")
        {
            // If the helper touches the Cell it self destructs

            Destroy(gameObject);
            Debug.Log("Destroyed Helper");
        }
    }
}
