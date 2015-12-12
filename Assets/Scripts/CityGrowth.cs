using UnityEngine;
using System.Collections;

public class CityGrowth : MonoBehaviour
{

	public float shakeAmount = 0.1f;
	public float shakeDecreaseFactor = 1f;
	public float popTime = 1f;
	public float tileWidth = 220;
	public float tileHeight = 125;

	private int cityLevel = 1;
	private int tileSize = 1;
	// 1x1, 3x3, 5x5...
	private int maxTileSize = 5;
	private int originXTilePos;
	private int originYTilePos;
	private int[,] tileMap;
	private float shakeTime;
	private Vector2 origPos;
	private GameObject baseTileObj;

	// Use this for initialization
	void Start ()
	{
		origPos = transform.localPosition;
		tileMap = new int[maxTileSize, maxTileSize];
		baseTileObj = Resources.Load ("Prefabs/BaseTile") as GameObject;
		//originXTilePos
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (shakeTime > 0) {
			transform.localPosition = origPos + Random.insideUnitCircle * shakeAmount;
			shakeTime -= Time.deltaTime * shakeDecreaseFactor;
		} else {
			shakeTime = 0;
			transform.localPosition = origPos;
		}
	}

	public void LevelUp ()
	{/*
		cityLevel++;
		if (cityLevel > Mathf.Pow (tileSize, 2)) {
			tileSize += 2;
		}

		if (!(cityLevel > Mathf.Pow (maxTileSize, 2))) {
			while (true) {
				int x = Random.Range (0, tileSize - 1) + maxTileSize / 2;
				int y = Random.Range (0, tileSize - 1) + maxTileSize / 2;
				if (tileMap [x, y] == 0) {
					tileMap [x, y] = 1;
				//	float imgX = 
					//Instantiate(baseTileObj,new Vector2(origPos+
					//break;
				}
			}
		}

		shakeTime = 1f;*/
	}

}
