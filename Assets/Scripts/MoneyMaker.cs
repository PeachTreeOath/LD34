using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoneyMaker : MonoBehaviour {

    public GameObject moneyParent; //container

    //List of prefabs for money pictures in order from smallest to largest
    public List<GameObject> orderedMoneyPrefabs;

    private List<Money> orderedMoney;
    private List<int> numDisplayed; //indices correspond to orderedMoneyPrefab

    private float lastMoneyUpdate = 0;
    private float moneyUpdateThresh = 1;

    private List<GameObject> activeObjs;

	// Use this for initialization
	void Start () {
        activeObjs = new List<GameObject>();
        orderedMoney = new List<Money>();
        numDisplayed = new List<int>();
        for (int i = 0; i < orderedMoneyPrefabs.Count; i++){
            Money m = orderedMoneyPrefabs[i].GetComponentInChildren<Money>(true);
            if(m == null) {
                Debug.LogError("Shits broke at i=" + i);
            }
            orderedMoney.Add(m);
            numDisplayed.Add(0);
        }
        moneyUpdateThresh = orderedMoney[0].value;
	}
	
	// Update is called once per frame
	void Update () {
        float newMoney = Globals.gameState.money - lastMoneyUpdate;

        if(newMoney >= moneyUpdateThresh) {
            //Debug.Log("Adding money: " + newMoney); 
            float remain = addAmt(newMoney);
            lastMoneyUpdate += newMoney - remain;
            consolidate();
        }
	}

    //updates bins and returns the remainder that didn't fit in the lowest bin 
    private float addAmt(float amt) {
        float workingAmt = amt;
        int i = 0;
        int bundles = (int) (workingAmt / orderedMoney[i].value);
        numDisplayed[i] += bundles;
        workingAmt = workingAmt - (bundles * orderedMoney[i].value);
        //this is pretty hacky, we will add on new icons here for the lowest amount
        //which will stick around until they need to be consolidated
        //Otherwise these would not get displayed
        addToDisplay(orderedMoneyPrefabs[0], bundles);
        return workingAmt;
    }

    //regroup money together
    private void consolidate() {
        bool changed = false;
        for(int i = 0; i < orderedMoney.Count-1; i++) {
            float myFact = orderedMoney[i].value;
            float nextFact = orderedMoney[i + 1].value;

            float myVal = numDisplayed[i] * myFact;
            int upgradeBundles = (int) (myVal / nextFact);
            float myRemain = myVal - (upgradeBundles * nextFact); //should be an even amount for this bin but clamp it anyways
            int myNewBundles = (int)(myRemain / myFact);
            if(upgradeBundles > 0) {
                changed = true;
                //move a bundle from current bin to next bin
                numDisplayed[i] = myNewBundles;
                numDisplayed[i + 1] += upgradeBundles;
            }
        }
        if(changed) {
            display();
        } 

    }

    private void display() {
        //clear existing
        for(int i = activeObjs.Count -1; i >=0; i--) {
            Destroy(activeObjs[i]);
        }
        activeObjs.Clear();

        //add new objs in reverse order so largest objects are in back
        for(int i = orderedMoney.Count-1; i >= 0; i--) {
            for(int j = 0; j < numDisplayed[i]; j++) {
                place(orderedMoneyPrefabs[i]);
            }
        }
    }

    //Adds some stacks without redrawing everything.  Useful if you know the 
    //new stacks will be in front already.
    private void addToDisplay(GameObject prefab, int numToAdd) {
        for(int i = 0; i < numToAdd; i++) {
            place(prefab);
        }
    }

    private void place(GameObject prefab) {
        //Debug.Log("Drawing money prefab: " + prefab.name);
        //this could be slow //TODO use object pool
        GameObject go = Instantiate(prefab);
        go.transform.parent = moneyParent.transform;
        float xLim = go.transform.position.x;
        float newX = Random.Range(xLim - 1, xLim);
        float yLim = go.transform.position.y;
        float newY = Random.Range(yLim - 0.25f, yLim);
        go.transform.position = new Vector3(newX, newY, 0);

        activeObjs.Add(go);
    }



}
