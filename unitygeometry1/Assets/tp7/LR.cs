using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]

public class LR : MonoBehaviour
{
    public int numberOfPoints;
    LineRenderer lineRenderer;
    Vector3 p0, p1, p2, p3;

    void Start()
    {
        numberOfPoints = 24;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        //lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        lineRenderer.positionCount = 4;

        p0 = new Vector3(-2, -2, 0);
        p1 = new Vector3(-1, 1, 0);
        p2 = new Vector3(1, 1, 0);
        p3 = new Vector3(2, -2, 0);

    }

    void Update()
    {
        lineRenderer.SetPosition(0, p0);
        lineRenderer.SetPosition(1, p1);
        lineRenderer.SetPosition(2, p2);
        lineRenderer.SetPosition(3, p3);

        p0 = movepoint(p0);
    }
    Vector3 movepoint(Vector3 point)
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        return point + new Vector3(v, h, 0) * 100f * Time.deltaTime;
    }

}
