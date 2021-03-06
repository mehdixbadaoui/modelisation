﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build : MonoBehaviour
{
    public GameObject[] bottom;
    public GameObject[] middle;
    public GameObject[] top;

    
    float height_offste;
    public int stories { get; set; }
    void Start()
    {
        //Build();
    }

    public void Build()
    {
        height_offste = 0;
        buildBottom();
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

        GameObject prefab = bottom[UnityEngine.Random.Range(0, bottom.Length)];
        GameObject go = Instantiate(prefab, newPos, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);

        height_offste += prefab.GetComponentInChildren<MeshRenderer>().bounds.size.y;
    }

    private void buildMiddle()
    {
        Vector3 newPos = transform.position;
        newPos.y += height_offste;

        GameObject prefab = middle[UnityEngine.Random.Range(0, middle.Length)];
        GameObject go = Instantiate(prefab, newPos, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);

        height_offste += prefab.GetComponentInChildren<MeshRenderer>().bounds.size.y;
    }

    private void buildTop()
    {
        Vector3 newPos = transform.position;
        newPos.y += height_offste;

        GameObject prefab = top[UnityEngine.Random.Range(0, top.Length)];
        GameObject go = Instantiate(prefab, newPos, Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);

        height_offste += prefab.GetComponentInChildren<MeshRenderer>().bounds.size.y;
    }

}
