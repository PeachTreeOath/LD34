﻿using UnityEngine;
using System.Collections;

public class MainCityProgress : MonoBehaviour {

	GameObject progressBar;
	Renderer rend;

	// Use this for initialization
	void Start () {
		Globals.gameState.cityProgress = 0;
		progressBar = Instantiate(Resources.Load("Prefabs/LoadingBar") as GameObject) as GameObject;
		rend = progressBar.GetComponent<Renderer>();
		rend.material.SetFloat("_Progress", 0);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateProgressUI();
		UpdateProgress();
	}

	void UpdateProgress()
	{
		if(Globals.gameState.money >= LevelUpSchedule.schedule[Application.loadedLevel, Globals.gameState.cityProgress])
		{
			Globals.gameState.cityProgress++;
			if(Globals.gameState.cityProgress == Globals.cityTierCount)
			{
				Destroy(this);
				Camera.main.gameObject.AddComponent<LoadNextLevel>().shrinkSpeed = 1;
			}
		}
	}

	void UpdateProgressUI()
	{
		int prevGoal = 0;
		if(Application.loadedLevel > 0)
		{
			int idx2 = (Globals.gameState.cityProgress-1);
			if(idx2 < 0)
			{
				idx2 = Globals.cityTierCount-1;
			}
			prevGoal = LevelUpSchedule.schedule[Application.loadedLevel-1, idx2];
		}else if (Globals.gameState.cityProgress > 0)
		{
			prevGoal = LevelUpSchedule.schedule[Application.loadedLevel, Globals.gameState.cityProgress-1];
		}
		rend.material.SetFloat("_Progress", (Globals.gameState.money-prevGoal)/(Globals.gameState.moneyGoal-prevGoal));
	}
}