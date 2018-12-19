using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpyTerrain : MonoBehaviour
{

    public Transform pointPrefab;

    [Range(10, 100)]
    public int resolution = 90;

    Transform[] points;

    void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;

        Vector3 position;
        position.y = 0f;
        position.z = 0f;

        points = new Transform[resolution * resolution]; //Adjust for Z Grid


        for (int i = 0, z = 0; z < resolution; z++)
        {
            position.z = (z + 0.5f) * step - 1f; // New row made 

            for (int x = 0; x < resolution; i++, x++)
            {
                //Each time row is finished then reset
                /* if (x == resolution)
                 {
                     x = 0;
                     z += 1; // offset of z dimension
                 }*/

                Transform point = Instantiate(pointPrefab); //Instantiates prefab on that point
                position.x = (x + 0.5f) * step - 1f; // Takes a step over

                point.localPosition = position;
                point.localScale = scale;
                point.SetParent(transform, false); //Creating Clones
                points[i] = point;
            }
        }
    }

    void Update()
    {
        float t = Time.time;

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = MultiSine2DFunction(position.x, position.z, Time.time);
            //position = SphereDissolve(position.x, position.z, Time.time); //calls sphere dissolve
            point.localPosition = position;
        }

    }

    //Sphere Cell Dissolve only works because i failed at creating a working sphere
    static Vector3 SphereDissolve(float x, float z, float t)
    {
        Vector3 pos;
        float r = Mathf.Cos(Mathf.PI * 0.5f * z);
        pos.x = r * Mathf.Sin(Mathf.PI * x);
        //p.y = v;
        pos.y = Mathf.Sin(Mathf.PI * 0.5f * z);
        pos.z = r * Mathf.Cos(Mathf.PI * x);
        return pos;
    }

    //Bumpy Terrain
    static float MultiSine2DFunction(float x, float z, float t)
    {
        float y = 4f * Mathf.Sin(Mathf.PI * (x + z + t * 0.5f)); //Adjusts height

        //Following code iterates a wave like feature
        y += Mathf.Sin(Mathf.PI * (x + t));
        y += Mathf.Sin(2f * Mathf.PI * (z + 2f * t)) * 0.5f;
        y *= 1f / 5.5f;

        return y; //Gives back coordinate
    }

}


