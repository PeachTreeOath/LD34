using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadNextLevel : MonoBehaviour {

	public float shrinkSpeed;
	object [] allObjs;
	List<Vector3> startScales;
	float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.deltaTime;
		startScales = new List<Vector3>();
		allObjs = GameObject.FindObjectsOfType(typeof(GameObject));
		for(int i = 0; i < allObjs.Length; i++)
		{
			startScales.Add(((GameObject) allObjs[i]).transform.localScale);
		}
	}
	
	// Update is called once per frame
	void Update () {
		startTime += Time.deltaTime;
		for(int i = 0; i < allObjs.Length; i++)
		{
			GameObject g = (GameObject) allObjs[i];
			if(g != null &&
				!(g.tag.Equals("Background") || g.layer == LayerMask.NameToLayer("UI")))
			{
				g.transform.localScale = Vector3.Lerp(startScales[i], startScales[i] * .001f, startTime * shrinkSpeed);
			}
		}

		//if(Mathf.Abs(Vector3.Distance(startScales[0], ((GameObject) allObjs[0]).transform.localScale)) < .001f)
		if(Camera.main.gameObject.transform.localScale.magnitude <= .002f)
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
