using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerManager : MonoBehaviour
{
    float positionX;
    float positionY;
    float positionZ;

    void Start() {
        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;
    }

    public void playerClickPlanet(float positionSunx, float positionSunz, float positionActux, float positionActuy, float positionActuz, GameObject centrePlanet) {
        Vector3 newVector = new Vector3(positionSunx + (positionActux / 5) * 4, transform.position.y + 300, positionSunz + (positionActuz / 5) * 4);
        transform.position = newVector;
        transform.localScale = new Vector3(1, 1, 1);
        transform.LookAt(new Vector3(centrePlanet.transform.position.x, transform.position.y, centrePlanet.transform.position.z));
        //transform.Translate(Vector3.right * Time.deltaTime);
    }

    public void initialPlace() {
        Vector3 newVector = new Vector3(positionX, positionY, positionZ);
        transform.position = newVector;
        transform.localScale = new Vector3(500, 500, 500);
        //transform.LookAt(centreSun.transform.position);
        //transform.Translate(Vector3.right * Time.deltaTime);
    }

    public void setPosition(float x, float y, float z)
    {
        transform.position = new Vector3(x, y, z);
    }
}
