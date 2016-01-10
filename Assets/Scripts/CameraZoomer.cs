using UnityEngine;
using System.Collections;

public class CameraZoomer : MonoBehaviour {

    public float minSize;
    public float startingSize;
    public float maxSize;
    public float zoomSpeed = 1f;

	// Use this for initialization
	void Start () {
        //minSize = Camera.main.orthographicSize;
        Camera.main.orthographicSize = startingSize;
	}
	
	// Update is called once per frame
	void Update () {
        float currSize = Camera.main.orthographicSize;
        float delta = Input.GetAxis("Mouse ScrollWheel");

        //Debug.Log("delta: " + delta);

        if(delta != 0) {
            float newSize = Mathf.Clamp(currSize + delta * -zoomSpeed, minSize, maxSize);
            Camera.main.orthographicSize = newSize;
        }
	}
}
