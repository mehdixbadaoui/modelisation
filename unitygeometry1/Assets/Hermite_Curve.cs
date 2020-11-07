using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class Hermite_Curve : MonoBehaviour
{
	public GameObject start, startTangentPoint, end, endTangentPoint;

	public Color color = Color.white;
	public float width = 0.2f;
	public int numberOfPoints = 20;
	LineRenderer lineRenderer;
	Vector3 s0, st, e0, et;
	Vector3 p0, p1, p2, p3;

	void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.useWorldSpace = true;
		lineRenderer.material = new Material(
			Shader.Find("Legacy Shaders/Particles/Additive"));

		s0 = start.transform.position;
		e0 = end.transform.position;
		st = startTangentPoint.transform.position - start.transform.position;
		et = endTangentPoint.transform.position - end.transform.position;


		hermite(s0, st, e0, et);

		//bezier(10, p0, p1, p2, p3);

	}

	void Update()
	{

		//hermite(s0, st, e0, et);

	}

	private void hermite(Vector3 p0, Vector3 p1, Vector3 m0, Vector3 m1)
	{
		// check parameters and components
		if (null == lineRenderer || null == start || null == startTangentPoint
			|| null == end || null == endTangentPoint)
		{
			return; // no points specified
		}

		// update line renderer
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;
		if (numberOfPoints > 0)
		{
			lineRenderer.positionCount = numberOfPoints;
		}

		// set points of Hermite curve
		float t;
		Vector3 position;

		for (int i = 0; i < numberOfPoints; i++)
		{
			t = i / (numberOfPoints - 1.0f);
			position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0
					  + (t * t * t - 2.0f * t * t + t) * m0
					  + (-2.0f * t * t * t + 3.0f * t * t) * p1
					  + (t * t * t - t * t) * m1;
			lineRenderer.SetPosition(i, position);
		}


	}

    private void bezier(int n, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
		lineRenderer.SetPosition(1, p0);
		lineRenderer.SetPosition(2, p1);
		lineRenderer.SetPosition(3, p2);
		lineRenderer.SetPosition(4, p3);
        Vector3 pos;
        //for (int i = 0; i < n; i++)
        //{
        //    pos =

        //}
    }
}
