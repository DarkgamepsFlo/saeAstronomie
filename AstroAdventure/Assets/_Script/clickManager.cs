using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour {

	void Update () {

        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                Debug.Log(hit.transform.name);
            }
        } 
	}
}