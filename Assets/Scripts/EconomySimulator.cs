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
		GameObject goodIconFab = Resources.Load("Prefabs/GoodIcon") as GameObject;
		for(int i = 0; i < cities.Length; i++)
		{
			timers.Add(Time.time);
			times.Add(Random.Range(minTimer, maxTimer));
			cities[i].GetComponent<TargetCityScript>().desiredGood = (Globals.GoodTypeEnum)Random.Range(0, (int)Globals.GoodTypeEnum.LAST);
			string iconName = cities[i].GetComponent<TargetCityScript>().desiredGood.ToString().ToLower();
			iconName = iconName.Substring(0, 1).ToUpper() + iconName.Substring(1);
			GameObject icon = Instantiate(goodIconFab);
			SpriteRenderer rend = icon.GetComponent<SpriteRenderer>();
			icons.Add(icon);
			rend.sprite = Resources.Load<Sprite>("Textures/Trade Goods/"+iconName);
			icon.transform.position = cities[i].transform.position + Vector3.up * ((cities[i].GetComponent<Renderer>().bounds.extents.y + rend.bounds.extents.y) * 1.15f) + Vector3.back;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < times.Count; i++)
		{
			if(Time.time - timers[i] > times[i])
			{
				timers[i] = Time.time;
				times[i] = (Random.Range(minTimer, maxTimer));
				cities[i].GetComponent<TargetCityScript>().desiredGood = (Globals.GoodTypeEnum)Random.Range(0, (int)Globals.GoodTypeEnum.LAST);
				string iconName = cities[i].GetComponent<TargetCityScript>().desiredGood.ToString().ToLower();
				iconName = iconName.Substring(0, 1).ToUpper() + iconName.Substring(1);
				icons[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Trade Goods/"+iconName);
			}
			icons[i].transform.position = cities[i].transform.position + Vector3.up * ((cities[i].GetComponent<Renderer>().bounds.extents.y + icons[i].GetComponent<Renderer>().bounds.extents.y) * 1.15f) + Vector3.back;
		}
	}
}
