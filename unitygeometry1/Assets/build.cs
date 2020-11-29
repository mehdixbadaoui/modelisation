using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build : MonoBehaviour
{
    public GameObject[] bottom;
    public GameObject[] middle;
    public GameObject[] top;

    Collider collider;
    
    float height_offste;
    // Start is called before the first frame update
    void Start()
    {
        height_offste = 0;
        buildBottom();
        int stories = UnityEngine.Random.Range(0, 10);
        for (int i = 0; i < stories; i++)
        {
            buildMiddle();

        }
        buildTop();
    }

    private void buildBottom()
    {
        Vector3 newPos = transform.position;
        newPos.y += height_offste;

        GameObject go = bottom[UnityEngine.Random.Range(0, bottom.Length)];
        Instantiate(go, newPos , Quaternion.identity);
        //collider = go.AddComponent<Collider>();

        //height_offste += go.GetComponent<Collider>().bounds.size.y;
        height_offste += go.GetComponentInChildren<MeshRenderer>().bounds.size.y;
    }

    private void buildMiddle()
    {
        Vector3 newPos = transform.position;
        newPos.y += height_offste;

        GameObject go = middle[UnityEngine.Random.Range(0, middle.Length)];
        Instantiate(go, newPos, Quaternion.identity);

        //collider = go.AddComponent<Collider>();

        //height_offste += go.GetComponent<Collider>().bounds.size.y;
        height_offste += go.GetComponentInChildren<MeshRenderer>().bounds.size.y;
    }

    private void buildTop()
    {
        Vector3 newPos = transform.position;
        newPos.y += height_offste;

        GameObject go = top[UnityEngine.Random.Range(0, top.Length)];
        Instantiate(go, newPos, Quaternion.identity);
        //collider = go.AddComponent<Collider>();

        //height_offste += go.GetComponent<Collider>().bounds.size.y;
        height_offste += go.GetComponentInChildren<MeshRenderer>().bounds.size.y;
    }

}
