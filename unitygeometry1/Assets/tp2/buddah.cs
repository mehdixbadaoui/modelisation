using UnityEngine;
using System;
using System.Globalization;
using System.IO;

public class buddah : MonoBehaviour
{
    public Material mat;

    Vector3[] vertices;
    int[] triangles;
    int[] triangles2;
    Mesh msh;

    // Start is called before the first frame update
    void Start()
    {
        string path = @"F:\School\Modelisation\modelisation\buddha.off.txt";
        draw(path);

        gravityCenter();
        normalize();
        
        slice(1000);

        export(@"D:\Documents\UNI\modelisation\export.txt");
        export(@"D:\Documents\UNI\modelisation\export.off");
    }

    public void draw(String path)
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

    void slice(int n)
    {
        triangles2 = new int[triangles.Length - 3*n];
        for(int i = 0; i < triangles2.Length; i++)
        {
            triangles2[i] = triangles[i];
        }

        msh.triangles = triangles2;
    }

    void export(string filename)
    {
        using (FileStream fs = File.Create(filename))
        {
        }
        using (StreamWriter sw = File.AppendText(filename))
        {
            sw.WriteLine("OFF");
            sw.WriteLine($"{vertices.Length} {triangles2.Length / 3} 0");
        }

        //File.WriteAllText(filename, "OFF\n");
        //File.WriteAllText(filename, $"{vertices.Length} {triangles.Length / 3} 0\n");
        for (int i = 0; i < vertices.Length; i++){
            using (StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine($"{vertices[i].x} {vertices[i].y} {vertices[i].z}");
            }

        }

        for (int i = 0; i < triangles2.Length; i++){
            using (StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine($"3 {triangles2[i]} {triangles2[i + 1]} {triangles2[i + 2]} ");
            }
            i += 2;
        }

    }
}
