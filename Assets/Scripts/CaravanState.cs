using UnityEngine;
using System.Collections;

public class CaravanState : MonoBehaviour {

	public Globals.GoodTypeEnum goodType = Globals.GoodTypeEnum.CORN;
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
			Debug.Log(Time.time + " caravan hit mult " + col.gameObject.GetComponent<Multiplier>().multiplier + " was " + multiplier);
			multiplier += col.gameObject.GetComponent<Multiplier>().multiplier;
		}
	}
}
