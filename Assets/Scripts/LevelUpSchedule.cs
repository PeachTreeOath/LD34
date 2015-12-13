using UnityEngine;
using System.Collections;

public class LevelUpSchedule : MonoBehaviour {

	public static int [,] schedule = new int[,] {
		{5, 10, 20, 30},
		{100, 300, 750, 1500},
		{4000, 9000, 20000, 40000},
		{100000, 170000, 300000, 600000},
		{1000000, 2000000, 5000000, 10000000}
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Globals.gameState.cityProgress < Globals.cityTierCount)
		{
			if(Application.loadedLevel < schedule.GetLength(0))
			{
				Globals.gameState.moneyGoal = schedule[Application.loadedLevel, Globals.gameState.cityProgress];
			}else if (!Globals.gameState.win)
			{
				Globals.gameState.win = true;
				Camera.main.gameObject.AddComponent<VictoryDisplayer>();
			}
		}
	}
}
