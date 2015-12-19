using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReflectionCallList : MonoBehaviour {

    public List<ReflectionCall> calls;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void doAllCalls() {
        if(calls == null || calls.Count == 0) {
            Debug.Log("No reflection calls added");
            return;
        }
        foreach(ReflectionCall rc in calls) {
            rc.doCall();
        }
    }
}
