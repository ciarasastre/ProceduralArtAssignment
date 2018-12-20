using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFrac : MonoBehaviour
{
    //Used for positioning the chid objects
    private static Vector3[] childPositions = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    //Used for rotating the child objects to point away from parent
    private static Quaternion[] childOrientations = {
        Quaternion.identity, //up child
        Quaternion.Euler(0f, 0f, -90f), //right child
        Quaternion.Euler(0f, 0f, 90f), // left child
        Quaternion.Euler(90f, 0f, 0f), //forward
        Quaternion.Euler(-90f, 0f, 0f) //backward
    };

    //Fractal
    public Mesh[] meshes;
    public Material material;

    //Signal
    public Mesh sigMesh;
    public Material sigMat;

    public float speed;
    public Transform childTarget;

    private int maxDepth = 2;
    private float childSize = 2;
    private float maxTwist = 20;

    private float maxRotationSpeed = 20;
    private float rotationSpeed;

    private int depth;

    private Material[,] materials;

    private void InitializeMaterials()
    {
        // Adding colours
        materials = new Material[maxDepth + 1, 2];

        //Changes colour per depth
        for (int i = 0; i <= maxDepth; i++)
        {
            //Inner colours (dont matter)
            materials[i, 0] = new Material(material);
            materials[i, 1] = new Material(material);
        }
        //Outside colours
        materials[maxDepth, 0].color = Color.blue;
        materials[maxDepth, 1].color = Color.cyan;
    }

    private void Start()
    {
        //This prevents it from jolting
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);

        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);

        //If there are no colours then fill
        if (materials == null)
        {
            InitializeMaterials();
        }

        //Add Meshes to create main sphere
        gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range(0, 2)];

        // EG There will be about 4 children for every 1 parent
        if (depth < maxDepth)
        {
            for (int i = 0; i < childPositions.Length; i++)
            {
                new GameObject("Fractal Child").AddComponent<CellFrac>().Initialize(this, i);
            }
        }
    }


    //Creates Sphere child that attatches to parent
    private void Initialize(CellFrac parent, int childIndex)
    {
        meshes = parent.meshes;
        materials = parent.materials; //pass material reference
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;

        maxRotationSpeed = parent.maxRotationSpeed;
        maxTwist = parent.maxTwist;

        transform.parent = parent.transform; // nests it in parent

        //Moves the childs position "Up"[directions] so that it is above and surrounding parent
        transform.localScale = Vector3.one * childSize;
        transform.localPosition = childPositions[childIndex] * (0.5f + 0.5f * childSize);

        transform.localRotation = childOrientations[childIndex];
    }

    //Rotates the entire cell
    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    //Collisions
    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.tag == "Virus")
        {
            //Send signal
            Debug.Log("HELP");

            //Save position
            Vector3 pos = transform.position;
            new GameObject("Signal").AddComponent<CellFrac>().SendSignal(this, pos);
        }*/

        if (collision.gameObject.tag == "Helpers")
        {
            // If the Cell gets touched by a helper it self destructs
            Destroy(gameObject);
            Debug.Log("Destroyed Cell");
        }
    }

    //Signal
    /*void SendSignal(CellFrac parent, Vector3 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, pos.z);

        sigMesh = parent.sigMesh;
        sigMat = parent.sigMat; //pass material reference

        childTarget = parent.childTarget;


        speed = parent.speed;
        //maxRotationSpeed = parent.maxRotationSpeed;
        //twist = parent.twist;
    }*/

}

