using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobalInputHandler : MonoBehaviour {

	public static RaycastHit2D[] hitInfos;
	public static bool hitSomething;
    public float dragThresh = 0.25f; //time before drag happens when holding click in seconds
    private bool but0Down = false;
    private float time0Down = 0;
    enum DragState {PRECHECK, DRAGGING_OBJ, DRAGGING_GLOBAL, DRAGGING_NOTHING };
    private DragState dragging = DragState.PRECHECK;
    private GameObject draggingObject = null;

    private Dictionary<int, Func<Vector3,bool>> clickDic = new Dictionary<int, Func<Vector3,bool>>();
    private Dictionary<int, Func<Vector3,bool>> dragDic = new Dictionary<int, Func<Vector3,bool>>();
    private Func<Vector3, bool> globalDragHandler = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        handlButZero();
	}

    //When there is a click and the given game object is the receiver, clickCallback will be called on that gameobject
    //the parameter for the function is mouse location of the initial click, the return value is a boolean: true if the click was handled, false to pass through
    public void registerForClick(GameObject go, Func<Vector3, bool> clickCallback) {
        clickDic.Add(go.GetInstanceID(), clickCallback);
    }

    //When there is a drag and the given game object is the receiver, clickCallback will be called on that gameobject
    //the parameter for the function is current mouse location, the return value is a boolean: true if the drag was handled, false to pass through
    public void registerForDrag(GameObject go, Func<Vector3,bool> dragCallback) {
        dragDic.Add(go.GetInstanceID(), dragCallback);
    }

    public void setGlobalDragHandler(Func<Vector3,bool> dragCallback) {
        globalDragHandler = dragCallback;
    }

    public void removeGlobalDragHandler() {
        globalDragHandler = null;
    }

    //no longer receive callbacks for the gameobject
    public void cancelClickReg(GameObject go) {
        clickDic.Remove(go.GetInstanceID());
    }

    //no longer receive callbacks for the gameobject
    public void cancelDragReg(GameObject go) {
        dragDic.Remove(go.GetInstanceID());
    }

    //handles anything with mouse button 0
    private void handlButZero() {
        bool down = Input.GetMouseButtonDown(0);
        bool up = Input.GetMouseButtonUp(0);
        Vector2 inputP = Input.mousePosition;
        if(but0Down) {
            //button was down prior to this frame
            time0Down += Time.deltaTime;
            if(up) {
                but0Down = false;
                dragging = DragState.PRECHECK;
                draggingObject = null;
            } else {
                if(time0Down >= dragThresh) {
            	    startDrag(inputP);
            	}
            }
        }else if(down) {
            //fresh click
            but0Down = true;
            singleClick(inputP);
        }	
    }

    private void startDrag(Vector2 mousePos) {
        switch(dragging) {
            case DragState.PRECHECK:
                doRaycast(mousePos);
        		if(hitSomething) {
                    //find first obj hit that will handle the drag
                    //this will also fire the first call
        		    foreach(RaycastHit2D rh in hitInfos) {
        		        Func<Vector3, bool> f = dragDic[rh.transform.gameObject.GetInstanceID()];
        		        if(f != null) {
        		            if(f(mousePos)) {
            	                dragging = DragState.DRAGGING_OBJ;
            	                draggingObject = rh.transform.gameObject;
        		                break; //handler found
        		            }
        		        }
        		    }
            	    if(globalDrag(mousePos)) { //no objects handled the drag call, default to global
                        dragging = DragState.DRAGGING_GLOBAL;  
            	    } else {
                        dragging = DragState.DRAGGING_NOTHING;
                    }
                }
                break;

            case DragState.DRAGGING_OBJ:
                updateDrag(mousePos);
                break;

            case DragState.DRAGGING_GLOBAL:
                globalDrag(mousePos);
                break;

            case DragState.DRAGGING_NOTHING:
                //nothing to do
                break;

            default:
                Debug.LogError("Invalid drag state!");
                break;
        }

    }

    private void updateDrag(Vector2 curMousePos) {
            dragDic[draggingObject.GetInstanceID()](curMousePos);
    }

    private bool globalDrag(Vector2 mousePos) {
        if(globalDragHandler != null) {
            return globalDragHandler(mousePos);
        }
        return false;
    }

    void singleClick(Vector2 inputPos) {
        doRaycast(inputPos);
        if(hitSomething) {
            foreach(RaycastHit2D rh in hitInfos) {
                Func<Vector3, bool> f = clickDic[rh.transform.gameObject.GetInstanceID()];
                if(f != null) {
                    if(f(inputPos)) {
                        break; //handler found
                    }
                }
            }
        }
    }



    private GameObject getOnTopObj(Vector2 screenPos) {
        doRaycast(Camera.main.ScreenToWorldPoint(screenPos));
        GameObject go = null;
        if(hitSomething) {
            go = hitInfos[0].transform.gameObject;
        }
        return go;
    }

    private void doRaycast(Vector2 pos) {
		hitSomething = false;
        hitInfos = Physics2D.RaycastAll(pos, -Vector2.up);
        if(hitInfos != null && hitInfos.Length!= 0) {        
			hitSomething = true;
		}
    }
}
