using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class SatelliteVariables
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
    public string center;
}

[System.Serializable]
public class Satellite
{
    public SatelliteVariables Moon;
}
