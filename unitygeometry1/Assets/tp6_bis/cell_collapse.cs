using System;
using UnityEngine;
using System.Globalization;


public class cell_collapse : MonoBehaviour
{
    public Material mat;

    Vector3[] vertices;
    int[] triangles;
    Mesh msh;

    private void Start()
    {
        string path = @"F:\School\Modelisation\modelisation\cube.off.txt";
        draw(path);
    }
    void draw(String path)
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        //string text = System.IO.File.ReadAllText(@"D:\Documents\UNI\modelisation\buddha.off.txt");
        string text = System.IO.File.ReadAllText(path);
        char[] delimiterChars = { ' ', '\n', '\t' };
        string[] vals = text.Split(delimiterChars);

        vertices = new Vector3[Int32.Parse(vals[1])];
        triangles = new int[Int32.Parse(vals[2]) * 3];

        Debug.Log($"{triangles.Length} triangles");
        int inc = 4;
        int erreurs = 0;

        for (int i = 0; i < vertices.Length; i++)
        {
            try
            {
                vertices[i] = new Vector3(float.Parse(vals[i + inc], NumberStyles.Any, CultureInfo.InvariantCulture),
                                          float.Parse(vals[i + inc + 1], NumberStyles.Any, CultureInfo.InvariantCulture),
                                          float.Parse(vals[i + inc + 2], NumberStyles.Any, CultureInfo.InvariantCulture)) * 1000;
                inc += 2;

            }
            catch (Exception e)
            {
                Debug.Log(i);
                erreurs++;
            }
        }

        inc = 1;
        for (int i = 0, count = 1; i < triangles.Length; i++, count++)
        {
            triangles[i] = int.Parse(vals[i + vertices.Length * 3 + 4 + inc], NumberStyles.Any, CultureInfo.InvariantCulture);
            if (count % 3 == 0)
            {
                inc += 2;
            }

        }

        msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;


    }

}
