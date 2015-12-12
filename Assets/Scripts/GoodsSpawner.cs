using UnityEngine;
using System.Collections;

//Handles spawning shit
public class GoodsSpawner: MonoBehaviour {

    public GameObject personPrefab;
    public float delayBetweenSpawnSec;
    public float numToSpawn;

    public LocationManager locMan;
    private float lastPersonSpawnTime;

    //pre-start setup
    void Awake () {
        scheduleSpawnPeeps(delayBetweenSpawnSec);
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void scheduleSpawnPeeps(float inSec) {
        Invoke("doSpawn", inSec);
    }

    //Called when it is time to spawn some peeps
    private void doSpawn() {
        for(int i = 0; i < numToSpawn; i++) {
            Vector3 pos = locMan.getPersonSpawnLoc();
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity) as GameObject;
        }
        scheduleSpawnPeeps(delayBetweenSpawnSec);

    }

}
