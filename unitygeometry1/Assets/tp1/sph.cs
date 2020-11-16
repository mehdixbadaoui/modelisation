using UnityEngine;

public class sph : MonoBehaviour
{
    public int edges, layers, tronc;

    Vector3[] vertices;
    int[] triangles;

    public Material mat;

    private float ang_1, ang_2;

    void Start()
    {
        gameObject.AddComponent<MeshFilter>();          
        gameObject.AddComponent<MeshRenderer>();

        sphere();

        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;

    }

    private void sphere()
    {
        tronc = layers - tronc;

        vertices = new Vector3[(tronc + 1) * (edges + 1) + 1];
        triangles = new int[(tronc * edges * 2 + 2 * edges) * 3];


        int index = 0;
        vertices[(tronc + 1) * (edges + 1)] = new Vector3(0, 0, 0);
        for (int i = 0; i <= edges; i++)
        {
            ang_1 = -Mathf.PI / 2 + i * Mathf.PI / edges;
            for (int j = 0; j <= tronc; j++)
            {
                ang_2 = j * 2 * Mathf.PI / layers;

                float x = Mathf.Cos(ang_2) * Mathf.Cos(ang_1);
                float y = Mathf.Sin(ang_2) * Mathf.Cos(ang_1);
                float z = Mathf.Sin(ang_1);

                vertices[index] = new Vector3(x, y, z);
                index++;
            }
        }

        int k = 0;
        for (int j = 0; j < edges; j++)
        {
            int r1 = j * (tronc + 1);
            int r2 = (j + 1) * (tronc + 1);
            for (int i = 0; i < tronc; i++)
            {
                triangles[k] = r1 + i;
                triangles[k + 1] = r2 + i + 1;
                triangles[k + 2] = r2 + i;

                triangles[k + 3] = r1 + i;
                triangles[k + 4] = r1 + i + 1;
                triangles[k + 5] = r2 + i + 1;

                k += 6;
            }
        }

        for (int i = 0; i < edges; i++)
        {

            triangles[k++] = (i * (tronc + 1)) + tronc + 1;
            triangles[k++] = ((tronc + 1) * (edges + 1));
            triangles[k++] = (i * (tronc + 1));

            triangles[k++] = ((i * (tronc + 1))) + tronc;
            triangles[k++] = ((tronc + 1) * (edges + 1));
            triangles[k++] = ((i * (tronc + 1)) + tronc + 1) + tronc;

        }


    }

}