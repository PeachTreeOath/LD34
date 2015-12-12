using UnityEngine;
using System.Collections;

public class CaravanState : MonoBehaviour {

	public Globals.GoodTypeEnum goodType;
	public float value = 5;
	public float multiplier = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Multiplier"))
		{
			multiplier += col.gameObject.GetComponent<Multiplier>().multiplier;
		}
	}
}
