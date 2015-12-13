using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

    private bool dragging;
    private bool active;
    private GameObject arrow; 

    public GameObject arrowPrefab;
    public GameObject caravanPrefab;
    public float shotSpeed = 20;
    private GlobalInputHandler GIH;

    // Use this for initialization
    void Start() {
        GIH = GameObject.Find("GlobalInputHandler").GetComponent<GlobalInputHandler>();
        active = false;
        GIH.registerForDrag(gameObject, onDragStart, onDrag, onDragEnd);
    }

    bool onDragStart(Vector3 mousePos)
    {
        //Debug.Log("Drag start slingshot");
        arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity) as GameObject;
        return true;
    }

    bool onDragEnd(Vector3 mousePos)
    {
        float angle = getAngle(mousePos);
        Shoot(angle);

        Destroy(arrow);
        arrow = null;
        return true;
    }

    bool onDrag(Vector3 mousePos)
    {
        float angle = getAngle(mousePos);
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        return true;
    }

    
	
	// Update is called once per frame
	void Update () {
	}

    private float getAngle(Vector3 mousePos)
    {
        Vector3 delta = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        Vector3 dir = -delta.normalized;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private void Shoot(float angle)
    {
        GameObject caravan = (GameObject)Instantiate(caravanPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
		caravan.GetComponent<CaravanState>().goodType = gameObject.GetComponent<TradeGood>().goodType;
        Rigidbody2D rigidBody = caravan.GetComponent<Rigidbody2D>();
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		Vector3 shootDir = rotation * new Vector3(shotSpeed, 0.0f, 0.0f);
		rigidBody.AddForce(new Vector2(shootDir.x, shootDir.y), ForceMode2D.Impulse);

		Globals.gameState.productionCounts[(int)gameObject.GetComponent<TradeGood>().goodType]--;
		if(Globals.gameState.productionCounts[(int)gameObject.GetComponent<TradeGood>().goodType] == 0)
		{
        	Destroy(gameObject);
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

    void OnDestroy() {
        //Debug.Log("Doing destroy on slingshot");
        GIH.cancelDragReg(gameObject);
    }
}
