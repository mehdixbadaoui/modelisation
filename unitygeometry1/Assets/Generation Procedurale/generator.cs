﻿using System;
using UnityEngine;

public class generator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public int blockWidth = 2;
    public int blockHeight = 2;

    [Range(10f, 500f)]
    public float scale = 20f;

    [Range(10f, 100f)]
    public float XOffset = 10f;

    [Range(1f, 100f)]
    public float YOffset = 10f;

    float building_height;
    public GameObject building;
    void Start()
    {
        GameObject go;
        for (int i = (int) -width / 2 , block_w = 0; i < width / 2; i++, block_w++)
        {
            if (block_w % (blockWidth + 1) == 0) continue;
            for (int j = (int) - height / 2, block_h = 0; j < height / 2; j++, block_h ++)
            {
                if (block_h % (blockHeight + 1) == 0) continue;

                building_height = calc_height(i, j);

                Vector3 new_pos = new Vector3(i, transform.position.y, j);

                go = Instantiate(building, new_pos, Quaternion.identity);
                go.transform.SetParent(transform);

                go.GetComponent<build>().stories = Mathf.FloorToInt(building_height * 10);
                go.GetComponent<build>().Build();

            }
        }
    }

    public float calc_height(int x, int y)
    {
        float xper = (float) x / width * scale;
        float yper = (float) y / height * scale;

        return Mathf.PerlinNoise(xper, yper);
    }

}
