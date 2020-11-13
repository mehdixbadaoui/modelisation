using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]

public class Chaikin : MonoBehaviour
{
    List<Transform> children;
    LineRenderer linerenderer;
    Transform t;

    public int times;

    private void Start()
    {
        children = new List<Transform>();
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
    }
}
