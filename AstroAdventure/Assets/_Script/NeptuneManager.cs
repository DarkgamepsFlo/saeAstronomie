using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class NeptuneManager : MonoBehaviour
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
    public float taillePlanete = 73.8f;
    // Permet de définir l'ellipse de la planète
    public float longueurEllipse = 1998f;
    public float largeurEllipse = 1968f;
    public float hauteurEllipse = 0f;
    // Permet de définir la rotation de la planète
    public float rotationx = 2.8f;
    public float rotationy = -12f;
    public float rotationz = 0f;
    // Permet de définir la vitesse de la planète
    public float vitesse = 0.05f;

    public Interactable interactable;

    // Start is called before the first frame update
    void Start()
    {
        // Permet de récupérer les objets représentant la planète concerné et du soleil
        centreEarth = GameObject.Find("Neptune");
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
        if (!interactable.attachedToHand)
        {
            epsi += vitesse * Time.deltaTime; // Ajuste la vitesse de rotation

            float x = (positionSunx) + largeurEllipse * Mathf.Sin(epsi);
            float y = (positionSuny) + hauteurEllipse * Mathf.Sin(epsi);
            float z = (positionSunz) - longueurEllipse * Mathf.Cos(epsi);

            transform.position = new Vector3(x, y, z);

            transform.Rotate(new Vector3(rotationx, rotationy, rotationz) * Time.deltaTime);
        }

    }
}