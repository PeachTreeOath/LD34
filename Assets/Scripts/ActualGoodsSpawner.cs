using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActualGoodsSpawner : MonoBehaviour {

	public Globals.GoodTypeEnum goodType;
    public GameObject tradeGoodPrefab;
    public float baseSpawnDelay;
	float goodsSpawnTime;
	TextMesh countText;
	
	void Start () {
		GameObject go = Utility.CreateTextObject(Resources.Load("Fonts/calibri") as Font, Resources.Load("Fonts/calibri_mat") as Material);
		go.transform.localScale *= .03f;
		countText = go.GetComponent<TextMesh>();
		countText.anchor = TextAnchor.MiddleLeft;
		countText.transform.position = gameObject.transform.position + Vector3.right * .5f;
		goodsSpawnTime = Mathf.Max(0, baseSpawnDelay - Globals.gameState.population * Globals.gameState.productionRates[(int)goodType]);
		Invoke("doSpawn", goodsSpawnTime); //TODO this needs to get way fancier
    }

	void Update()
	{
		if(Globals.gameState.productionCounts[(int)goodType] > 0)
		{
			countText.text = "x" + Globals.gameState.productionCounts[(int)goodType];
		}else
		{
			countText.text = "";
		}
	}

    private void doSpawn()
    {
		Globals.gameState.productionCounts[(int)goodType]++;
		countText.text = "x" + Globals.gameState.productionCounts[(int)goodType];
		if(Globals.gameState.productionCounts[(int)goodType] == 1)
		{
	        GameObject tradeGood = Instantiate(tradeGoodPrefab, transform.position, Quaternion.identity) as GameObject;
			BouncyScaler bs = tradeGood.AddComponent<BouncyScaler>();
			bs.targetScale = 1.4f;
			bs.epsilon = .01f;
			bs.scaleSpeed = 1f;
			tradeGood.GetComponent<TradeGood>().goodType = goodType;
	        TradeGood tradeGoodScript = tradeGood.GetComponent<TradeGood>(); //TODO this seems hella hacky...
	        tradeGoodScript.spawner = gameObject;
		}

		goodsSpawnTime = Mathf.Max(0, baseSpawnDelay - Globals.gameState.population * Globals.gameState.productionRates[(int)goodType]);
		Invoke("doSpawn", goodsSpawnTime);
    }
	
    void onGoodConsumed()
    {
		
	}
}
