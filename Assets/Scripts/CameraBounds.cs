using UnityEngine;
using System.Collections;

public class CameraBounds : MonoBehaviour {

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void start()
    {
        
    }

	// Update is called once per frame
	void LateUpdate () {
        float halfheight = Camera.main.orthographicSize;
        float halfwidth = halfheight * Screen.width / Screen.height;

        float mapw = maxX - minX;
        float maph = maxY - minY;

        Vector3 pos = Camera.main.transform.position;

        if (halfwidth * 2 >= mapw)
            pos.x = 0;
        else
            pos.x = Mathf.Clamp(pos.x, minX + halfwidth, maxX - halfwidth);

        if (halfheight * 2 >= maph)
            pos.y = 0;
        else
            pos.y = Mathf.Clamp(pos.y, minY + halfheight, maxY - halfheight);

        Camera.main.transform.position = pos;
    }
}
