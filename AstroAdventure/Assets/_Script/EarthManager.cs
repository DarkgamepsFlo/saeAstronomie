using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class EarthManager : MonoBehaviour
{
    // VARIABLE : Elles vont contenir des données
    // Représente les objets de la planète concerné et du soleil
    public GameObject centreSun;
    public GameObject centreEarth;
    // Permet de contenir la vitesse de rotation
    public float epsi;
    // Permet de contenir les positions du soleil
    public float positionSunx;
    public float positionSuny;
    public float positionSunz;

    float positionActux;
    float positionActuy;
    float positionActuz;
    // VARIABLE : Elles vont permettre de modifier directement les données
    // Permet de définir la taille de la terre
    public float taillePlanete = 65.5f;
    // Permet de définir l'ellipse de la planète
    public float longueurEllipse = 799f;
    public float largeurEllipse = 759f;
    public float hauteurEllipse = 0f;
    // Permet de définir la rotation de la planète
    public float rotationx = 2.3f;
    public float rotationy = -10f;
    public float rotationz = 0f;
    // Permet de définir la vitesse de la planète
    public float vitesse = 0.3f; // 0.3 = 104 000 km

    public Interactable interactable;
    public float waitTime = 5.0f;
    public float timer = 0.0f;
    public bool isGrab = false;

    public bool isClicked;

    // Start is called before the first frame update
    void Start()
    {
        // Permet de récupérer les objets représentant la planète concerné et du soleil
        centreEarth = GameObject.Find("Earth");
        centreSun = GameObject.Find("Sun");

        positionSunx = centreSun.transform.position.x;
        positionSuny = centreSun.transform.position.y;
        positionSunz = centreSun.transform.position.z;
        // Permet de récupérer l'ensemble des positions x, y et z du soleil
        positionActux = centreEarth.transform.position.x;
        positionActuy = centreEarth.transform.position.y;
        positionActuz = centreEarth.transform.position.z;
        // Permet de redéfinir une nouvelle taille
        centreEarth.transform.localScale = new Vector3(taillePlanete, taillePlanete, taillePlanete);

        if (interactable == null)
            interactable = GetComponent<Interactable>();
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        epsi += vitesse * Time.deltaTime; // Ajuste la vitesse de rotation

        float x = (positionSunx) + largeurEllipse * Mathf.Sin(epsi);
        float y = (positionSuny) + hauteurEllipse * Mathf.Sin(epsi);
        float z = (positionSunz) - longueurEllipse * Mathf.Cos(epsi);
        

        if (!interactable.attachedToHand)
        {
            if (isGrab) 
            {
                timer += Time.deltaTime;
                if (timer > waitTime)
                {
                    isGrab = false;
                    timer = 0.0f;
                    rigidBody.velocity = Vector3.zero;
                    rigidBody.angularVelocity = Vector3.zero;
                    transform.rotation = Quaternion.Euler(rotationx * Time.deltaTime, rotationy * Time.deltaTime, rotationz * Time.deltaTime);
                }
                    
            }
            else if(isClicked){     

                transform.Rotate(new Vector3(rotationx, rotationy, rotationz) * Time.deltaTime);
                transform.position = new Vector3(positionActux, positionActuy, positionActuz);
            }
            else 
            {
                positionActux = x;
                positionActuy = y;
                positionActuz = z;
                transform.position = new Vector3(x, y, z);
                transform.Rotate(new Vector3(rotationx, rotationy, rotationz) * Time.deltaTime);
            }
        }
        else
        {
            isGrab = true;
            timer = 0.0f;
        }
    }

    public void test()
    {
        isClicked = !isClicked;
        Camera mainCam = Camera.main;
        cameraManager scriptCible = mainCam.GetComponent<cameraManager>();
        if(isClicked){
            scriptCible.testModifCam(positionSunx, positionSunz, positionActux, positionActuy, positionActuz, centreEarth);
        }
        else{
            scriptCible.initialPlace(centreSun);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "HandColliderLeft(Clone)" && collision.gameObject.name != "HandColliderRight(Clone)" /*&& collision.gameObject.name != "HeadCollider"*/) 
        {
            isGrab = true;
            if (collision.gameObject.name == "Sphere") // Sphere collider du soleil
                transform.position = new Vector3(0, 10000, 0); // chut
        }
    }
}