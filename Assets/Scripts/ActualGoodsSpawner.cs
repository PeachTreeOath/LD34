using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ActualGoodsSpawner : MonoBehaviour {

	public Globals.GoodTypeEnum goodType;
    public GameObject tradeGoodPrefab;
    public float baseSpawnDelay;
	float goodsSpawnTime;
	TextMesh countText;

	float spawnTimer;
	float spawnTime;

    private GResource getModel() {
        return Globals.gameState.getResource(goodType);
    }

	public void UpdateSpawnTimer()
	{
		float minMon = Mathf.Max(1, Globals.gameState.money);
        GResource r = getModel();
        if(r != null) {
		    goodsSpawnTime = Mathf.Max(0, baseSpawnDelay - r.prodRate * (Globals.gameState.population/minMon));
		    spawnTime = goodsSpawnTime;
        }
	}

	void Start () {
		spawnTimer = Time.time;
		GameObject go = Utility.CreateTextObject(Resources.Load("Fonts/calibri") as Font, Resources.Load("Fonts/calibri_mat") as Material);
		go.transform.localScale *= .03f;
		countText = go.GetComponent<TextMesh>();
		countText.anchor = TextAnchor.MiddleLeft;
		countText.transform.position = gameObject.transform.position + Vector3.right * .5f;
        GResource res = ScriptableObject.CreateInstance<GResource>();
        res.init(goodType, 0, 1);
        Globals.gameState.addResource(res);
		UpdateSpawnTimer();
		//Invoke("doSpawn", goodsSpawnTime); //TODO this needs to get way fancier
    }

	void Update()
	{
        UpdateSpawnTimer(); //If this calculation becomes too intense we can update on change only
		if(getModel().prodCount > 0)
		{
            countText.text = "x" + getModel().prodCount;
		}else
		{
			countText.text = "";
		}

		if(Time.time - spawnTimer >= spawnTime)
		{
			doSpawn();
		}
	}

    private void doSpawn()
    {
        GResource r = getModel();
        r.prodCount++;
        countText.text = "x" + r.prodCount;
        
		if(r.prodCount == 1) {
	        GameObject tradeGood = Instantiate(tradeGoodPrefab, transform.position, Quaternion.identity) as GameObject;
			DontDestroyOnLoad(tradeGood);
			BouncyScaler bs = tradeGood.AddComponent<BouncyScaler>();
			bs.targetScale = 1.4f;
			bs.epsilon = .01f;
			bs.scaleSpeed = 1f;
			tradeGood.GetComponent<TradeGood>().goodType = goodType;
	        TradeGood tradeGoodScript = tradeGood.GetComponent<TradeGood>(); //TODO this seems hella hacky...
	        tradeGoodScript.spawner = gameObject;
		}

		UpdateSpawnTimer();
		spawnTimer = Time.time;
		//Invoke("doSpawn", goodsSpawnTime);
    }

    public void debugSpawnImmediate() {
        doSpawn();
    }
	
    void onGoodConsumed()
    {
		
	}
}
