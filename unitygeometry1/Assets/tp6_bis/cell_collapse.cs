using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System.Globalization;


public static class Utils
{
    public static bool IsAny<T>(this IEnumerable<T> data)
    {
        return data != null && data.Any();
    }
}

public class cell_collapse : MonoBehaviour
{
    public Material mat;

    Vector3[] vertices;
    int[] triangles;
    Mesh msh;
    public int grid_size;

    private Dictionary <Vector3, Vector3> vec_cell;

    private void Start()
    {
        string path = @"F:\School\Modelisation\modelisation\bunny.off.txt";
        draw(path);
        vec_cell = new Dictionary<Vector3, Vector3>();
        grid_size = 32;
        grid();
        
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

        //msh = new Mesh();

        //msh.vertices = vertices;
        //msh.triangles = triangles;

        //gameObject.GetComponent<MeshFilter>().mesh = msh;
        //gameObject.GetComponent<MeshRenderer>().material = mat;


    }

    void grid()
    {
        foreach (Vector3 vec in vertices)
        {
            vec_cell.Add(vec, new Vector3(vec.x % grid_size, vec.y % grid_size, vec.z % grid_size));
        }

        IEnumerable<Vector3> matches = vec_cell.Where(o => o.Value == new Vector3(10, 10, 10))
                  .Select(o => o.Key);


        int count = 0;
        Vector3 avg = Vector3.zero;

        Debug.Log(matches != null /*&& matches.Any()*/);
        foreach (Vector3 match in matches)
        {
            Debug.Log(match);

            avg += match;
            count++;
        }
        avg /= count;
        Debug.Log(count);

    }

}
