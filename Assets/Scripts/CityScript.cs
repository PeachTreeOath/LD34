using UnityEngine;
using System.Collections;

public class CityScript : MonoBehaviour {

	public GameObject caravanPrefab;
	public float shotSpeed = 20f;
    private GlobalInputHandler GIH;

	// Use this for initialization
	void Start () {
       GIH = GameObject.Find("GlobalInputHandler").GetComponent<GlobalInputHandler>();
	}
	
	// Update is called once per frame
	void Update () {
        GIH.setGlobalClickHandler(onClick);
	}

    private bool onClick(Vector3 pos) {
        Shoot();
        return true;
    }

	private void Shoot()
	{
		GameObject caravan = (GameObject)Instantiate(caravanPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
		RotationScript rot = GetComponent <RotationScript>();
		Rigidbody2D rigidBody = caravan.GetComponent<Rigidbody2D> ();
		rigidBody.velocity = new Vector2 (Mathf.Cos(rot.getAngle()) * shotSpeed, Mathf.Sin(rot.getAngle()) * shotSpeed);
	}
}
