using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class PlanetsVariables
{
    public float size;
    public float ellipseLength;
    public float ellipseWidth;
    public float ellipseHeight;
    public float rotationx;
    public float rotationy;
    public float rotationz;
    public float speed;
    public float waitTime;
}

[System.Serializable]
public class Planets
{
    public PlanetsVariables Mercury;
    public PlanetsVariables Venus;
    public PlanetsVariables Earth;
    public PlanetsVariables Mars;
    public PlanetsVariables Jupiter;
    public PlanetsVariables Saturn;
    public PlanetsVariables Uranus;
    public PlanetsVariables Neptune;
}
