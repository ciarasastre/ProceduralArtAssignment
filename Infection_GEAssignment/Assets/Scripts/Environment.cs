using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unity auto add mesh filter and renderer
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Environment : MonoBehaviour {

    public int xSize, ySize;
    private Mesh mesh;

    //Generate Mesh as soon as object Awakens
    private void Awake()
    {
        Generate();
    }

    private Vector3[] vertices;

    private void Generate()
    {
        //Create Mesh
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Environment";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];

        //Position vertices
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for(int x =0; x<xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);
            }
        }

        //Apply Mesh
        mesh.vertices = vertices;

        //Start on triangles
        int[] triangles = new int[3]; //Must be multiples of 3
        triangles[0] = 0;
        triangles[1] = xSize + 1;
        triangles[2] = 1;
        mesh.triangles = triangles;
    }

    //Draws the vertices points 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }

        //Check wether array exists and jump out if it doesnt
        if(vertices == null)
        {
            return;
        }
    }
}
