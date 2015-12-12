using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {

    enum PState { WAITING, MOVING };
    private PState curState = PState.WAITING;
    private float curXDir = 1;
    private float curYDir = 0;
    private float timeInCurState = 0;


	// Use this for initialization
	void Start () {
        Collider2D col = gameObject.GetComponent<Collider2D>();
        if(col == null) {
            Debug.LogError("Person needs a Collider2D: " + gameObject.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
        doMove();	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.name == "CaravanPrefab(Clone)") {
            Debug.Log("Person hit by chariot");
			Destroy (gameObject);
		} else {
            Debug.Log("Person hit by: " + col.gameObject.name);
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
    }

    //activity during a move state tick
    private void updateMove() {
        float scale = 0.3f * Time.deltaTime; //scale to frame-rate for smoother movement
        Vector3 deltaPos = new Vector3(scale * curXDir, scale * curYDir, 0);
        gameObject.transform.position += deltaPos; 
    }
}
