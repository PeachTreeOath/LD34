using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CityGrowth : MonoBehaviour
{
	/*
	public float shakeAmount = 0.1f;
	public float shakeDecreaseFactor = 1f;
	public float popTime = 1f;
	*/
	public int startingCityLevel = 1;

	private int cityLevel = 1;
	private float tileWidth = 110;
	private float tileHeight = 62;
	private int tileSize = 1;
	private int maxTileSize = 5; // 1x1, 3x3, 5x5...
	private float originXTilePos;
	private float originYTilePos;
	private int[,] tileMap;
	private float shakeTime;
	private Vector2 origPos;
	private List<GameObject> tileObjs;
	private float scaleTarget;

	void Awake()
	{
		origPos = transform.localPosition;
		tileMap = new int[maxTileSize, maxTileSize];
		tileMap [maxTileSize / 2, maxTileSize / 2] = 1;
		tileObjs = new List<GameObject> ();
		tileObjs.Add(Resources.Load ("Prefabs/Buildings/EiffelTile") as GameObject);
		tileObjs.Add(Resources.Load ("Prefabs/Buildings/TrumpTile") as GameObject);
		tileWidth = tileObjs[0].GetComponent<SpriteRenderer> ().bounds.size.x;
		tileHeight = tileWidth / 2;
		originXTilePos = transform.position.x - maxTileSize / 2 * tileWidth;
		originYTilePos = transform.position.y;

		for (int i = 1; i < startingCityLevel; i++) {
			LevelUp (true);
		}
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		if (shakeTime > 0) {
			transform.localPosition = origPos + Random.insideUnitCircle * shakeAmount;
			shakeTime -= Time.deltaTime * shakeDecreaseFactor;
		} else {
			shakeTime = 0;
			transform.localPosition = origPos;
		}
		*/
	}

	public void LevelUp (bool skipAnimation)
	{
		cityLevel++;
		if (cityLevel > Mathf.Pow (tileSize, 2)) {
			tileSize += 2;
		}

		if (!(cityLevel > Mathf.Pow (maxTileSize, 2))) {
			while (true) {
				int randX = Random.Range (-tileSize/2, (tileSize/2) +1) ;
				int randY = Random.Range (-tileSize/2, (tileSize/2) +1) ;
				int x = randX + (maxTileSize / 2);
				int y = randY + (maxTileSize / 2);
				if (tileMap [x, y] == 0) {
					tileMap [x, y] = 1;
					float imgX = originXTilePos + (x * tileWidth / 2) + (y * tileWidth / 2);
					float imgY = originYTilePos - (x * tileHeight / 2) + (y * tileHeight / 2);
					GameObject tileObj = tileObjs [Random.Range(0,tileObjs.Count)];
					GameObject newTile = (GameObject)Instantiate (tileObj, new Vector2 (imgX, imgY), Quaternion.identity);
					newTile.GetComponent<BaseTile> ().SkipAnimation ();
					newTile.transform.parent = this.gameObject.transform;
					newTile.GetComponent<SpriteRenderer> ().sortingLayerName = "Bldg" + (x + 1);
					newTile.GetComponent<SpriteRenderer> ().sortingOrder = maxTileSize - y;
					break;
				}
			}
		}

	//	shakeTime = 1f;
	}

}
