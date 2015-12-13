using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour {

    public float dragSpeed = 1;

    private Vector3 startDragPos = Vector3.zero;
    private Vector3 startCamPos = Vector3.zero;

    private GlobalInputHandler GIH;

    void Awake() {
       GIH = GameObject.Find("GlobalInputHandler").GetComponent<GlobalInputHandler>();
    }

    // Use this for initialization
    void Start() {
        GIH.setGlobalDragHandler(OnDragStart, OnDrag, OnDragEnd);
    }

    // Update is called once per frame
    void Update() {
    }

    bool OnDragStart(Vector3 pos) {
        startDragPos = pos; 
        startCamPos = Camera.main.transform.position;
        return true;
    }

    bool OnDragEnd(Vector3 pos) {
        startDragPos = Vector3.zero;
        startCamPos = Vector3.zero;
        return true;
    }

    bool OnDrag(Vector3 pos) {
        Vector3 posDelta = Camera.main.ScreenToWorldPoint(startDragPos) - Camera.main.ScreenToWorldPoint(pos);
        posDelta.x *= -1;
        posDelta.y *= -1;
        if(posDelta.sqrMagnitude > 0.1) {
            Vector3 camNew = startCamPos - posDelta;

        	//Camera.main.transform.Translate(delta, Space.World);
        	Camera.main.transform.position = camNew;
        }
        return true;
    }
}
