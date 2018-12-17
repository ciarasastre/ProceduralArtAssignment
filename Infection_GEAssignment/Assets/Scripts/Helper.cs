using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour {

    [Range(2, 256)] // Maximum range 256 squared
    public int resolution = 10;

    // Add shape settings
    public ShapeSettings shapeSettings;

    ShapeGenerator shapeGenerator;


    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    terrainFace[] terrainFaces;

    private void OnValidate()
    {
        GenerateSphere();
    }
    void Initialize()
    {
        shapeGenerator = new ShapeGenerator(shapeSettings);

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
            

            terrainFaces[i] = new terrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);
        }

    }

    //Generate entire sphere here
    public void GenerateSphere()
    {
        Initialize();
        GenerateMesh();
    }

    //Only on shape change call this method
    public void OnShapeSettingUpdated()
    {
        Initialize();
        GenerateMesh();
    }

    //Generate triangle mesh here
    void GenerateMesh()
    {
        foreach (terrainFace face in terrainFaces)
        {
            face.ConstructMesh();
        }
    }
    
}
