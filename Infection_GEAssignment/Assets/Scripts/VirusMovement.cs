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
            // Creates a 4 new virus in one burst
            //for (int i = 0; i < 4; i++)
            //{
                //Save position
                //Vector3 pos = transform.position;
                StartCoroutine(VirusCreation());
           // }
        }
    }

    IEnumerator VirusCreation()
    {
        /*yield return new WaitForSeconds(2);
        //Save position
        Vector3 pos = transform.position;
        new GameObject("Virus Child").AddComponent<VirusMovement>().VirusStandBy(this, pos);*/

        //This creates 4, 2 secs at a time
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(3);
            //Save position
            Vector3 pos = transform.position;
            new GameObject("Virus Child").AddComponent<VirusMovement>().VirusStandBy(this,pos);
        }
    }

    void VirusStandBy(VirusMovement parent, Vector3 pos)
    {

        transform.position = new Vector3(pos.x, pos.y, pos.z);

        mesh = parent.mesh;
        material = parent.material; //pass material reference
        

        target = parent.childTarget;


        speed = parent.speed;
        maxRotationSpeed = parent.maxRotationSpeed;//parent.maxRotationSpeed;
        twist = parent.twist;  // parent.twist;

        /*bool standby = true;

        if(standby == true)
        {
            Debug.Log("its true");
            float speed2 = 5f;
            //adjust this to change how high it goes
            float height = 0.5f;

            //get the Virus current position
            Vector3 pos = transform.position;

            //calculate what the new Y position will be
            float newY = Mathf.Sin(Time.time * speed2);

            //set the object's Y to the new calculated Y
            transform.position = new Vector3(pos.x, newY, pos.z) * height;
        }
        else
        {
            // Get Destoyed orfind new cell
        }*/

        
    }

    
}
