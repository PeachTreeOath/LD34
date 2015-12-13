using UnityEngine;
using System.Collections;

public class CaravanState : MonoBehaviour {

	public Globals.GoodTypeEnum goodType = Globals.GoodTypeEnum.CORN;
    public float baseValue = 0;
	public float value = 5;
	public float multiplier = 1;
    public float dollarsPerDistance = 100;

    private Vector3 prevPos;
    private TextMesh bonusText;

    // Use this for initialization
    void Start () {
        //TODO move to a separate tracking object that won't inherit rotation
        GameObject label = Utility.CreateTextObject(Resources.Load<Font>("Fonts/calibri"), Resources.Load<Material>("Fonts/calibri_mat"));
        label.transform.parent = gameObject.transform;
        label.transform.position = Vector3.zero;
        label.transform.localScale *= .05f;
        label.transform.localPosition = Vector3.zero;

        bonusText = label.GetComponent<TextMesh>();
        bonusText.anchor = TextAnchor.LowerCenter;
        bonusText.text = "+$0";

        prevPos = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 currPos = gameObject.transform.position;
        float distance = (currPos - prevPos).magnitude;
        baseValue += distance;

        value = multiplier * dollarsPerDistance * baseValue;

        string text = multiplier > 1 ? "x" + multiplier + " " : "";
        text += "+$" + Mathf.FloorToInt(value);
        bonusText.text = text;

        prevPos = currPos;
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Multiplier"))
		{
			Debug.Log(Time.time + " caravan hit mult " + col.gameObject.GetComponent<Multiplier>().multiplier + " was " + multiplier);
			multiplier += col.gameObject.GetComponent<Multiplier>().multiplier - 1;
            Destroy(col.gameObject);
		}
	}
}
