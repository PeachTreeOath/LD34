using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	public enum GoodTypeEnum { CORN, OIL, TIMBER, LAST }; //TODO

	public static float musicVolume = 100;
	public static float sfxVolume = 25;
	public static GameObject bgMusic;
	public static GameState gameState = null;
	public static GameObject playerCity;
	public static int cityTierCount = 4;

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

    public static Vector3 getBotLeftScreenPointInWorldSpace() {
        return Camera.main.ScreenToWorldPoint(new Vector3(0,0,0));
    }

    public static Vector3 getTopRightScreenPointInWorldSpace() {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

}
