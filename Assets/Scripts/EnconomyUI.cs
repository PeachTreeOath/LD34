﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class EnconomyUI : MonoBehaviour {

	//public GameObject canvas;
	//private RectTransform canvasXfrom;
	//GameObject moneyLabel;
	GameObject moneyValue;
	//GameObject popLabel;
	public GameObject popValue;

	// Use this for initialization
	void Start () {
		//canvas = GameObject.Find("Canvas");
        //if(canvas == null) {
        //    Debug.LogError("Canvas not set on EnconomyUI");
        //}
        if(popValue == null) {
            Debug.LogError("PopValue text not set on EnconomyUI");
        }
		//canvasXfrom = canvas.GetComponent<RectTransform>();

		//GameObject textFab = Resources.Load("Prefabs/UI/UIText") as GameObject;
		//moneyLabel = Instantiate(textFab);
		//moneyLabel.transform.SetParent(canvas.transform, false);
		//moneyLabel.GetComponent<Text>().text = "Money: $";
		//moneyLabel.GetComponent<Text>().raycastTarget = false;
		//moneyLabel.GetComponent<Text>().color = Color.black;
		//moneyValue = Instantiate(textFab);
		//moneyValue.transform.SetParent(canvas.transform, false);
		//moneyValue.GetComponent<Text>().text = "0";
		//moneyValue.GetComponent<Text>().raycastTarget = false;
		//moneyValue.GetComponent<Text>().color = Color.black;

		//popLabel = Instantiate(textFab);
		//popLabel.transform.SetParent(canvas.transform, false);
		//popLabel.GetComponent<Text>().text = "Population: ";
		//popLabel.GetComponent<Text>().raycastTarget = false;
		//popLabel.GetComponent<Text>().color = Color.black;
		//popValue = Instantiate(textFab);
		//popValue.transform.SetParent(canvas.transform, false);
		//popValue.GetComponent<Text>().raycastTarget = false;
		//popValue.GetComponent<Text>().color = Color.black;
		popValue.GetComponent<Text>().text = "0";

		//popLabel.GetComponent<RectTransform>().position = new Vector3(canvasXfrom.rect.width * .1f, canvasXfrom.rect.height * .95f, 0);
		//popValue.GetComponent<RectTransform>().position = popLabel.GetComponent<RectTransform>().position + new Vector3(popLabel.GetComponent<RectTransform>().rect.width * .65f, 0, 0);

		//moneyLabel.GetComponent<RectTransform>().position = new Vector3(canvasXfrom.rect.width * .1f, canvasXfrom.rect.height * .9f, 0);
		//moneyValue.GetComponent<RectTransform>().position = moneyLabel.GetComponent<RectTransform>().position + new Vector3(moneyLabel.GetComponent<RectTransform>().rect.width * .55f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//moneyValue.GetComponent<Text>().text = String.Format("{0:0.00}", Globals.gameState.money) + "";  //money maker will handle money display in depth, we can combine these if it doesnt get too ugly
		popValue.GetComponent<Text>().text = Globals.gameState.population + "";
	}
}
