using UnityEngine;
using System.Collections;

//Assigns scene stuff to a canvas, needed when levels change and existing connections drop
public class CanvasLevelLoader : MonoBehaviour {

    public string cameraName;
    public GameObject canvas;

    void OnLevelWasLoaded(int level) {
        Canvas c = canvas.GetComponent<Canvas>();
        if(c.worldCamera == null) {
            c.worldCamera = GameObject.Find(cameraName).GetComponent<Camera>();
        	Debug.Log("Canvas acquired new camera");
        } else {
            Debug.Log("Canvas retains old camera");
        }

    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
