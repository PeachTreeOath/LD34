using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : ScriptableObject {

    private ResourceManager resourceMgr = ScriptableObject.CreateInstance<ResourceManager>();

    public int maxProdRate;
	public float money;
	public float population;
	public float moneyGoal;
	public int cityProgress;
	public bool win = false;

    public void addResource(GResource res) {
        resourceMgr.addResource(res);
    }

    //Access the resource data for the model
    public GResource getResource(Globals.GoodTypeEnum type) {
        return resourceMgr.getResource(type);
    }

    public GResource getResource(string resName) {
        return resourceMgr.getResource(resName);
    }
}
