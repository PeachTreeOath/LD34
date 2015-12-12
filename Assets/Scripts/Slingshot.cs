using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

    private bool dragging;
    private bool active;
    private GameObject arrow; 

    public GameObject arrowPrefab;

    // Use this for initialization
    void Start() {
        dragging = false;
        active = false;
    }

    void onDragStart(Vector3 mousePos)
    {
        arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity) as GameObject;
    }

    void onDragEnd(Vector3 mousePos)
    {
        Destroy(arrow);
        arrow = null;
    }

    void onDrag(Vector3 mousePos)
    {
        Vector3 delta = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        Vector3 dir = -delta.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Input.mousePosition;
        bool hit = hitThis();

        if (Input.GetMouseButton(0))
        {
            if (!dragging)
            {
                dragging = true;
                if (hit)
                {
                    active = true;
                    onDragStart(mousePos);
                }
            }
            else if (active)
                onDrag(mousePos);
        }
        else {
            dragging = false;
            if (active)
            {
                active = false;
                onDragEnd(mousePos);
            }
        }
	}

    private bool hitThis()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
        foreach(Collider2D collider in colliders)
        {
            if (collider.gameObject.GetInstanceID() == gameObject.GetInstanceID()) return true;
        }

        return false;
    }
}
