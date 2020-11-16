using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class cylindre : MonoBehaviour
{
    public Material mat;
    public int edges, height, radius;

    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        int rotate = 360 / edges;
        vertices = new Vector3[2 * (edges + 1)];
        triangles = new int[12 * edges];

        Vector3 c = gameObject.transform.position;

        int angle = 0;
        vertices[0] = c;
        vertices[edges + 1] = c + new Vector3(0, height, 0);

        for (int i = 1; i <= edges; i++)
        {
            vertices[i] = new Vector3(radius * (float)Math.Cos(angle * Math.PI / 180), 0, radius * (float)Math.Sin(angle * Math.PI / 180));
            vertices[edges + 1 + i] = new Vector3(radius * (float)Math.Cos(angle * Math.PI / 180), height, radius * (float)Math.Sin(angle * Math.PI / 180));
            angle += rotate;

        }
        int start_bottom = 0;
        int start_top = edges + 1;

        int tri = 0;
        for (int i = 1; i < edges; i++)
        {
            triangles[tri] = start_bottom;
            triangles[tri + 2] = i + 1;
            triangles[tri + 1] = i;

            triangles[tri + 3] = start_top;
            triangles[tri + 4] = i + edges + 2;
            triangles[tri + 5] = i + edges + 1;

            triangles[tri + 6] = i;
            triangles[tri + 8] = i + edges + 2;
            triangles[tri + 7] = i + edges + 1;

            triangles[tri + 9] = i;
            triangles[tri + 10] = i + edges + 2;
            triangles[tri + 11] = i + 1;



            tri += 12;
        }

        triangles[tri] = start_bottom;
        triangles[tri + 1] = edges;
        triangles[tri + 2] = start_bottom + 1;

        triangles[tri + 3] = start_top;
        triangles[tri + 4] = start_top + 1;
        triangles[tri + 5] = 2 * edges + 1;

        triangles[tri + 6] = start_bottom + 1;
        triangles[tri + 7] = edges;
        triangles[tri + 8] = 2 * edges + 1;

        triangles[tri + 9] = start_bottom + 1;
        triangles[tri + 10] = 2 * edges + 1;
        triangles[tri + 11] = start_top + 1;


        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
