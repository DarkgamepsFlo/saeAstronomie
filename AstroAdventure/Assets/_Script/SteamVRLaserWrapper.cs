using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SteamVRLaserWrapper : MonoBehaviour {
    private SteamVR_LaserPointer steamVrLaserPointer;

    private void Awake() {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
    }

    private void OnPointerClick(object sender, PointerEventArgs e) {
        //setLaserVisible(true);

        if (e.target.name == "Button") {

            if (e.target.GetComponentInParent<Button>())
                e.target.GetComponentInParent<Button>().onClick.Invoke();
            
        }
        else {
            GameObject gameObjectCible = GameObject.Find("clickManager");
            ClickManager clickScript = gameObjectCible.GetComponent<ClickManager>();
            if (clickScript != null)
                clickScript.clickPlanets(e.target.name);
        }
    }

    private void OnPointerOut(object sender, PointerEventArgs e) {
        //setLaserVisible(false);
    }

    private void OnPointerIn(object sender, PointerEventArgs e) {
        //setLaserVisible(true);
    }

    /*private void setLaserVisible(bool visible) {
        MeshRenderer laserPointerRender = steamVrLaserPointer.gameObject.transform.GetChild(5).GetComponentInChildren<MeshRenderer>();
        laserPointerRender.enabled = visible;
    }*/
}