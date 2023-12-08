using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class PlanetsEllipsesVariables
{
    public int resolution;
    public float xRadius;
    public float yRadius;
    public float zRadius;
}

[System.Serializable]
public class PlanetsEllipses
{
    public PlanetsEllipsesVariables Mercury;
    public PlanetsEllipsesVariables Venus;
    public PlanetsEllipsesVariables Earth;
    public PlanetsEllipsesVariables Mars;
    public PlanetsEllipsesVariables Jupiter;
    public PlanetsEllipsesVariables Saturn;
    public PlanetsEllipsesVariables Uranus;
    public PlanetsEllipsesVariables Neptune;
}