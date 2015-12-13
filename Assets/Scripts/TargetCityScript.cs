using UnityEngine;
using System.Collections;

public class TargetCityScript : MonoBehaviour {

	public Globals.GoodTypeEnum desiredGood;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "CaravanPrefab(Clone)") {
			//Debug.Log(Time.time + " caravan arrived " + col.gameObject.GetComponent<CaravanState>().goodType + " " + desiredGood);
			if(desiredGood == col.gameObject.GetComponent<CaravanState>().goodType)
			{
				float multiplier = col.gameObject.GetComponent<CaravanState>().multiplier;
				if(multiplier > 0)
				{
					Globals.gameState.money += col.gameObject.GetComponent<CaravanState>().value * multiplier;
				}else
				{
					Globals.gameState.money += col.gameObject.GetComponent<CaravanState>().value;
				}

				Debug.Log(Time.time + " multiplier " + multiplier + " money " + Globals.gameState.money + "  value " + col.gameObject.GetComponent<CaravanState>().value);
				GetComponent<CityGrowth> ().LevelUp (false);
			}
			
			Destroy (col.gameObject);
		}
	}
}
