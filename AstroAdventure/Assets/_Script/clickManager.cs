using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour {

	void Update () {

        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                Debug.Log(hit.transform.name);
                GameObject gameObjectCible = GameObject.Find(hit.transform.name);
                if (gameObjectCible != null){
                    EarthManager scriptCible = gameObjectCible.GetComponent<EarthManager>();

                    if (scriptCible != null){
                        scriptCible.test();
                    }
                    else{
                        Debug.LogError("Le script MyScript n'a pas été trouvé sur le GameObject.");
                    }
                }
                else{
                    Debug.LogError("Le GameObject avec le nom " + hit.transform.name + " n'a pas été trouvé.");
                }
            }
        } 
	}
}