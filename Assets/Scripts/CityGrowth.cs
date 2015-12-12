using UnityEngine;
using System.Collections;

public class CityGrowth : MonoBehaviour
{

	public float shakeAmount = 0.1f;
	public float shakeDecreaseFactor = 1f;
	public float popTime = 1f;
	public float tileWidth;
	public float tileHeight;

	private int cityLevel = 1;
	private int tileSize = 1;
	// 1x1, 3x3, 5x5...
	private int maxTileSize = 5;
	private float originXTilePos;
	private float originYTilePos;
	private int[,] tileMap;
	private float shakeTime;
	private Vector2 origPos;
	private GameObject baseTileObj;

	// Use this for initialization
	void Start ()
	{
		origPos = transform.localPosition;
		tileMap = new int[maxTileSize, maxTileSize];
		tileMap [maxTileSize / 2, maxTileSize / 2] = 1;
		baseTileObj = Resources.Load ("Prefabs/BaseTile") as GameObject;
		tileWidth = baseTileObj.GetComponent<SpriteRenderer> ().bounds.size.x;
		tileHeight = baseTileObj.GetComponent<SpriteRenderer>().bounds.size.y;
		originXTilePos = transform.position.x - maxTileSize / 2 * tileWidth;
		originYTilePos = transform.position.y;
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
					GameObject newTile = (GameObject)Instantiate (baseTileObj, new Vector2 (imgX, imgY), Quaternion.identity);
					newTile.transform.parent = this.gameObject.transform;
					break;
				}
			}
		}

		//shakeTime = 1f;
	}

}
