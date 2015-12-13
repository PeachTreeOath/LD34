using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	public enum GoodTypeEnum { CORN, OIL, TIMBER, LAST }; //TODO

	public static float musicVolume = 100;
	public static float sfxVolume = 25;
	public static GameObject bgMusic;
	public static GameState gameState = null;
	public static GameObject playerCity;

	void Start()
	{
		playerCity = GameObject.FindGameObjectWithTag("Player");
		if(gameState == null)
		{
			gameState = ScriptableObject.CreateInstance<GameState>();
			gameState.productionRates = new System.Collections.Generic.List<float>();
			gameState.productionCounts = new System.Collections.Generic.List<int>();
		}
	}
}
