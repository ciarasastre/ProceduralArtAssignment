using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainMesh : MonoBehaviour {

    public int resolution = 256;

    private Texture2D texture;

    private void Awake()
    {   
        //Create texture when componant awakens 
        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true); //true=yes we want mipmaps
        texture.name = "Terrain Texture";
        GetComponent<MeshRenderer>().material.mainTexture = texture;

        //Now to fill the texture
        FillTexture();

    }

    //Adds colour
    private void FillTexture()
    {
        float stepSize = 1f / resolution;

        for(int x = 0; x < resolution; x++)
        {
            for(int y = 0; y < resolution; y++)
            {
                texture.SetPixel(x, y, new Color(x * stepSize, y * stepSize, 0f)); // Color.red
            }
        }

        //Now apply that texture
        texture.Apply();
    }

    //Called directly after awake
    private void OnEnable()
    {
        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        texture.name = "Terrain Texture";

        //Get rid of default wrap mode
        texture.wrapMode = TextureWrapMode.Clamp;

        //Point Filtering
        texture.filterMode = FilterMode.Point;
        GetComponent<MeshRenderer>().material.mainTexture = texture;

        //Now to fill the texture
        FillTexture();
    }
    // Use this for initialization
    void Start() { 

         
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
