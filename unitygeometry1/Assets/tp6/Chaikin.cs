using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]

public class Chaikin : MonoBehaviour
{
    List<Transform> children;
    List<Transform> newpoints;
    LineRenderer linerenderer;
    Transform t;
    public GameObject father;

    public int times;
    public bool sub;

    private void Start()
    {
        children = new List<Transform>();

        newpoints = new List<Transform>();

        linerenderer = GetComponent<LineRenderer>();


        t = gameObject.transform;

        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(t.GetChild(i));
        }

        linerenderer.positionCount = children.Count;
        for (int i = 0; i < children.Count; i++)
        {
            linerenderer.SetPosition(i, children[i].position);
        }


    }
    void Update()
    {
        if (sub)
        {
            subdiviser();
        }
    }

    private void subdiviser()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            GameObject go1 = new GameObject();
            go1.transform.position = t.GetChild(i).transform.position * 3 / 4
                                    + t.GetChild(i + 1).transform.position / 4;
            GameObject go2 = new GameObject();
            go2.transform.position = t.GetChild(i + 1).transform.position * 3 / 4
                                    + t.GetChild(i).transform.position / 4;

            newpoints.Add(go1.transform);
            newpoints.Add(go2.transform);

        }

        GameObject golast = new GameObject();
        golast.transform.position = newpoints[0].transform.position;


        newpoints.Add(golast.transform);

        linerenderer.positionCount = newpoints.Count;
        for (int i = 0; i < linerenderer.positionCount;  i++)
        {
            linerenderer.SetPosition(i, newpoints[i].position);
        }
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform t in newpoints)
        {
            t.parent = gameObject.transform;
        }

        newpoints = new List<Transform>();
        times++;
        sub = false;
    }
}
