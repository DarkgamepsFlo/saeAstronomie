using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SaturnManager : MonoBehaviour
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
    // VARIABLE : Elles vont permettre de modifier directement les données
    // Permet de définir la taille de la terre
    public float taillePlanete = 87.3f;
    // Permet de définir l'ellipse de la planète
    public float longueurEllipse = 1576f;
    public float largeurEllipse = 1526f;
    public float hauteurEllipse = 0f;
    // Permet de définir la rotation de la planète
    public float rotationx = 2.6f;
    public float rotationy = -14f;
    public float rotationz = 0f;
    // Permet de définir la vitesse de la planète
    public float vitesse = 0.1f;

    public Interactable interactable;
    public float waitTime = 3.0f;
    public float timer = 0.0f;
    public bool isGrab;

    // Start is called before the first frame update
    void Start()
    {
        // Permet de récupérer les objets représentant la planète concerné et du soleil
        centreEarth = GameObject.Find("Saturn");
        centreSun = GameObject.Find("Sun");
        // Permet de récupérer l'ensemble des positions x, y et z du soleil
        positionSunx = centreSun.transform.position.x;
        positionSuny = centreSun.transform.position.y;
        positionSunz = centreSun.transform.position.z;
        // Permet de redéfinir une nouvelle taille
        centreEarth.transform.localScale = new Vector3(taillePlanete, taillePlanete, taillePlanete);

        if (interactable == null)
            interactable = GetComponent<Interactable>();
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
                }
                    
            }
            else 
            {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "HandColliderLeft(Clone)" && collision.gameObject.name != "HandColliderRight(Clone)" /*&& collision.gameObject.name != "HeadCollider"*/)
            isGrab = true;
    }
}