using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : ScriptableObject {

    private List<GResource> resources = new List<GResource>();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addResource(GResource r) {
        resources.Add(r);
    }

    public GResource getResource(Globals.GoodTypeEnum type) {
        foreach(GResource r in resources) {
            if (r.goodType == type) return r;
        }
        return null;
    }

    public GResource getResource(string resName) {
        foreach(GResource r in resources) {
            if (r.resName == resName) return r;
        }
        return null;
    }
}

