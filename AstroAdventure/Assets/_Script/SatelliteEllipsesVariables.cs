using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class SatelliteEllipsesVariables
{
    public int resolution;
    public float xRadius;
    public float yRadius;
    public float zRadius;
    public string center;
}

[System.Serializable]
public class SatelliteEllipses
{
    public SatelliteEllipsesVariables Moon;
}