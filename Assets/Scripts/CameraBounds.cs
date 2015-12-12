using UnityEngine;
using System.Collections;

public class CameraBounds : MonoBehaviour {

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

	// Update is called once per frame
	void LateUpdate () {
        float halfheight = Camera.main.orthographicSize;
        float halfwidth = halfheight * Screen.width / Screen.height;

        Vector3 pos = Camera.main.transform.position;
        pos.x = Mathf.Clamp(pos.x, minX + halfwidth, maxX - halfwidth);
        pos.y = Mathf.Clamp(pos.y, minY + halfheight, maxY - halfheight);
        Camera.main.transform.position = pos;
    }
}
