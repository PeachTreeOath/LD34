using UnityEngine;
using System.Collections;

public class TimedObject : MonoBehaviour {

	public float lifeTime;
	float lifeTimer;

	// Use this for initialization
	void Start () {
		lifeTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lifeTimer > lifeTime)
		{
			Destroy(gameObject);
		}
	}
}
