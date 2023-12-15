using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.SceneManagement;

public class PlanetManager : MonoBehaviour
{
    // VARIABLE : Elles vont contenir des données
    // Représente les objets de la planète concerné et du soleil
    private GameObject centreSun;
    // Permet de contenir la vitesse de rotation
    private float epsi;
    // Permet de contenir les positions du soleil
    private float positionSunx;
    private float positionSuny;
    private float positionSunz;

    private float positionActux;
    private float positionActuy;
    private float positionActuz;

    // VARIABLE : Elles vont permettre de modifier directement les données
    // Permet de définir la taille de la terre
    private float taillePlanete;
    // Permet de définir l'ellipse de la planète
    private float longueurEllipse;
    private float largeurEllipse;
    private float hauteurEllipse;
    // Permet de définir la rotation de la planète
    private float rotationx;
    private float rotationy;
    private float rotationz;
    // Permet de définir la vitesse de la planète
    private float vitesse; // 0.3 = 104 000 km

    // Permet de changer le scale des planètes selon la scène
    private float scale;

    private Interactable interactable;
    private Rigidbody rigidBody;
    private float waitTime;
    private float timer = 0.0f;
    private bool isGrab;
    private bool isClicked = false;

    // Start is called before the first frame update
    void Start() {
        if (SceneManager.GetActiveScene().name == "solarScene") {
            scale = 0.0025f;
        }
        else {
            scale = 1f;
        }

        loadVariables();

        // Permet de récupérer les objets représentant le soleil
        centreSun = GameObject.Find("Sun");

        positionSunx = centreSun.transform.position.x;
        positionSuny = centreSun.transform.position.y;
        positionSunz = centreSun.transform.position.z;
        // Permet de récupérer l'ensemble des positions x, y et z du soleil
        positionActux = transform.position.x;
        positionActuy = transform.position.y;
        positionActuz = transform.position.z;

        if (interactable == null)
            interactable = GetComponent<Interactable>();
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();
        
        // Permet de redéfinir une nouvelle taille
        transform.localScale = new Vector3(taillePlanete, taillePlanete, taillePlanete);
    }

    // Update is called once per frame
    void Update()
    {
        epsi += vitesse * Time.deltaTime; // Ajuste la vitesse de rotation

        float x = (positionSunx) + largeurEllipse * Mathf.Sin(epsi);
        float y = (positionSuny) + hauteurEllipse * Mathf.Sin(epsi);
        float z = (positionSunz) - longueurEllipse * Mathf.Cos(epsi);
        

        if (!interactable.attachedToHand) {
            if (isGrab) {
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

    public bool getIsGrab()
    {
        return isGrab;
    }

    private void loadVariables() {
        string jsonContent = File.ReadAllText("./Assets/_Script/PlanetsVariables.json");
        Planets planets = JsonConvert.DeserializeObject<Planets>(jsonContent);
        PlanetsVariables planetsVariables = null;
        switch (gameObject.name) {
            case "Mercury":
                planetsVariables = planets.Mercury;
                break;
            case "Venus":
                planetsVariables = planets.Venus;
                break;
            case "Earth":
                planetsVariables = planets.Earth;
                break;
            case "Mars":
                planetsVariables = planets.Mars;
                break;
            case "Jupiter":
                planetsVariables = planets.Jupiter;
                break;
            case "Saturn":
                planetsVariables = planets.Saturn;
                break;
            case "Uranus":
                planetsVariables = planets.Uranus;
                break;
            case "Neptune":
                planetsVariables = planets.Neptune;
                break;
        }

        if (planetsVariables != null) {
            taillePlanete = planetsVariables.size * scale;
            longueurEllipse = planetsVariables.ellipseLength * scale;
            largeurEllipse = planetsVariables.ellipseWidth * scale;
            hauteurEllipse = planetsVariables.ellipseHeight * scale;
            rotationx = planetsVariables.rotationx;
            rotationy = planetsVariables.rotationy;
            rotationz = planetsVariables.rotationz;
            vitesse = planetsVariables.speed;
            waitTime = planetsVariables.waitTime;
        }
        else {
            Debug.LogError($"Planète inconnue : {gameObject.name}");
        }
    }

    public void onClick() {
        isClicked = !isClicked;
        GameObject gameObjectCible = GameObject.Find("Player Variant");
        PlayerManager scriptCible = gameObjectCible.GetComponent<PlayerManager>();
        
        if(isClicked){
            SceneManager.LoadScene(2);
            scriptCible.setPosition(positionSunx + (positionActux / 5) * 4, 300, positionSunz + (positionActuz / 5) * 4);
        }
        else{
            SceneManager.LoadScene(1);
            scriptCible.setPosition(500, 500, 500);
        }
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name != "TeleportArea" && collision.gameObject.name != "HandColliderLeft(Clone)" && collision.gameObject.name != "HandColliderRight(Clone)" /*&& collision.gameObject.name != "HeadCollider"*/) {
            isGrab = true;
            if (collision.gameObject.name == "Sphere") // Sphere collider du soleil
                transform.position = new Vector3(0, 20000, 0); // chut
        }
    }
}
