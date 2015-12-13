using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : ScriptableObject {

	public List<int> productionCounts;
	public List<float> productionRates;
	public float money;
	public float population;
	public float moneyGoal;
	public int cityProgress;
}
