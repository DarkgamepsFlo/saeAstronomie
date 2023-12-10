using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SatelliteEllipseManager : MonoBehaviour
{
    private int resolution; // Nombre de points sur le cercle
    private float xRadius; // Rayon selon l'axe X
    private float yRadius; // Rayon selon l'axe Y
    private float zRadius; // Rayon selon l'axe Z
    private string center;
    private GameObject centerPlanet;
    private float positionPlanetx;
    private float positionPlanety;
    private float positionPlanetz;
    private LineRenderer lineRenderer;

    void Start()
    {
        loadVariables();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        CreateEllipse();
    }

    void CreateEllipse()
    {
        // Permet de récupérer les objets représentant la planète
        centerPlanet = GameObject.Find(center);

        positionPlanetx = centerPlanet.transform.position.x;
        positionPlanety = centerPlanet.transform.position.y;
        positionPlanetz = centerPlanet.transform.position.z;

        lineRenderer.positionCount = resolution + 1;
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (2f * Mathf.PI) / resolution;
        float theta = 10f;

        for (int i = 0; i < resolution + 1; i++)
        {
            float x = positionPlanetx + xRadius * Mathf.Sin(theta);
            float y = positionPlanety + yRadius;
            float z = positionPlanetz + zRadius * Mathf.Cos(theta);

            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            theta += deltaTheta;
        }
    }

    public void loadVariables()
    {
        string jsonContent = File.ReadAllText("./Assets/_Script/SatelliteEllipsesVariables.json");
        SatelliteEllipses satelliteEllipses = JsonConvert.DeserializeObject<SatelliteEllipses>(jsonContent);
        SatelliteEllipsesVariables satelliteEllipsesVariables = null;
        switch (gameObject.name)
        {
            case "MoonEllipse":
                satelliteEllipsesVariables = satelliteEllipses.Moon;
                break;
        }

        if (satelliteEllipsesVariables != null)
        {
            resolution = satelliteEllipsesVariables.resolution;
            xRadius = satelliteEllipsesVariables.xRadius;
            yRadius = satelliteEllipsesVariables.yRadius;
            zRadius = satelliteEllipsesVariables.zRadius;
            center = satelliteEllipsesVariables.center;
        }
        else
        {
            Debug.LogError($"Ellipse inconnue : {gameObject.name}");
        }
    }
}
