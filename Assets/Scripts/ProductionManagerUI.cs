using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProductionManagerUI : MonoBehaviour {

	public List<string> goods;
	Camera mainCam;
	List<GameObject> sliders;
	List<GameObject> bars;


	// Use this for initialization
	void Start () {
		mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

		BuildUI();
	}

	void BuildUI()
	{
		sliders = new List<GameObject>();
		bars = new List<GameObject>();

		for(int i = 0; i < goods.Count; i++)
		{
			GameObject slider = Utility.CreateBillboard();
			slider.transform.localScale *= .001f;
			Utility.SetObjectWidth(ref slider, Screen.width * .33f, mainCam);
			Utility.SetObjectHeight(ref slider, Screen.height * .03f, mainCam);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
