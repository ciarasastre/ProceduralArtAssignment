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

    //public Transform empty;
    public Transform childTarget;

    private Vector3 pos;
    public float speed;

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

    //Collisions
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cell")
        {
            StartCoroutine(VirusCreation());
        }
    }


    IEnumerator VirusCreation()
    {
        /*yield return new WaitForSeconds(2);
        //Save position
        Vector3 pos = transform.position;
        new GameObject("Virus Child").AddComponent<VirusMovement>().VirusStandBy(this, pos);*/

        //This creates 2, 3 secs at a time
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(3);
            //Save position
            Vector3 pos = transform.position;
            new GameObject("Virus Child").AddComponent<VirusMovement>().VirusDuplicate(this,pos);
        }

        // Destroy main virus when finished
        Destroy(gameObject);
    }

    void VirusDuplicate(VirusMovement parent, Vector3 pos)
    {

        transform.position = new Vector3(pos.x, pos.y, pos.z);

        mesh = parent.mesh;
        material = parent.material; //pass material reference

        target = parent.childTarget;


        speed = parent.speed;
        maxRotationSpeed = parent.maxRotationSpeed;
        twist = parent.twist;  
        //transform.parent = parent.transform; // nests it in parent
        
    }
    
}
