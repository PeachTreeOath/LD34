using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProductionManagerUI : MonoBehaviour {

	public List<string> goods;
	Camera mainCam;
	List<Slider> sliders;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

		BuildUI();
	}

	void BuildUI()
	{
		Globals.gameState.productionRates = new List<float>();
		sliders = new List<Slider>();
		GameObject sliderFab = Resources.Load("Prefabs/UI/Slider") as GameObject;
		GameObject textFab = Resources.Load("Prefabs/UI/UIText") as GameObject;
		GameObject canvas = GameObject.Find("Canvas");
		RectTransform canvasXfrom = canvas.GetComponent<RectTransform>();
		for(int i = 0; i < goods.Count; i++)
		{
			GameObject slider = Instantiate(sliderFab);
			GameObject text = Instantiate(textFab);
			text.GetComponent<Text>().text = goods[i];
			text.GetComponent<Text>().raycastTarget = false;
			slider.transform.SetParent(canvas.transform, false);
			text.transform.SetParent(canvas.transform, false);

			RectTransform sxform = slider.GetComponent<RectTransform>();
			sxform.position = (new Vector3(canvasXfrom.rect.width/2, sxform.rect.height * (goods.Count + 1 - i) * 1.2f, 0));
			sliders.Add(slider.GetComponent<Slider>());
			Globals.gameState.productionRates.Add(slider.GetComponent<Slider>().value);
			RectTransform txform = text.GetComponent<RectTransform>();
			txform.transform.position = sxform.position + new Vector3(-sxform.rect.width/1.85f, 0, 0);
		}

		GameObject textTitle = Instantiate(textFab);
		textTitle.GetComponent<Text>().text = "Production";
		textTitle.GetComponent<Text>().raycastTarget = false;
		textTitle.transform.SetParent(canvas.transform, false);

		RectTransform sxform2 = sliders[0].GetComponent<RectTransform>();
		RectTransform txform2 = textTitle.GetComponent<RectTransform>();
		txform2.transform.position = sxform2.position + new Vector3(0, sxform2.rect.height/2 + txform2.rect.height/2 + 5, 0);
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < sliders.Count; i++)
		{
			if(Globals.gameState.productionRates[i] != sliders[i].value)
			{
				float sum = 0;
				for(int j = 0; j < sliders.Count; j++)
				{
					sum += sliders[j].value;
				}
				if(sum > 1)
				{
					float delta = (sum - 1)/(sliders.Count - 1);
					Debug.Log(Time.time + " delta " + delta);
					for(int j = 0; j < sliders.Count; j++)
					{
						if(i != j)
						{
							sliders[j].value -= delta;
							if(sliders[j].value < 0)
							{
								sliders[j].value = 0;
							}
						}
					}
				}
			}
		}
	}

	void LateUpdate()
	{
		for(int i = 0; i < sliders.Count; i++)
		{
			Globals.gameState.productionRates[i] = sliders[i].value;
		}
	}
}
