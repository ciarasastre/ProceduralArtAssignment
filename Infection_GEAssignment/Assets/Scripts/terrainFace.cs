using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainFace {

    Mesh mesh;
    int resolution; // how detailed it needs to be
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public terrainFace(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x); // Find direction it is facing (Outwards)
        axisB = Vector3.Cross(localUp, axisA); // Find Perpendicular
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        //Find out how many triangles are in our Mesh
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];

        int triIndex = 0;

        for (int x = 0; x<resolution; x++)
        {
            for (int y = 0; y<resolution; y++)
            {
                int i = x + y * resolution; //Number of iterations on loop
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                //Find center of square
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                vertices[i] = pointOnUnitCube;

                //Create Triangles
                if(x != resolution - 1 && y != resolution -1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = 1;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;
                    triIndex += 6;

                }
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }


    

}
