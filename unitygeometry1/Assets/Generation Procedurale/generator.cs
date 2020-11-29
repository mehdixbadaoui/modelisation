using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    public GameObject building;
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3 new_pos = new Vector3(i, transform.position.y, j);
                Instantiate(building, new_pos, Quaternion.identity);

            }
        }
    }

}
