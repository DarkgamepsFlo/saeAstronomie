using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UranusEllipseManager : MonoBehaviour
{
    public int resolution = 2000; // Nombre de points sur le cercle
    public float xRadius = 1808f; // Rayon selon l'axe X
    public float yRadius = 0f; // Rayon selon l'axe Y
    public float zRadius = 1848f; // Rayon selon l'axe Z

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        CreateEllipse();
    }

    void CreateEllipse()
    {
        lineRenderer.positionCount = resolution + 1;
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (2f * Mathf.PI) / resolution;
        float theta = 10f;

        for (int i = 0; i < resolution + 1; i++)
        {
            float x = xRadius * Mathf.Sin(theta);
            float y = yRadius;
            float z = zRadius * Mathf.Cos(theta);
            
            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            theta += deltaTheta;
        }
    }
}
