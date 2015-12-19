using UnityEngine;
using System.Collections;

public class ToggleAlpha : MonoBehaviour {

    public float max = 1.0f;
    public float min = 0.1f;
    private CanvasGroup myGroup;

    private bool on = true;
	// Use this for initialization
	void Start () {
        myGroup = GetComponent<CanvasGroup>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void toggleState() {
        Debug.Log("Clicked");
        if(on) {
            on = false;
            myGroup.alpha = min;
        } else {
            on = true;
            myGroup.alpha = max;
        }
    }


}
