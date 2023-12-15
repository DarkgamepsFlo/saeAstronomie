using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;

public class PlanetEllipseManager : MonoBehaviour
{
    private int resolution; // Nombre de points sur le cercle
    private float xRadius; // Rayon selon l'axe X
    private float yRadius; // Rayon selon l'axe Y
    private float zRadius; // Rayon selon l'axe Z

    private LineRenderer lineRenderer;
    
    private float scale;

    void Start() {
        if (SceneManager.GetActiveScene().name == "solarScene")
        {
            scale = 0.0065f;
        }
        else
        {
            scale = 1f;
        }
        loadVariables();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.01f, 0.01f);
        CreateEllipse();
    }

    void CreateEllipse() {
        lineRenderer.positionCount = resolution + 1;
        lineRenderer.useWorldSpace = false;

        float deltaTheta = (2f * Mathf.PI) / resolution;
        float theta = 10f;

        for (int i = 0; i < resolution + 1; i++) {
            float x = xRadius * Mathf.Sin(theta) * scale;
            float y = yRadius * scale;
            float z = zRadius * Mathf.Cos(theta) * scale;
            
            lineRenderer.SetPosition(i, new Vector3(x, y, z));

            theta += deltaTheta;
        }
    }

    public void loadVariables() {
        string jsonContent = File.ReadAllText("./Assets/_Script/PlanetsEllipsesVariables.json");
        PlanetsEllipses planetsEllipses = JsonConvert.DeserializeObject<PlanetsEllipses>(jsonContent);
        PlanetsEllipsesVariables planetsEllipsesVariables = null;
        switch (gameObject.name) {
            case "MercuryEllipse":
                planetsEllipsesVariables = planetsEllipses.Mercury;
                break;
            case "VenusEllipse":
                planetsEllipsesVariables = planetsEllipses.Venus;
                break;
            case "EarthEllipse":
                planetsEllipsesVariables = planetsEllipses.Earth;
                break;
            case "MarsEllipse":
                planetsEllipsesVariables = planetsEllipses.Mars;
                break;
            case "JupiterEllipse":
                planetsEllipsesVariables = planetsEllipses.Jupiter;
                break;
            case "SaturnEllipse":
                planetsEllipsesVariables = planetsEllipses.Saturn;
                break;
            case "UranusEllipse":
                planetsEllipsesVariables = planetsEllipses.Uranus;
                break;
            case "NeptuneEllipse":
                planetsEllipsesVariables = planetsEllipses.Neptune;
                break;
        }

        if (planetsEllipsesVariables != null) {
            resolution = planetsEllipsesVariables.resolution;
            xRadius = planetsEllipsesVariables.xRadius;
            yRadius = planetsEllipsesVariables.yRadius;
            zRadius = planetsEllipsesVariables.zRadius;
        }
        else {
            Debug.LogError($"Ellipse inconnue : {gameObject.name}");
        }
    }
}
