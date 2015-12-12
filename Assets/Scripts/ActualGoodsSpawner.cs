using UnityEngine;
using System.Collections;

public class ActualGoodsSpawner : MonoBehaviour {

    public GameObject tradeGoodPrefab;
    public float spawnDelay;

	
	void Start () {
        Invoke("doSpawn", spawnDelay); //TODO this needs to get way fancier
    }

    private void doSpawn()
    {
        GameObject tradeGood = Instantiate(tradeGoodPrefab, transform.position, Quaternion.identity) as GameObject;
        TradeGood tradeGoodScript = tradeGood.GetComponent<TradeGood>(); //TODO this seems hella hacky...
        tradeGoodScript.spawner = gameObject;
    }
	
    void onGoodConsumed()
    {
        Invoke("doSpawn", spawnDelay);
    }

}
