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
			}

			Destroy (col.gameObject);
		}
	}
}
