using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
* Handles everything for a stat bar.  Kind of like a slider but with push buttons instead of a knob.
*/
public class StatBar : MonoBehaviour {

    public string statName;
    public int maxSize;
    public int numSections;
    public Color col;
    public Sprite icon;
    public int startingTicks = 0; //number of sections populated on start
    public GameObject bar;
    public GameObject downArrow;
    public GameObject upArrow;
    public GameObject iconImage;

    private float tickSize;
    private int curTicks;
    //inspector variables only
    private int prevMaxSize;
    private Color prevColor;
    private Sprite prevIcon;

	// Use this for initialization
	void Start () {
        if(maxSize <= 0) {
            Debug.LogError("Invalid max size");
        }
        if(numSections < 1) {
            Debug.LogError("Invalid number of sections");
        }
        if(statName == null || statName == "") {
            Debug.LogError("StatBar missing stateName");
        }
        RectTransform rt = bar.GetComponent<RectTransform>();
        float realSize = rt.sizeDelta.y; //note, y value because bar is assumed to be vertical
        tickSize =  realSize / numSections;
        curTicks = Mathf.Min(numSections, startingTicks);
        setNumTicks(curTicks);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void downClicked() {
        dec();
    }

    public void upClicked() {
        inc();
    }

    public void dec() {
        setNumTicks(curTicks - 1);
    }

    public void inc() {
        setNumTicks(curTicks + 1);
    }

    public int getNumTicks() {
        return curTicks;
    }

    public void setNumTicks(int newNumTicks) {
        curTicks = Mathf.Clamp(newNumTicks, 0, numSections);
        float newSize = curTicks * tickSize;
        //Debug.Log("curTicks=" + curTicks + ", tickSize=" + tickSize + ", newSize=" + newSize);
        setBarSize(newSize);
    }

    private void setBarSize(float newSize) {
        RectTransform rt = bar.GetComponent<RectTransform>();
	    rt.sizeDelta = new Vector2(rt.sizeDelta.x, Mathf.Max(2,newSize)); //use min size so bar doesn't disappear completely
    }

    private void setColor(Color newCol) {
        bar.GetComponent<Image>().color = newCol;
    }

    private void setIcon(Sprite newIcon) {
        iconImage.GetComponent<Image>().sprite = newIcon;
    }

    //inspector updates
    void OnValidate() {
        if(maxSize != prevMaxSize) {
            setBarSize(maxSize);
            prevMaxSize = maxSize;
        }
        if(col != prevColor) {
            setColor(col);
            prevColor = col;
        } 
        if(icon != prevIcon) {
            setIcon(icon);
            prevIcon = icon;
        }
    }

}
