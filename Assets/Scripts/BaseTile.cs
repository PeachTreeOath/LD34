using UnityEngine;
using System.Collections;

public class BaseTile : MonoBehaviour {

	public float scale = 0;
	public float vel = .001f;
	public float accel = .00002f;

	private float currAccel;
	private bool increasing = true;

	// Use this for initialization
	void Start () {
		currAccel = accel;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3(scale,scale,1);
		if (scale > 1) {
			if (increasing) {
				increasing = !increasing;
				accel = 
			}
			currAccel = -accel;
		} else {
			if (increasing) {
				increasing = !increasing;
			}
			currAccel = accel;
		}

		scale += vel;	
		vel += currAccel;
	}

	public void SpringToLife()
	{


	}

}
