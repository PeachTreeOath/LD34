using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour {

    public float dragSpeed = 1;

    private bool mousedown;
    private Vector3 startDragPos;

    // Use this for initialization
    void Start() {
        mousedown = false;
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            if (!mousedown)
            {
                mousedown = true;
                OnMouseDown(pos);
            }
            else
                OnDrag(pos);
        }
        else
            mousedown = false;
    }

    void OnMouseDown(Vector3 pos)
    {
        startDragPos = pos;
    }

    void OnDrag(Vector3 pos)
    {
        Vector3 newPos = Camera.main.ScreenToViewportPoint(startDragPos - pos);
        Vector3 delta = new Vector3(newPos.x * dragSpeed, newPos.y * dragSpeed, 0);

        Camera.main.transform.Translate(delta, Space.World);
    }
}
