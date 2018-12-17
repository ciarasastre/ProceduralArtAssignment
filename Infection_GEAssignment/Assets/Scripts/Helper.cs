using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

    [Range(2, 256)] // Maximum range 256 squared
    public int resolution = 10;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    terrainFace[] terrainFaces;

    private void OnValidate()
    {
        Initialize();
        GenerateMesh();
    }
    void Initialize()
    {
        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        
        terrainFaces = new terrainFace[6];

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back}; 

        for(int i=0; i < 6; i++)
        {
            if(meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }
            

            terrainFaces[i] = new terrainFace(meshFilters[i].sharedMesh, resolution, directions[i]);
        }

    }

    void GenerateMesh()
    {
        foreach (terrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }
}
