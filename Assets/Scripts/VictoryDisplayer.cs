using UnityEngine;
using System.Collections;

public class VictoryDisplayer : MonoBehaviour {

	int curIdx;
	string endText = "The End?";
	public float moveSpeed;
	float tarY;
	GameObject curLetter;
	Font font;
	Material fontMat;
	float startX;

	// Use this for initialization
	void Start () {
		moveSpeed = 1;
		Vector3 tarPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width*.3f, Screen.height * .4f, 10));
		tarY = tarPos.y;
		startX = tarPos.x;
		curIdx = 0;
		font = Resources.Load("Fonts/calibri") as Font;
		fontMat = Resources.Load("Fonts/calibri_mat") as Material;
		curLetter = null;
	}
	
	// Update is called once per frame
	void Update () {
		TrySpawnLetter();
		UpdateLetterPos();
	}

	void TrySpawnLetter()
	{
		if(curLetter == null)
		{
			Debug.Log(Time.time + " spawn letter");
			curLetter = Utility.CreateTextObject(font, fontMat);
			curLetter.GetComponent<TextMesh>().text = endText[curIdx].ToString();
			curLetter.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
			curLetter.transform.localScale *= .25f;
			curLetter.name = "Victory " + endText[curIdx].ToString();
			Vector3 dims = Utility.GetScreenDims(curLetter, Camera.main);
			curLetter.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height + dims.y/2, 10));
			curLetter.transform.position = new Vector3(startX, curLetter.transform.position.y, curLetter.transform.position.z);
			startX += curLetter.GetComponent<Renderer>().bounds.size.x;
			moveSpeed = 1;
			if(endText[curIdx]== ' ')
			{
				moveSpeed = 10;
			}else if(curIdx == endText.Length - 1)
			{
				moveSpeed = .2f;
			}
		}
	}

	void UpdateLetterPos()
	{
		curLetter.transform.position += Vector3.down * Time.deltaTime * moveSpeed;
		if(curLetter.transform.position.y <= tarY)
		{
			curLetter.transform.position = new Vector3(curLetter.transform.position.x, tarY, curLetter.transform.position.z);
			curLetter = null;
			curIdx++;

			if(curIdx == endText.Length)
			{
				Destroy(this);
			}
		}
	}
}
