using UnityEngine;
using System.Collections;

public class CameraZoomer : MonoBehaviour {

    private float minSize;

    public float maxSize;
    public float zoomSpeed = 1f;

	// Use this for initialization
	void Start () {
        minSize = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
        float currSize = Camera.main.orthographicSize;
        float delta = Input.GetAxis("Mouse ScrollWheel");

        Debug.Log("delta: " + delta);

        float newSize = Mathf.Clamp(currSize + delta * -zoomSpeed, minSize, maxSize);

        Camera.main.orthographicSize = newSize;
	}
}
