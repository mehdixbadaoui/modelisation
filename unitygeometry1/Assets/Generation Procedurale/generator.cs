using System;
using UnityEngine;

public class generator : MonoBehaviour
{
    public int width, height;

    [Range(10f, 100f)]
    public float scale = 20f;

    [Range(10f, 100f)]
    public float XOffset = 10f;

    [Range(10f, 100f)]
    public float YOffset = 10f;

    float building_height;
    public GameObject building;
    void Start()
    {
        GameObject go;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                building_height = calc_height(i, j);

                Vector3 new_pos = new Vector3(i, transform.position.y, j);
                go = Instantiate(building, new_pos, Quaternion.identity);
                go.transform.SetParent(transform);
                go.GetComponent<build>().stories = Mathf.FloorToInt(building_height * 100);
                Debug.Log(go.GetComponent<build>().stories);
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
