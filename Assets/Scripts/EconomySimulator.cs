using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EconomySimulator : MonoBehaviour {

	public float minTimer;
	public float maxTimer;
	GameObject [] cities;
	List<float> timers;
	List<float> times;
	List<GameObject> icons;

	// Use this for initialization
	void Start () {
		timers = new List<float>();
		times = new List<float>();
		icons = new List<GameObject>();
		cities = GameObject.FindGameObjectsWithTag("City");
		for(int i = 0; i < cities.Length; i++)
		{
			timers.Add(Time.time);
			times.Add(Random.Range(minTimer, maxTimer));
			cities[i].GetComponent<TargetCityScript>().desiredGood = (Globals.GoodTypeEnum)Random.Range(0, (int)Globals.GoodTypeEnum.LAST);
			GameObject icon = Utility.CreateBillboard();
			icon.transform.localScale *= .01f;
			icons.Add(icon);
			string iconName = cities[i].GetComponent<TargetCityScript>().desiredGood.ToString().ToLower();
			iconName = iconName.Substring(0, 1).ToUpper() + iconName.Substring(1);
			icon.GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/Trade Goods/"+iconName) as Texture2D;

			icon.transform.position = cities[i].transform.position + Vector3.up * ((cities[i].GetComponent<Renderer>().bounds.extents.y + icon.GetComponent<Renderer>().bounds.extents.y) * 1.15f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < times.Count; i++)
		{
			if(Time.time - timers[i] > times[i])
			{
				times[i] = Time.time;
				times[i] = (Random.Range(minTimer, maxTimer));
				cities[i].GetComponent<TargetCityScript>().desiredGood = (Globals.GoodTypeEnum)Random.Range(0, (int)Globals.GoodTypeEnum.LAST);
				string iconName = cities[i].GetComponent<TargetCityScript>().desiredGood.ToString().ToLower();
				iconName = iconName.Substring(0, 1).ToUpper() + iconName.Substring(1);
				icons[i].GetComponent<Renderer>().material.mainTexture = Resources.Load("Textures/Trade Goods/"+iconName) as Texture2D;
			}
		}
	}
}
