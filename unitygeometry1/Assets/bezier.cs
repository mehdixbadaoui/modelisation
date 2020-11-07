using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]

public class bezier : MonoBehaviour
{
    public Color color = Color.white;
    public float width = 0.2f;
    public int numberOfPoints = 4;
    LineRenderer lineRenderer;

    Vector3 p0, p1, p2, p3;

    // Start is called before the first frame update
    void Start()
    {
        numberOfPoints = 24;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));

        p0 = new Vector3(-2, -2, 0);
        p1 = new Vector3(-1, 1, 0);
        p2 = new Vector3(1, 1, 0);
        p3 = new Vector3(2, -2, 0);

        

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Keypad1)) movepoint(p0);
        else if (Input.GetKey(KeyCode.Keypad2)) movepoint(p1);
        else if (Input.GetKey(KeyCode.Keypad3)) movepoint(p2);
        else if (Input.GetKey(KeyCode.Keypad4)) movepoint(p3);

        curve(4, p0, p1, p2, p3);

        Debug.Log(p0);

    }

    private void curve(int n, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        if (numberOfPoints > 0)
        {
            lineRenderer.positionCount = numberOfPoints;
        }


        lineRenderer.SetPosition(20, p3);
        lineRenderer.SetPosition(21, p2);
        lineRenderer.SetPosition(22, p1);
        lineRenderer.SetPosition(23, p0);

        float t;
        Vector3 position;

        for(int i = 0; i < 20; i++)
        {
            t = i / (numberOfPoints - 1.0f);
            position = p0 * bernstein(0, 3, t) +
                       p1 * bernstein(1, 3, t) +
                       p2 * bernstein(2, 3, t) +
                       p3 * bernstein(3, 3, t);

            lineRenderer.SetPosition(i, position);
        }

    }
    float bernstein(int i, int n, float t)
    {
        return fact(n) / (fact(i) * fact(n - i)) * Mathf.Pow(t, i) * Mathf.Pow(1 - t, n - i);
    }

    private int fact(int n)
    {
        if (n == 0 || n == 1) return 1;
        else return fact(n - 1);
    }

    void movepoint(Vector3 point)
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        point = new Vector3(0, 0, 0);
        //point += new Vector3(v, h, 0) * 100f * Time.deltaTime;
    }
}

