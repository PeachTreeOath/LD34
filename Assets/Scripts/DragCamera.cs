using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour {

    public float dragSpeed = 1;

    private bool mousedown;
    private Vector3 startDragPos = Vector3.zero;

    private GlobalInputHandler GIH;

    void Awake() {
       GIH = GameObject.Find("GlobalInputHandler").GetComponent<GlobalInputHandler>();
    }

    // Use this for initialization
    void Start() {
        mousedown = false;
        GIH.setGlobalDragHandler(OnDragStart, OnDrag, OnDragEnd);
    }

    // Update is called once per frame
    void Update() {
    }

    bool OnDragStart(Vector3 pos) {
        startDragPos = pos;
        return true;
    }

    bool OnDragEnd(Vector3 pos) {
        startDragPos = Vector3.zero;
        return true;
    }

    bool OnDrag(Vector3 pos) {
        Vector3 newPos = Camera.main.ScreenToViewportPoint(startDragPos - pos);
        Vector3 delta = new Vector3(newPos.x * dragSpeed, newPos.y * dragSpeed, 0);

        Camera.main.transform.Translate(delta, Space.World);
        return true;
    }
}
