using UnityEngine;
using System.Collections;

public class LevelUpSchedule : MonoBehaviour {

	public static int [,] schedule = new int[,] {
		{5, 10, 20, 30},
		{40, 50, 60, 70},
		{85, 100, 115, 130},
		{145, 160, 175, 190},
		{210, 230, 250, 275}
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Globals.gameState.cityProgress < Globals.cityTierCount)
		{
			Globals.gameState.moneyGoal = schedule[Application.loadedLevel, Globals.gameState.cityProgress];
		}
	}
}
