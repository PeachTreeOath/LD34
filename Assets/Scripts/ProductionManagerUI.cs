using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ProductionManagerUI : MonoBehaviour {

	public List<StatBar> sliders; //The order the sliders are added should match the actual layout otherwise the indices will be wrong
    private GlobalInputHandler GIH;

	// Use this for initialization
	void Start () {
        if(sliders == null) {
            Debug.LogError("No stat bars added in editor");
        } else {
            foreach (StatBar s in sliders) {
                if (s == null) Debug.LogError("Unset StatBar in editor");
            }
        }
        GIH = GameObject.Find("GlobalInputHandler").GetComponent<GlobalInputHandler>();
	}

	// Update is called once per frame
	void Update () {
		for(int i = 0; i < sliders.Count; i++)
		{
            GResource r = Globals.gameState.getResource(sliders[i].statName);
            if(r == null) {
                Debug.Log("No resource for " + sliders[i].statName);
                continue;
            }
			if(r.prodRate != sliders[i].getNumTicks())
			{
                int sum = getCurrentProdRate();
                int max = Globals.gameState.maxProdRate;
                if(sum > max) {
                    //production for all goods exceeded max; reduce others by 1, round robin
                    int delta = sum - max;
                    doReduce(i, delta);
                }
			}
		}

		for(int i = 0; i < sliders.Count; i++)
		{
			Globals.gameState.getResource(sliders[i].statName).prodRate = sliders[i].getNumTicks();
		}
	}

    //Get the total prod rate displayed by the UI
    private int getCurrentProdRate() {
		int sum = 0;
		for(int j = 0; j < sliders.Count; j++)
		{
            sum += sliders[j].getNumTicks();
		}
        return sum;
    }

    private void doReduce(int workingIdx, int reduceBy) {
        if(sliders.Count <= 1) {
            Debug.Log("Tried to reduce a single counter that exceeded max"); //this means your counter is probably misconfigured
            return;
        }
        //round robin reduce by one, excluding the working index
        int idx = workingIdx + 1;
        //Debug.Log("Working idx=" + workingIdx);
        while(reduceBy > 0) {
            if(idx == workingIdx) {
                idx++;
            }
            idx = idx % sliders.Count;
            if(sliders[idx].getNumTicks() > 0) {
                //Debug.Log("Reducing idx=" + idx);
                sliders[idx].dec();
            	reduceBy--;
            } else {
                //Debug.Log("Skipping empty idx=" + idx);
            }
            idx++;
        }
            
    }

}
