using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ProductionManagerUI : MonoBehaviour {

	public List<string> goods;
	List<Slider> sliders;
    private GlobalInputHandler GIH; 

	// Use this for initialization
	void Start () {
        GIH = GameObject.Find("GlobalInputHandler").GetComponent<GlobalInputHandler>();
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
		/*GameObject scrollerBG = Instantiate(Resources.Load("Prefabs/UI/ScrollerBG")) as GameObject;
		scrollerBG.transform.SetParent(canvasXfrom, false);
		scrollerBG.transform.localScale = new Vector3(scrollerBG.transform.localScale.x * 3.5f, scrollerBG.transform.localScale.y, scrollerBG.transform.localScale.z);
		scrollerBG.GetComponent<RectTransform>().position = new Vector3(canvasXfrom.rect.width*.856f, canvasXfrom.rect.height - 70, 0);*/

		for(int i = 0; i < goods.Count; i++)
		{
			GameObject slider = Instantiate(sliderFab);
            GIH.registerForDrag(slider, onDrag, onDrag, onDrag);
            GIH.registerForClick(slider, onClick);
			GameObject text = Instantiate(textFab);
			text.GetComponent<Text>().text = goods[i];
			text.GetComponent<Text>().raycastTarget = false;
			slider.transform.SetParent(canvas.transform, false);

			text.transform.SetParent(canvas.transform, false);

			RectTransform sxform = slider.GetComponent<RectTransform>();
			sxform.position = (new Vector3(canvasXfrom.rect.width*.90f, canvasXfrom.rect.height - sxform.rect.height * (i+2.5f) * 1.2f, 0));
			sliders.Add(slider.GetComponent<Slider>());
			Globals.gameState.productionRates.Add(slider.GetComponent<Slider>().value);
			Globals.gameState.productionCounts.Add(0);
			RectTransform txform = text.GetComponent<RectTransform>();
			txform.transform.position = sxform.position + new Vector3(-sxform.rect.width/1.85f, 0, 0);
		}

		GameObject textTitle = Instantiate(textFab);
		textTitle.GetComponent<Text>().text = "Set Production Rates";
		textTitle.GetComponent<Text>().raycastTarget = false;
		textTitle.transform.SetParent(canvas.transform, false);
		textTitle.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;

		RectTransform sxform2 = sliders[0].GetComponent<RectTransform>();
		RectTransform txform2 = textTitle.GetComponent<RectTransform>();
		txform2.transform.position = sxform2.position + new Vector3(-30, sxform2.rect.height/2 + txform2.rect.height/2 + 5, 0);
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

    bool onClick(Vector3 pos) {
        Debug.Log("Prod onClick");
        return true;
    }

    bool onDrag(Vector3 pos) {
        Debug.Log("Prod onDrag");
        return true; //intercept drags over the control
    }

	void LateUpdate()
	{
		for(int i = 0; i < sliders.Count; i++)
		{
			Globals.gameState.productionRates[i] = sliders[i].value;
		}
	}
}
