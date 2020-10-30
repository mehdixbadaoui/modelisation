using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using System;
using System.Globalization;
using System.Linq;
using System.Diagnostics;
using System.IO.IsolatedStorage;

public class buddah : MonoBehaviour
{
    public Material mat;

    Vector3[] vertices;
    int[] triangles;

    
    // Start is called before the first frame update
    void Start()
    {
        drawBuddha();
        gravityCenter();
        normalize();

        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }

    void drawBuddha()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        string text = System.IO.File.ReadAllText(@"D:\Documents\UNI\modelisation\buddha.off.txt");
        char[] delimiterChars = { ' ', '\n', '\t' };
        string[] vals = text.Split(delimiterChars);

        vertices = new Vector3[Int32.Parse(vals[1])];
        triangles = new int[Int32.Parse(vals[2]) * 3];


        int inc = 4;
        int erreurs = 0;

        for (int i = 0; i < vertices.Length; i++)
        {
            try
            {
                vertices[i] = new Vector3(float.Parse(vals[i + inc], NumberStyles.Float, CultureInfo.InvariantCulture),
                                          float.Parse(vals[i + inc + 1], NumberStyles.Float, CultureInfo.InvariantCulture),
                                          float.Parse(vals[i + inc + 2], NumberStyles.Float, CultureInfo.InvariantCulture));
                inc += 2;

            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(i);
                erreurs++;
            }
        }

        inc = 1;
        for (int i = 0, count = 1; i < triangles.Length; i++, count++)
        {
            triangles[i] = int.Parse(vals[i + vertices.Length * 3 + 4 + inc], NumberStyles.Integer, CultureInfo.InvariantCulture);
            //Debug.Log(vals[i + vertices.Length * 3 + 4 + inc]);
            if (count % 3 == 0)
            {
                inc += 2;
            }
        }



    }

    void gravityCenter()
    {
        float x = 0;
        float y = 0;
        float z = 0;

        foreach(Vector3 vertex in vertices)
        {
            x += vertex.x;
            y += vertex.y;
            z += vertex.z;
        }

        Vector3 offset =  new Vector3(x / vertices.Length, y / vertices.Length, z / vertices.Length);
        gameObject.transform.position += offset;

    }

    void normalize()
    {
        float max = 0;
        foreach(Vector3 vertex in vertices)
        {
            if (Mathf.Abs(vertex.x) > max) max = Mathf.Abs(vertex.x);
            if (Mathf.Abs(vertex.y) > max) max = Mathf.Abs(vertex.y);
            if (Mathf.Abs(vertex.z) > max) max = Mathf.Abs(vertex.z);
        }

        for(int i = 0; i< vertices.Length; i++)
        {
            vertices[i].x /= max;
            vertices[i].y /= max;
            vertices[i].z /= max;
        }
    }
}
