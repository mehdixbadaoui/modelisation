using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour
{
    public Material mat;
    public int columns, rows;

    Vector3[] vertices;
    int[] triangles;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        int yrows = rows;
        int xcols = columns;
        vertices = new Vector3[(yrows + 1) * (xcols + 1)];
        triangles = new int[6 * yrows * xcols];

        int v = 0;
        for (int x = 0; x <= xcols; x++)
        {
            for (int y = 0; y <= yrows; y++)
            {
                vertices[v] = new Vector3(y, x, 0);
                v++;
            }

        }

        int vert = 0;
        int tri = 0;

        for (int i = 0; i < xcols; i++)
        {
            for (int j = 0; j < yrows; j++)
            {

                triangles[tri] = vert; Debug.Log(tri + " " + vert);
                triangles[tri + 1] = vert + 1; Debug.Log((tri + 1) + " " + (vert + 1));
                triangles[tri + 2] = vert + yrows + 1; Debug.Log((tri + 2) + " " + (vert + yrows + 1));
                triangles[tri + 3] = vert + yrows + 1; Debug.Log((tri + 3) + " " + (vert + yrows + 1));
                triangles[tri + 4] = vert + 1; Debug.Log((tri + 4) + " " + (vert + 1));
                triangles[tri + 5] = vert + yrows + 2; Debug.Log((tri + 5) + " " + (vert + yrows + 2));

                vert++;
                tri += 6;

            }
            vert++;
        }

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
