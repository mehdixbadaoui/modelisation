using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Hello_Triangle : MonoBehaviour
{

    public Material mat;

    // int yrows = 3;
    // int xcols = 4;
    public int rayon, nparallele, nmeridien,ntronc;

        Vector3[] vertices;
        int[] triangles;


    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();


        // plan(4, 3);

        // circle(new Vector3(0, 0, 0), 3, 4);

        // cylindre(new Vector3(0, 0, 0), 3, 60, 10);

        //cone(new Vector3(0, 0, 0), 3, 20, 5);

        sphere(new Vector3(0, 0, 0), 4, 8);

        Mesh msh = new Mesh();

        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;


    }

    void plan(int c, int r){
        
        int yrows = r;
        int xcols = c;
        vertices = new Vector3[(yrows+1)* (xcols+1)];
        triangles = new int[6 * yrows * xcols];

        int v = 0;
        for(int x = 0; x <= xcols; x++){
            for(int y = 0; y <= yrows; y++){
                vertices[v] = new Vector3(y, x, 0);
                Debug.Log(vertices[v]);
                v++;
            }

        }

        int vert = 0;
        int tri = 0;

        for(int i = 0; i < xcols; i++){ 
            for(int j = 0; j < yrows; j++){

                triangles[tri] = vert;                  Debug.Log(tri+" "+vert);
                triangles[tri + 1] = vert + 1;          Debug.Log((tri + 1)+" "+(vert + 1));
                triangles[tri + 2] = vert + yrows + 1;   Debug.Log((tri + 2)+" "+(vert + yrows + 1));
                triangles[tri + 3] = vert + yrows + 1;   Debug.Log((tri + 3)+" "+(vert + yrows + 1));
                triangles[tri + 4] = vert + 1;          Debug.Log((tri + 4)+" "+(vert + 1));
                triangles[tri + 5] = vert + yrows +2;    Debug.Log((tri + 5)+" "+(vert + yrows +2));

                vert ++;
                tri += 6;

            }
            vert++;
        }
        // yield return new WaitForSeconds(.1f);



    }

    void circle(Vector3 c, int r, int edges){
        float rotate = 360 / edges;
        vertices = new Vector3[edges + 1];
        triangles = new int[ 3*edges];

        int start = 0;
        float angle = 0;

        vertices[0] = c;
        for (int i = 1; i <= edges; i++)
        {
            vertices[i] = new Vector3(r * (float)Math.Cos(angle * Math.PI / 180), 0, r * (float)Math.Sin(angle * Math.PI / 180));
            angle += rotate;
        }

        int tri = start;
        for (int i = 1; i < edges; i++)
        {
            triangles[tri] = 0;
            triangles[tri+1] = i + 1;
            triangles[tri+2] = i;

        tri += 3;
        }

        triangles[tri] = start;
        triangles[tri+1] = start + 1;
        triangles[tri+2] = edges;


    }

    void cylindre(Vector3 c, float r, int edges, float height){

        // int     void circle(Vector3 c, int r, int edges){
        int rotate = 360 / edges;
        vertices = new Vector3[2* (edges + 1)];
        triangles = new int[ 12*edges];


        int angle = 0;
        vertices[0] = c;
        vertices[edges + 1] = c + new Vector3(0, height, 0);

        for (int i = 1; i <= edges; i++)
        {
            vertices[i] = new Vector3(r * (float)Math.Cos(angle * Math.PI / 180), 0, r * (float)Math.Sin(angle * Math.PI / 180));
            vertices[edges + 1 + i] = new Vector3(r * (float)Math.Cos(angle * Math.PI / 180), height, r * (float)Math.Sin(angle * Math.PI / 180));
            angle += rotate;

        }
        int start_bottom = 0;
        int start_top = edges + 1;

        int tri = 0;
        for (int i = 1; i < edges; i++)
        {
            triangles[tri] = start_bottom;
            triangles[tri+2] = i + 1;
            triangles[tri+1] = i;

            triangles[tri+3] = start_top;
            triangles[tri+4] = i + edges +2;
            triangles[tri+5] = i + edges + 1;

            triangles[tri+6] = i;
            triangles[tri+8] = i + edges + 2;
            triangles[tri+7] = i + edges + 1;

            triangles[tri+9] = i;
            triangles[tri+10] = i + edges +2;
            triangles[tri+11] = i + 1;



        tri += 12;
        }

        triangles[tri] = start_bottom;
        triangles[tri+1] = edges;
        triangles[tri+2] = start_bottom + 1;

        triangles[tri+3] = start_top;
        triangles[tri+4] = start_top + 1;
        triangles[tri+5] = 2*edges + 1;

        triangles[tri+6] = start_bottom + 1;
        triangles[tri+7] = edges;
        triangles[tri+8] = 2*edges + 1;

        triangles[tri+9] = start_bottom + 1;
        triangles[tri+10] = 2*edges + 1;
        triangles[tri+11] = start_top + 1;


    }

    void sphere(Vector3 c, float r, int edges){



        float rotate1 = 360 / edges;
        float rotate2 = 360 / edges;
        vertices = new Vector3[edges * edges + 1];
        triangles = new int[ 15*edges];

        int start = 0;
        float teta = 0;
        float phi = 0;

        vertices[0] = c;
        int inc = 0;
        for (int j = 0; j < edges; j++){
            for (int i = 1; i <= edges; i++)
            {
                vertices[i+inc] = new Vector3(r * (float)Math.Sin(phi * Math.PI / 180) * (float)Math.Cos(teta * Math.PI / 180) ,
                                                r * (float)Math.Sin(phi * Math.PI / 180) * (float)Math.Sin(teta * Math.PI / 180),
                                                r * (float)Math.Cos(phi * Math.PI / 180));
                teta += rotate1;
            }
            inc += edges;
            phi += rotate2;
        }

        int tri = start;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        // for (int i = 1; i < edges; i++)
        // {
        //     triangles[tri] = 0;
        //     triangles[tri+1] = i + 1;
        //     triangles[tri+2] = i;

        // tri += 3;
        // }

        // triangles[tri] = start;
        // triangles[tri+1] = start + 1;
        // triangles[tri+2] = edges;



    }

    void cone(Vector3 c, float r, int edges, int h){

        float rotate = 360 / edges;
        vertices = new Vector3[edges + 2];
        triangles = new int[ 6*edges];

        int start = 0;
        float angle = 0;

        vertices[0] = c;
        for (int i = 1; i <= edges; i++)
        {
            vertices[i] = new Vector3(r * (float)Math.Cos(angle * Math.PI / 180), 0, r * (float)Math.Sin(angle * Math.PI / 180));
            angle += rotate;
        }
        Vector3 sommet = new Vector3(c.x, h, c.z);
        vertices[edges + 1] = sommet;

        int tri = start;
        for (int i = 1; i < edges; i++)
        {
            triangles[tri] = 0;
            triangles[tri+1] = i;
            triangles[tri+2] = i + 1;
            triangles[tri+3] = edges + 1;
            triangles[tri+4] = i + 1;
            triangles[tri+5] = i;

        tri += 6;
        }

        triangles[tri] = start;
        triangles[tri+1] = edges;
        triangles[tri+2] = start + 1;
        triangles[tri+3] = edges + 1;
        triangles[tri+4] = start + 1;
        triangles[tri+5] = edges;

    }


    // void sphere_mat(int r, int p, int m, int t){

    //     const float PI = 3.1415926f;
    //     int rayon = r, nparallele = p, nmeridien = m, ntronc = t;
    //     int[] triangles;
    //     Vector3[] vertices;
    //     Material mat;

    //     ntronc = nmeridien - ntronc;
    //     // Création des structures de données qui accueilleront sommets et  triangles  // Remplissage de la structure sommet 

    //     vertices = new Vector3[(ntronc + 1) *(nparallele+1)+1];
    //     triangles = new int[(ntronc *nparallele *2 + 2*nparallele) * 3];


    //     int index = 0;
    //     vertices[(ntronc + 1) * (nparallele + 1)] = new Vector3(0, 0, 0); 
    //     for (int i = 0; i <= nparallele; i++)
    //     {
    //         float phi = -PI / 2 + i * PI / nparallele;
    //         for(int j = 0; j<= ntronc; j++)
    //         {
    //             float teta = j * 2 * PI / nmeridien;
                
    //             float x = Mathf.Cos(teta) * Mathf.Cos(phi);
    //             float y = Mathf.Sin(teta) * Mathf.Cos(phi);
    //             float z = Mathf.Sin(phi);

    //             vertices[index] = new Vector3(x, y ,z);
    //             index++;
    //         }
    //     }

    //     int k = 0;
    //     for (int j = 0; j < nparallele; j++)
    //     {
    //         int r1 = j * (ntronc + 1);
    //         int r2 = (j+1) * (ntronc + 1);
    //         for (int i = 0; i < ntronc; i++)
    //         {
    //             triangles[k] = r1 + i;
    //             triangles[k+1] = r2 + i +1;
    //             triangles[k+2] = r2 + i;

    //             triangles[k+3] = r1 + i;
    //             triangles[k+4] = r1 + i + 1;
    //             triangles[k+5] = r2 + i + 1;

    //             k += 6;
    //         }
    //     }

    //     for (int i =0;i<nparallele;i++)
    //     {
           
    //         triangles[k++] = (i * (ntronc + 1)) + ntronc + 1;
    //         triangles[k++] = ((ntronc + 1) * (nparallele + 1)); // ok
    //         triangles[k++] = (i * (ntronc + 1));

    //         triangles[k++] = ((i * (ntronc + 1))) + ntronc;
    //         triangles[k++] = ((ntronc + 1) * (nparallele + 1)); // ok
    //         triangles[k++] = ((i * (ntronc + 1)) + ntronc + 1) + ntronc;


         

    //     }


    // }

    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        // DrawSphere();
        Gizmos.color = Color.black;
        foreach (var point in vertices)
        {
            Gizmos.DrawSphere(point, 0.1f);
        }
    }

    

}