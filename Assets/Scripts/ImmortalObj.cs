﻿using UnityEngine;
using System.Collections;

public class ImmortalObj : MonoBehaviour {

     private static ImmortalObj instance = null;
	 public static ImmortalObj Instance {
	     get { return instance; }
	 }

    public bool childrenAreImmortal = true;

     void Awake() {
         if (instance != null && instance != this) {
            stop();
	        Destroy(this.gameObject);
	        return;
	     } else {
	         instance = this;
	     }
	     DontDestroyOnLoad(this.gameObject);
    }

    bool isMute = false;
    private GameObject muteBut;

    private float timeFillerPlayed = 0;
    private float timeMainPlayed = 0;
    private bool isPlayingMain = false;
    private bool donePlayingMain = false;
    private bool isPlayingFiller = false;
    private bool donePlayingFiller = false;

    private AudioSource mainMusic;
    private AudioSource fillerMusic;
    private bool running = false;

    private GlobalInputHandler GIH;

    // any other methods you need
	// Use this for initialization
	void Start () {
        if(childrenAreImmortal) {
            foreach (Component go in GetComponentsInChildren<Component>()) {
                if(go.gameObject.transform.parent == null) {
                    DontDestroyOnLoad(go);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(running) {
            doPlayMain();
            doPlayFiller();
        }	
	}

    public void doRun() {
        stop();
        restart();
        running = true;
    }

    public void stop() {
        //comment
        running = false;
        if(mainMusic != null) { mainMusic.Stop(); }
        if(fillerMusic != null) { fillerMusic.Stop(); }
    }

    public void restart() {
        GIH = GameObject.Find("GlobalInputHandler").GetComponent<GlobalInputHandler>();
        if(muteBut != null) {
            GIH.cancelClickReg(muteBut);
            muteBut = null;
        }
        muteBut = GameObject.Find("soundbut"); 
        if(isMute) {
            muteBut.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/sfx_off");
        } else {
            muteBut.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/sfx_on");
        }
        GIH.registerForClick(muteBut, doMute);
        timeMainPlayed = 0;
        timeFillerPlayed = 0;
        isPlayingFiller = false;
        isPlayingMain = false;
        donePlayingMain = false;
        donePlayingFiller = false;
        mainMusic = GameObject.Find("IntroMusic").GetComponent<AudioSource>();
        if(mainMusic == null) {
            Debug.LogError("Failed to load main music :<");
        }
        fillerMusic = GameObject.Find("FillerMusic").GetComponent<AudioSource>();
        if(fillerMusic == null) {
            Debug.LogError("Failed to load filler music :<");
        }
    }

    private void doPlayMain() {
        if(isPlayingMain) {
            timeMainPlayed += Time.deltaTime;
            if(timeMainPlayed >= mainMusic.clip.length) {
                isPlayingMain = false;
                donePlayingMain = true;
            }
        } else {
            //not playing...are we done or do we need to start?
            if (!donePlayingMain) {
                //not playing and not started...we need to start
                fillerMusic.Stop();
                timeMainPlayed = 0;
                donePlayingMain = false;
                isPlayingMain = true;
                Debug.Log("Starting main music");
                mainMusic.PlayOneShot(mainMusic.clip, 1);
//                Invoke("setDonePlayingMain", mainMusic.clip.length);
            } else {
                //not playing and done...nothing to do
            }
        }
    }

 
     public bool doMute(Vector3 pos){
        Debug.Log("Mute clicked");
        if(isMute) {
            //muted -> sounded
            AudioListener.volume = 1;
            isMute = false;
            muteBut.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/sfx_on");
        } else {
            //sounded -> muted
            AudioListener.volume = 0;
            isMute = true;
            muteBut.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/sfx_off");
        }
        return true;
	 }

    private void doPlayFiller() {
        if(isPlayingMain) {
//            Debug.Log("Main playing still");
            return;
        }
        if(isPlayingFiller) {
            timeFillerPlayed += Time.deltaTime;
            if(timeFillerPlayed >= fillerMusic.clip.length) {
                isPlayingFiller = false;
                donePlayingFiller = true;
                //poor mans loop
                fillerMusic.Stop();
                fillerMusic.PlayOneShot(fillerMusic.clip);
                timeFillerPlayed = 0;
                isPlayingFiller = true;
            }
        } else {
            //not playing...are we done or do we need to start?
            if (!donePlayingFiller) {
                Debug.Log("Starting filler");
                //not playing and not started...we need to start
                timeFillerPlayed = 0;
                isPlayingFiller = true;
                fillerMusic.PlayOneShot(fillerMusic.clip, 1);
            } else {
                //not playing and done...nothing to do
            }
        }
    }
}
