using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SteamVRLaserWrapper : MonoBehaviour
{
    private SteamVR_LaserPointer steamVrLaserPointer;

    private void Awake()
    {
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "solarScene")
        {
            //steamVrLaserPointer.transform.localScale =  new Vector3(100, 100, 100); // ça change la taille de la main...
        }
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        IPointerClickHandler clickHandler = e.target.GetComponent<IPointerClickHandler>();
        /*if (clickHandler == null)
        {
            return;
        }*/
        if (e.target.name == "Button")
        {

            if (e.target.GetComponentInParent<Button>())
            {
                e.target.GetComponentInParent<Button>().onClick.Invoke();
            }
            
        }
        else
        {
            GameObject gameObjectCible = GameObject.Find("clickManager");
            ClickManager clickScript = gameObjectCible.GetComponent<ClickManager>();
            if (clickScript != null)
            {
                clickScript.clickPlanets(e.target.name);
            }
        }


        //clickHandler.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    private void OnPointerOut(object sender, PointerEventArgs e)
    {
        IPointerExitHandler pointerExitHandler = e.target.GetComponent<IPointerExitHandler>();
        if (pointerExitHandler == null)
        {
            return;
        }

        pointerExitHandler.OnPointerExit(new PointerEventData(EventSystem.current));
    }

    private void OnPointerIn(object sender, PointerEventArgs e)
    {
        IPointerEnterHandler pointerEnterHandler = e.target.GetComponent<IPointerEnterHandler>();
        if (pointerEnterHandler == null)
        {
            return;
        }

        pointerEnterHandler.OnPointerEnter(new PointerEventData(EventSystem.current));
    }
}