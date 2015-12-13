using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GlobalInputHandler : MonoBehaviour {

	public static Collider2D[] hitInfos;
	public static bool hitSomething;
    public float dragThresh = 0.25f; //time before drag happens when holding click in seconds
    private bool but0Down = false;
    private float time0Down = 0;
    enum DragState {PRECHECK, DRAGGING_OBJ, DRAGGING_GLOBAL, DRAGGING_NOTHING };
    private DragState dragging = DragState.PRECHECK;
    private GameObject draggingObject = null;
    private bool UIObj = false;

    private Dictionary<int, Func<Vector3,bool>> clickDic = new Dictionary<int, Func<Vector3,bool>>();
    private Dictionary<int, Func<Vector3,bool>[]> dragDic = new Dictionary<int, Func<Vector3,bool>[]>();
    private Func<Vector3, bool>[] globalDragHandler = null;
    private Func<Vector3, bool> globalClickHandler = null;

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

    //When there is a drag and the given game object is the receiver, click[start,,end]Callback will be called on that gameobject
    //the parameter for the function is current mouse location, the return value is a boolean: true if the drag was handled, false to pass through
    public void registerForDrag(GameObject go, Func<Vector3,bool> dragStartCallback,Func<Vector3,bool> dragCallback, Func<Vector3,bool> dragEndCallback ) {
        dragDic.Add(go.GetInstanceID(), new Func<Vector3,bool>[] {dragStartCallback, dragCallback, dragEndCallback});
    }

    public void setGlobalDragHandler(Func<Vector3,bool> dragStartCallback,Func<Vector3,bool> dragCallback, Func<Vector3,bool> dragEndCallback) {
        globalDragHandler = new Func<Vector3,bool>[] {dragStartCallback, dragCallback, dragEndCallback};
    }

    public void setGlobalClickHandler(Func<Vector3,bool> clickHandler) {
        globalClickHandler = clickHandler;
    }

    public void removeGlobalDragHandler() {
        globalDragHandler = null;
    }

    public void removeGlobalClickHandler() {
        globalClickHandler = null;
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

        Vector3 inputP = Input.mousePosition;
        if(but0Down) {
            //button was down prior to this frame
            time0Down += Time.deltaTime;
            if(up) {
                endAllDrag(inputP);
                if(time0Down < dragThresh) {
                    singleClick(inputP);
                }
                time0Down = 0;
            } else {
                if(time0Down >= dragThresh) {
            	    startDrag(inputP);
            	}
            }
        }else if(down) {
            //fresh click, maybe.  If the mouse is let up before a drag starts then it is a click
            UIObj = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            but0Down = true;
        }	
    }

    private void startDrag(Vector3 mousePos) {
        switch(dragging) {
            case DragState.PRECHECK:
                //Debug.Log("Drag precheck");
                doRaycast(mousePos);
        		if(hitSomething) {
                    //find first obj hit that will handle the drag
                    //this will also fire the first call
        		    for(int i = 0; i < hitInfos.Length; i++) {
                        Func<Vector3, bool>[] f;
                        dragDic.TryGetValue(hitInfos[i].transform.gameObject.GetInstanceID(),out f);
        		        if(f != null) {
                            //Debug.Log("Somthing hit");
        		            if(f[0](mousePos)) {
            	                dragging = DragState.DRAGGING_OBJ;
            	                draggingObject = hitInfos[i].transform.gameObject;
        		                break; //handler found
        		            }
        		        }
        		    }
                } else {
            	    //no objects handled the drag call, default to global if not over UI element
                    if(!UIObj && globalDragStart(mousePos)) { //Don't engage UI elements
                        dragging = DragState.DRAGGING_GLOBAL;  
            	    } else {
                        //Debug.Log("Dragging NOTHING");
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
                //Debug.LogError("Invalid drag state!");
                break;
        }

    }

    private void updateDrag(Vector3 curMousePos) {
            dragDic[draggingObject.GetInstanceID()][1](curMousePos);
    }

    private void endDrag(Vector3 curMousePos) {
            dragDic[draggingObject.GetInstanceID()][2](curMousePos);
    }

    private bool globalDragStart(Vector3 mousePos) {
        //Debug.Log("Starting global drag");
        if(globalDragHandler != null) {
            return globalDragHandler[0](mousePos);
        }
        return false;
    }

    private bool globalDrag(Vector3 mousePos) {
        if(globalDragHandler != null) {
            return globalDragHandler[1](mousePos);
        }
        return false;
    }
    
    private bool endGlobalDrag(Vector3 mousePos) {
        if(globalDragHandler != null) {
            return globalDragHandler[2](mousePos);
        }
        return false;
    }

    private void endAllDrag(Vector3 mousePos) {
        switch(dragging) {
            case DragState.DRAGGING_GLOBAL:
                endGlobalDrag(mousePos);
                break;

            case DragState.DRAGGING_OBJ:
                endDrag(mousePos);
                break;
        }
        but0Down = false;
		dragging = DragState.PRECHECK;
		draggingObject = null;
    }


    void singleClick(Vector3 inputPos) {
        //Debug.Log("Single click");
        doRaycast(inputPos);
        bool result = false;
        if(hitSomething) {
        //Debug.Log("Single click hit something");
            if(hitInfos == null) {
                //Debug.Log("WTFFFFF");
            }
            for(int i = 0; i < hitInfos.Length; i++) {
                Func<Vector3, bool> f;
                clickDic.TryGetValue(hitInfos[i].transform.gameObject.GetInstanceID(),out f);
                if(f != null) {
                    if(f(inputPos)) {
        //Debug.Log("Single click hit something registered");
                        result = true;
                        break; //handler found
                    }
                }
            }
        }
        if(!hitSomething && !result){
        //Debug.Log("Single click hit nothing");
            if(!UIObj && globalClickHandler != null) { //Don't engage UI elements
                //Debug.Log("Doing global click");
                globalClickHandler(inputPos);
            }
        }
    }



    private GameObject getOnTopObj(Vector3 screenPos) {
        doRaycast(Camera.main.ScreenToWorldPoint(screenPos));
        GameObject go = null;
        if(hitSomething) {
            go = hitInfos[0].transform.gameObject;
        }
        return go;
    }

    private void doRaycast(Vector3 pos) {
        //Debug.Log("Raycast called");
		hitSomething = false;
        hitInfos = Physics2D.OverlapPointAll((Vector2)Camera.main.ScreenToWorldPoint(pos));
        if(hitInfos != null && hitInfos.Length!= 0) {
		    hitSomething = true;
		}
        //for(int i = 0; i < hitInfos.Length; i++) {
            //Debug.Log("Raycast hit something: " + hitInfos[i].transform.gameObject.name);
        //}
    }
}
