using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	public static float musicVolume = 100;
	public static float sfxVolume = 25;
	public static GameObject bgMusic;
	public static GameState gameState = null;

	void Start()
	{
		if(gameState == null)
		{
			gameState = ScriptableObject.CreateInstance<GameState>();
			gameState.productionRates = new System.Collections.Generic.List<float>();
		}
	}
}
