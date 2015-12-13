using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnterLevel : MonoBehaviour {

	public float growSpeed;
	object [] allObjs;
	List<Vector3> startScales;
	List<Vector3> endScales;
	float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.deltaTime;
		startScales = new List<Vector3>();
		endScales = new List<Vector3>();
		allObjs = GameObject.FindObjectsOfType(typeof(GameObject));
		for(int i = 0; i < allObjs.Length; i++)
		{
			GameObject g = (GameObject)allObjs[i];
			startScales.Add(g.transform.localScale * .01f);
			endScales.Add(g.transform.localScale);


			if(!(g.tag.Equals("Background") || g.layer == LayerMask.NameToLayer("UI")))
			{
				g.transform.localScale = g.transform.localScale * .01f;
				Debug.Log(Time.time + " shrunk " +  g.name + " tag " + g.tag + " layer " + g.layer + " UI layer " + LayerMask.NameToLayer("UI"));
			}
		}
	}

	// Update is called once per frame
	void Update () {
		startTime += Time.deltaTime;
		for(int i = 0; i < allObjs.Length; i++)
		{
			GameObject g = (GameObject) allObjs[i];
			if(!(g.tag.Equals("Background") || g.layer == LayerMask.NameToLayer("UI")))
			{
				Debug.Log(Time.time + " growing");
				g.transform.localScale = Vector3.Lerp(startScales[i], endScales[i], startTime * growSpeed);
			}
		}

		if(Mathf.Abs(Vector3.Distance(endScales[0], ((GameObject) allObjs[0]).transform.localScale)) < .01f)
		{
			for(int i = 0; i < allObjs.Length; i++)
			{
				GameObject g = (GameObject) allObjs[i];
				if(!(g.tag.Equals("Background") || g.layer == LayerMask.NameToLayer("UI")))
				{
					g.transform.localScale = endScales[i];
				}
			}
			Destroy(this);
		}
	}
}
