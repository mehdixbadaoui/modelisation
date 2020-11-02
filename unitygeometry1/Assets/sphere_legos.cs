using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere_legos : MonoBehaviour
{
    public Material mat;

    public GameObject prefab;

    public int width, height, depth;
    List<Vector3> centres = new List<Vector3>();

    Vector3[] vertices;
    int[] triangles;


    // Start is called before the first frame update
    void Start()
    {
        width = 30;
        height = 60;
        depth = 30;
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        
        // centres.Add(new Vector3(12, 12, 12));
        //centres.Add(new Vector3(15, 15, 15));
        centres.Add(new Vector3(24, 18, 15));

        //draw_diff(width, height, depth, new Vector3(12, 12, 12), centres, 10);
        //drawLegos(width, height, depth, centres, 10);

        drawBlock(5, 5, 5);

        Mesh msh = new Mesh();
        
        msh.vertices = vertices;
        msh.triangles = triangles;

        gameObject.GetComponent<MeshFilter>().mesh = msh;
        gameObject.GetComponent<MeshRenderer>().material = mat;


    }

    // Update is called once per frame
    void Update()
    {
        sculpt();
    }

    public void drawLegos(int w, int h, int d, List<Vector3> c, int r){
        
        int v = 0;
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                for (int k = 0; k < d; k++)
                {
                    // List<float> distances;
                    Vector3 current = new Vector3(i, j, k);
                    bool draw = false;

                    foreach (Vector3 centre in centres)
                    {
                        Vector3 distance = current - centre;
                        float dist = distance.magnitude;
                        if(dist < r) {
                            draw = true;
                        }
                    }

                    if (draw){

                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(i, j, k);


                    }

                }


            }
        }

    }

    public void draw_intersec(int w, int h, int d, List<Vector3> c, int r){
        int v = 0;
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                for (int k = 0; k < d; k++)
                {
                    // List<float> distances;
                    Vector3 current = new Vector3(i, j, k);
                    bool draw = false;

                    foreach (Vector3 centre in centres)
                    {
                        Vector3 distance = current - centre;
                        float dist = distance.magnitude;
                        if (dist < r)
                        {
                            draw = true;
                        }
                        foreach (Vector3 othercenter in centres)
                        {
                            if(othercenter == centre) continue;
                            Vector3 otherdistance = current - othercenter;
                            float otherdist = otherdistance.magnitude;
                            if(otherdist >= r) {
                            draw = false;
                        }

                        }



                    }

                    if (draw)
                    {

                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(i, j, k);


                    }

                }


            }
        }

    }

    private void draw_diff(int w, int h, int d, Vector3 c1, List<Vector3> c, int r){

        int v = 0;
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                for (int k = 0; k < d; k++)
                {
                    // List<float> distances;
                    Vector3 current = new Vector3(i, j, k);
                    bool draw = false;

                    Vector3 distance = current - c1;
                    float dist = distance.magnitude;
                    if(dist < r) {
                        draw = true;
                    }

                    foreach (Vector3 centre in c)
                    {
                        Vector3 distance2 = current - centre;
                        float dist2 = distance2.magnitude;
                        if(dist2 < r) {
                            draw = false;
                        }
                    }

                    if (draw){

                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(i, j, k);


                    }

                }


            }
        }


    }

    private void drawBlock(int w, int h, int d)
    {
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                for (int k = 0; k < d; k++)
                {
                    //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //cube.transform.position = new Vector3(i, j, k);
                    GameObject cube = Instantiate(prefab, new Vector3(i, j, k), Quaternion.identity);
                    cube.GetComponent<cubeweight>().weight = 25;
                    cube.layer = 9;

                }


            }
        }

    }

    private void sculpt()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, 1 << 9))
            {
                if (hit.transform)
                {
                    //hit.transform.gameObject.SetActive(false);
                    hit.transform.GetComponent<cubeweight>().weight -= 10;
                    hit.transform.gameObject.layer = 8;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, 1 << 8))
            {
                if (hit.transform)
                {
                    hit.transform.GetComponent<cubeweight>().weight += 10;
                    hit.transform.gameObject.layer = 9;
                }
            }
        }



    }
    //private void OnDrawGizmos()
    //{
    //    if (vertices == null) return;
    //    // DrawSphere();
    //    Gizmos.color = Color.white;
    //    foreach (var point in vertices)
    //    {
    //        Gizmos.DrawCube(point, new Vector3(0.1f, 0.1f, 0.1f));
    //    }
    //}

}
