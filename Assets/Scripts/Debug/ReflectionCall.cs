using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

public class ReflectionCall : MonoBehaviour {

    public string gameObjectName = "";
    public string scriptName = "";
    public string functionName = "";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void doCall() {
        GameObject go = GameObject.Find(gameObjectName);
        if(go == null) {
            showError("Null GameObject");
            return;
        }
        MonoBehaviour script = go.GetComponent(scriptName) as MonoBehaviour;
//        foreach(MonoBehaviour c in go.GetComponentsInChildren<MonoBehaviour>(true)) {
//            if(c.name == scriptName) {
//                script = c;
//                break;
//            }
//        }
        if(script == null) {
            showError("Null script");
            return;
        }
        Type thisType = script.GetType();
        MethodInfo theMethod = thisType.GetMethod(functionName);
        theMethod.Invoke(script, null);
    }

    private void showError(string msg) {
        Debug.LogError("Couldn't do reflection invoke: " + msg + " for '" + gameObjectName + "." + scriptName + "." + functionName + "'");
    }
}
