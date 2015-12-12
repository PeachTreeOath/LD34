using UnityEngine;
using System.Collections;

public class DragCamera : MonoBehaviour {

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private bool mousedown;
    private Vector3 startDragPos;
    private Vector3 startCamPos;

    // Use this for initialization
    void Start() {
        mousedown = false;
    }

    // Update is called once per frame
    void Update() {
        ProcessInput();
    }

    void LateUpdate()
    {
        ClampToBounds();
    }

    void ProcessInput()
    {
        Vector3 pos = Input.mousePosition;
        if (Input.GetMouseButton(0))
        {
            if (!mousedown)
            {
                mousedown = true;
                OnMouseDown(pos);
            }
            else
            {
                OnDrag(pos);
            }
        }
        else
        {
            mousedown = false;
        }
    }

    void ClampToBounds()
    {
        float halfheight = Camera.main.orthographicSize;
        float halfwidth = halfheight * Screen.width / Screen.height;

        Vector3 pos = Camera.main.transform.position;
        pos.x = Mathf.Clamp(pos.x, minX + halfwidth, maxX - halfheight);
        pos.y = Mathf.Clamp(pos.y, minY + halfheight, maxY - halfheight);
        Camera.main.transform.position = pos;
    }

    void OnMouseDown(Vector3 pos)
    {
        startDragPos = Camera.main.ScreenToWorldPoint(pos);
        startCamPos = Camera.main.transform.position;
    }

    void OnDrag(Vector3 pos)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(pos);
        Vector3 delta = newPos - startDragPos;

        Camera.main.transform.position = startCamPos + delta;
    }
}
