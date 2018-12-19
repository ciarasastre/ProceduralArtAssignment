using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unity auto add mesh filter and renderer
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Environment : MonoBehaviour {

    public int xSize, ySize, zSize;
    private Mesh mesh;
    private Vector3[] vertices;
    int[] triangles;

    //Generate Mesh as soon as object Awakens
    /*private void Awake()
    {
        Generate();
    }*/

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateEnvironment();
        UpdateEnvironment();
        
    }

    void CreateEnvironment()
    {
        //Create the Vertices

        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        //int i = 0; //Loop through vertices

        for (int i=0, z=0; z<=zSize; z++) //Loop through z axis
        {
            for (int x = 0; x <= xSize; x++) //Loop through x axis
            {
                //Can create perlin noise effect here:
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z); //zero plane
                i++;
            }
        }

        //Create triangles
        triangles = new int[xSize * zSize * 6]; //Must be multiples of 3

        //Keep count of triangles and vertices
        int vertCount = 0;
        int triCount = 0;

        //Create trianlges on the zAxis
        for(int z = 0; z < zSize; z++)
        {
            //Create triangles on xAxis
            for (int x = 0; x < xSize; x++)
            {
                //This chunk creates a square within the vertices
                triangles[triCount + 0] = vertCount + 0;
                triangles[triCount + 1] = vertCount + xSize + 1;
                triangles[triCount + 2] = vertCount + 1;

                triangles[triCount + 3] = vertCount + 1;
                triangles[triCount + 4] = vertCount + xSize + 1;
                triangles[triCount + 5] = vertCount + xSize + 2;

                vertCount++; // Takes position of vert and brings it one over
                triCount += 6; // Moves so its not iterating the same triangle
            }

            vertCount++; //Makes sure it moves onto new line not connected
        }

    }

    void UpdateEnvironment()
    {
        mesh.Clear();

        //Apply Meshes
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }


    //Draws the vertices points 
    /*private void OnDrawGizmos()
    {
        //Check wether array exists and jump out if it doesnt
        if (vertices == null)
        {
            return;
        }

        //Gizmos.color = Color.black;
        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }

        
    }*/
}
