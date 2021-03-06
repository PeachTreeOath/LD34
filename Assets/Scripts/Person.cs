using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

	public float moveSpeed = .3f;
	private AudioSource hitSound;
	enum PState { WAITING, MOVING };
	private PState curState = PState.WAITING;
	private float curXDir = 1;
	private float curYDir = 0;
	private float timeInCurState = 0;
	private float xScale; //positive is right facing, negative is left facing
	bool goToCity;
	GlobalInputHandler GIH;
	GameObject [] goodSpawners;

	bool onClick(Vector3 pos) {
		//Debug.Log(Time.time + " clicked person");
		goToCity = true;
		return true;
	}

	// Use this for initialization
	void Start () {
		goodSpawners = GameObject.FindGameObjectsWithTag("TradeGoodSpawner");
		GIH = GameObject.Find("GlobalInputHandler").GetComponent<GlobalInputHandler>();
		GIH.registerForClick(gameObject, onClick);
		goToCity = false;
		Collider2D col = gameObject.GetComponent<Collider2D>();
		if(col == null) {
			Debug.LogError("Person needs a Collider2D: " + gameObject.name);
		}
		hitSound = gameObject.GetComponent<AudioSource>();
		if(hitSound == null) {
			Debug.LogError("No sound on Person!");
		}
		Mathf.Abs(xScale = gameObject.transform.localScale.x);
	}

	// Update is called once per frame
	void Update () {
		if(Utility.hitSomething &&
			Utility.hitInfo.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
		{
			//goToCity = true;
		}
		if(!goToCity)
		{
			doMove();
		}else
		{
			moveToCity();
		}
	}

	void moveToCity()
	{
		Vector3 moveDir = Globals.playerCity.transform.position - gameObject.transform.position;
		moveDir.Normalize();
		gameObject.transform.position += moveDir * moveSpeed * 5 * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "CaravanPrefab(Clone)") {
			AudioSource.PlayClipAtPoint(hitSound.clip, transform.position);
			//Debug.Log("Person hit by chariot");
			Destroy (gameObject);
		} else if(goToCity && col.gameObject.tag.Equals("Player")) {
			Globals.gameState.population += 1;
			Destroy(gameObject);
			for(int k = 0; k < goodSpawners.Length; k++)
			{
				goodSpawners[k].GetComponent<ActualGoodsSpawner>().UpdateSpawnTimer();
			}
		}else{
			//Debug.Log("Person hit by: " + col.gameObject.name);
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if(goToCity && col.gameObject.tag.Equals("Player")) {
			Globals.gameState.population += 1;
			Destroy(gameObject);
		}
	}

	//handles movement state & transitions
	private void doMove() {
		timeInCurState += Time.deltaTime;
		switch(curState) {
		case PState.MOVING:
			if(timeInCurState > getMoveTime()) {
				startWaitState();
			} else {
				updateMove();
			}
			break;

		case PState.WAITING:
			if(timeInCurState > getWaitTime()) {
				startMoveState();
			} else {
				updateWait();
			}
			break;

		default:
			Debug.LogError("State not handled: " + curState.ToString());
			break;
		}

	}

	//gets the limit of how long the move state should last
	private float getMoveTime() {
		return Random.Range(2, 6); //TODO better AI
	}

	//gets the limit of how long the wait state should last
	private float getWaitTime() {
		return Random.Range(1, 5); //TODO better AI
	}

	//transition to wait state
	private void startWaitState() {
		curState = PState.WAITING;
		timeInCurState = 0;
	}

	//activity per tick in the wait state
	private void updateWait() {
		//nothing to do yet
	}

	//transition to the move state
	private void startMoveState() {
		curState = PState.MOVING;
		timeInCurState = 0;
		curXDir = Random.Range(-1, 1);
		curYDir = Random.Range(-1, 1);
		Vector3 ls = gameObject.transform.localScale;
		if(curXDir < 0) {
			gameObject.transform.localScale = new Vector3(-xScale, ls.y, ls.z);
		} else {
			gameObject.transform.localScale = new Vector3(xScale, ls.y, ls.z);
		}
	}

	//activity during a move state tick
	private void updateMove() {
		float scale = moveSpeed * Time.deltaTime; //scale to frame-rate for smoother movement
		Vector3 deltaPos = new Vector3(scale * curXDir, scale * curYDir, 0);
		gameObject.transform.position += deltaPos; 
	}
}