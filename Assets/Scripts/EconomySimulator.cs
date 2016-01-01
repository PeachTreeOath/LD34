using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EconomySimulator : MonoBehaviour {

    public float offscreenBuffer = 0; //offset from screen edge, bigger is more offset
	public float minTimer;
	public float maxTimer;
	GameObject [] cities;
	List<float> timers;
	List<float> times;
	List<GameObject> icons;
    List<GameObject> lines;

	// Use this for initialization
	void Start () {
        Globals.gameState.maxProdRate = 3;
        loadGameObjects();
	}

    private void loadGameObjects() {
		timers = new List<float>();
		times = new List<float>();
		icons = new List<GameObject>();
		cities = GameObject.FindGameObjectsWithTag("City");
        lines = new List<GameObject>(new GameObject[cities.Length]);
		GameObject goodIconFab = Resources.Load("Prefabs/GoodIcon") as GameObject;
		for(int i = 0; i < cities.Length; i++)
		{
			timers.Add(Time.time);
			times.Add(Random.Range(minTimer, maxTimer));
			cities[i].GetComponent<TargetCityScript>().desiredGood = (Globals.GoodTypeEnum)Random.Range(0, (int)Globals.GoodTypeEnum.LAST);
			string iconName = cities[i].GetComponent<TargetCityScript>().desiredGood.ToString().ToLower();
			iconName = iconName.Substring(0, 1).ToUpper() + iconName.Substring(1);
			GameObject icon = Instantiate(goodIconFab);
			SpriteRenderer rend = icon.GetComponent<SpriteRenderer>();
			icons.Add(icon);
			rend.sprite = Resources.Load<Sprite>("Textures/Trade Goods/"+iconName);
			Vector3 desiredPos = cities[i].transform.position + Vector3.up * ((cities[i].GetComponent<Renderer>().bounds.extents.y + rend.bounds.extents.y) * 1.15f) + Vector3.back;
            Vector3 bestPos = getOnscreenPos(desiredPos);
            if(Vector3.Distance(bestPos,desiredPos) > Mathf.Epsilon) {
                GameObject newLine = createLineTo(icon, desiredPos); //TODO
                lines[i] = newLine;
            }
            icons[i].transform.position = bestPos;
		}

    }
	
    void OnLevelWasLoaded(int level) {
        loadGameObjects();
        Debug.Log("Enconomy loaded new cities for level " + level);
    }

	// Update is called once per frame
	void Update () {
        if(cities[0] == null) {
            Debug.Log("Aborting update to load new level");
            return;
        }

		for(int i = 0; i < times.Count; i++)
		{
			if(Time.time - timers[i] > times[i])
			{

			}
			Vector3 desiredPos = cities[i].transform.position + Vector3.up * ((cities[i].GetComponent<Renderer>().bounds.extents.y + icons[i].GetComponent<Renderer>().bounds.extents.y) * 1.15f) + Vector3.back;
            Vector3 bestPos = getOnscreenPos(desiredPos);
            if(lines[i] != null) {
                Destroy(lines[i]);
            }
            if(Vector3.Distance(bestPos,desiredPos) > Mathf.Epsilon) {
                GameObject newLine = createLineTo(icons[i], desiredPos); 
                lines[i] = newLine;
            }
            icons[i].transform.position = bestPos;
		}
	}

    public void UpdateDesiredGood(GameObject gameObject)
    {
        int index = 0;
        foreach (var city in cities)
        {
            if(gameObject.GetInstanceID() == city.GetInstanceID() )
            {
                city.GetComponent<TargetCityScript>().desiredGood = (Globals.GoodTypeEnum)Random.Range(0, (int)Globals.GoodTypeEnum.LAST);
                string iconName = city.GetComponent<TargetCityScript>().desiredGood.ToString().ToLower();
                iconName = iconName.Substring(0, 1).ToUpper() + iconName.Substring(1);
                icons[index].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Trade Goods/" + iconName);
            }
            index++;
        }

    }

    //Get the closest on screen point to desiredPos
    private Vector3 getOnscreenPos(Vector3 desiredPos) {
        Vector3 tr = Globals.getTopRightScreenPointInWorldSpace();
        Vector3 bl = Globals.getBotLeftScreenPointInWorldSpace();
        float x = Mathf.Clamp(desiredPos.x, bl.x+offscreenBuffer, tr.x - offscreenBuffer);
        float y = Mathf.Clamp(desiredPos.y, bl.y+offscreenBuffer, tr.y-offscreenBuffer);
        //if(!Mathf.Equals(x, desiredPos.x)) {
        //    //add buffer
        //    if(x < 0) {
        //        x = x + offscreenBuffer;
        //    } else {
        //        x = x - offscreenBuffer;
        //    }
        //}
        //if(!Mathf.Equals(y, desiredPos.y)) {
        //    //add buffer
        //    if(y < 0) {
        //        y = y + offscreenBuffer;
        //    } else {
        //        y = y - offscreenBuffer;
        //    }
        //}

        return new Vector3(x, y, desiredPos.z);
    }

    private GameObject createLineTo(GameObject startingObj, Vector3 lineEnd) {
        GameObject go = new GameObject("line start");
        go.transform.parent = startingObj.transform;
        LineRenderer r = go.AddComponent<LineRenderer>();
        r.material = Resources.Load("Materials/lineMat", typeof(Material)) as Material;
        r.sortingLayerName = "Icons";
        r.sortingOrder = -1;
        r.SetColors(Color.black, Color.black);
        r.SetWidth(0.08F, 0.02F);
        r.SetVertexCount(2);
        r.SetPosition(0, startingObj.transform.position);
        r.SetPosition(1, lineEnd);
        return go;
    }
}
