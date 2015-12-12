using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Collider2D col = gameObject.GetComponent<Collider2D>();
        if(col == null) {
            Debug.LogError("Person needs a Collider2D: " + gameObject.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "CaravanPrefab(Clone)") {
            Debug.Log("Person hit by chariot");
			Destroy (gameObject);
		} else {
            Debug.Log("Person hit by: " + col.gameObject.name);
        }
	}
}
