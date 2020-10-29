using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using System;
using System.Globalization;
using System.Linq;

public class buddah : MonoBehaviour
{
    public Material mat;

    Vector3[] vertices;
    int[] triangles;

    
    // Start is called before the first frame update
    void Start()
    {
        drawBuddha();
    }

    void drawBuddha()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        string text = System.IO.File.ReadAllText(@"D:\Documents\UNI\Geometrie unreal\buddha.off.txt");
        char[] delimiterChars = { ' ', '\n', '\t' };
        string[] vals = text.Split(delimiterChars);

        //Debug.Log(string.Format("Contents of WriteText.txt = {0}", text));


        vertices = new Vector3[Int32.Parse(vals[1])];
        triangles = new int[Int32.Parse(vals[2]) * 3];

        //Debug.Log("vert len "+vertices.Length);

        int inc = 4;
        int erreurs = 0;

        //VETRICES
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
                Debug.Log(i);
                erreurs++;
            }
        }
        //Debug.Log(vertices[vertices.Length - 1].z);

        //TRIANGLES
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

        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;


    }
}
