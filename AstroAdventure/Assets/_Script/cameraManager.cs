using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 5F;
	public float sensitivityY = 5F;

	public float minY = -60F;
	public float maxY = 60F;

	float rotationY = 0F;

	float x;
	float y;
	float z;

	float xRotate;
	float yRotate;
	float zRotate;


    void Start ()
	{

		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;

		xRotate = Camera.main.transform.localEulerAngles.x;
		yRotate = Camera.main.transform.localEulerAngles.y;
		zRotate = Camera.main.transform.localEulerAngles.z;
		
	}

	void Update ()
	{

	}

	public void testModifCam(float positionSunx, float positionSunz, float positionActux, float positionActuy,  float positionActuz, GameObject centreEarth){
		Vector3 newVector = new Vector3(positionSunx + (positionActux / 2), transform.position.y, positionSunz + (positionActuz / 2));
		transform.position = newVector;
		transform.LookAt(centreEarth.transform.position);
		transform.Translate(Vector3.right * Time.deltaTime);
	}

	public void initialPlace(GameObject centreSun){
		Vector3 newVector = new Vector3(x, y, z);
		transform.position = newVector;
		transform.LookAt(centreSun.transform.position);
		transform.Translate(Vector3.right * Time.deltaTime);
	}
}