using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnterLevel : MonoBehaviour {

	public float growSpeed;
	object [] allObjs;
	List<Vector3> startScales;
	List<Vector3> endScales;
	float startTime;
    private AudioSource mainMusic;
    private AudioSource fillerMusic;
    private float timeFillerPlayed = 0;
    private float timeMainPlayed = 0;
    private bool isPlayingMain = false;
    private bool donePlayingMain = false;
    private bool isPlayingFiller = false;
    private bool donePlayingFiller = false;

	// Use this for initialization
	void Start () {
        mainMusic = GameObject.Find("IntroMusic").GetComponent<AudioSource>();
        if(mainMusic == null) {
            Debug.LogError("Failed to load main music :<");
        }
        fillerMusic = GameObject.Find("FillerMusic").GetComponent<AudioSource>();
        if(fillerMusic == null) {
            Debug.LogError("Failed to load filler music :<");
        }
        timeMainPlayed = 0;
        timeFillerPlayed = 0;
        isPlayingFiller = false;
        isPlayingMain = false;
        donePlayingMain = false;
        donePlayingFiller = false;
		startTime = Time.deltaTime;
		startScales = new List<Vector3>();
		endScales = new List<Vector3>();
		allObjs = GameObject.FindObjectsOfType(typeof(GameObject));
		for(int i = 0; i < allObjs.Length; i++)
		{
			GameObject g = (GameObject)allObjs[i];
			startScales.Add(g.transform.localScale * .01f);
			endScales.Add(g.transform.localScale);


			if(!(g.tag.Equals("Background") || g.layer == LayerMask.NameToLayer("UI")))
			{
				g.transform.localScale = g.transform.localScale * .01f;
				//Debug.Log(Time.time + " shrunk " +  g.name + " tag " + g.tag + " layer " + g.layer + " UI layer " + LayerMask.NameToLayer("UI"));
			}
		}
	}

	// Update is called once per frame
	void Update () {
		startTime += Time.deltaTime;
        doPlayMain();
        doPlayFiller();
		for(int i = 0; i < allObjs.Length; i++)
		{
			GameObject g = (GameObject) allObjs[i];
			if(!(g.tag.Equals("Background") || g.layer == LayerMask.NameToLayer("UI")))
			{
				//Debug.Log(Time.time + " growing");
				g.transform.localScale = Vector3.Lerp(startScales[i], endScales[i], startTime * growSpeed);
			}
		}

		if(Mathf.Abs(Vector3.Distance(endScales[0], Camera.main.gameObject.transform.localScale)) < .01f)
		{
			for(int i = 0; i < allObjs.Length; i++)
			{
				GameObject g = (GameObject) allObjs[i];
				if(!(g.tag.Equals("Background") || g.layer == LayerMask.NameToLayer("UI")))
				{
					g.transform.localScale = endScales[i];
				}
			}
			Destroy(this);
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
                isPlayingMain = true;
                mainMusic.PlayOneShot(mainMusic.clip, 1);
            } else {
                //not playing and done...nothing to do
            }
        }
    }

    private void doPlayFiller() {
        if(isPlayingMain) {
            return;
        }
        if(isPlayingFiller) {
            timeFillerPlayed += Time.deltaTime;
            if(timeFillerPlayed >= fillerMusic.clip.length) {
                isPlayingFiller = false;
                donePlayingFiller = true;
            }
        } else {
            //not playing...are we done or do we need to start?
            if (!donePlayingFiller) {
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
