using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Text : MonoBehaviour
{

    string chemin, jsonString;

    // Start is called before the first frame update
    void Start()
    {
        chemin = Application.streamingAssetsPath + "/_Json/text.json";
        jsonString = File.ReadAllText(chemin);
        Debug.Log(jsonString);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
