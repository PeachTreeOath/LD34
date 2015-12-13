using UnityEngine;
using System.Collections;

public class AnchorTo : MonoBehaviour {

    public GameObject target;
    public Vector3 offset = Vector3.zero;

	// Use this for initialization
	void Start () {
        gameObject.transform.position = target.transform.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
            gameObject.transform.position = target.transform.position + offset;
        else
            Destroy(gameObject);
    }
}
