using UnityEngine;
using System.Collections;

public class ScreenBorderBuilder : MonoBehaviour {

	Camera mainCam;
	GameObject northWall;
	GameObject southWall;
	GameObject westWall;
	GameObject eastWall;

	// Use this for initialization
	void Start () {
		mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();

		northWall = new GameObject("northWall");
		BoxCollider2D bCol = northWall.AddComponent<BoxCollider2D>();
		bCol.sharedMaterial = Resources.Load("Materials/BouncyMat") as PhysicsMaterial2D;
		SetColliderWidth(ref bCol, Screen.width, mainCam);
		Vector3 dims = GetColliderDims(bCol, mainCam);
		northWall.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height + dims.y/2, 10));

		southWall = new GameObject("southWall");
		bCol = southWall.AddComponent<BoxCollider2D>();
		bCol.sharedMaterial = Resources.Load("Materials/BouncyMat") as PhysicsMaterial2D;
		SetColliderWidth(ref bCol, Screen.width, mainCam);
		southWall.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width/2, -dims.y/2, 10));

		westWall = new GameObject("westWall");
		bCol = westWall.AddComponent<BoxCollider2D>();
		bCol.sharedMaterial = Resources.Load("Materials/BouncyMat") as PhysicsMaterial2D;
		SetColliderHeight(ref bCol, Screen.height, mainCam);
		dims = GetColliderDims(bCol, mainCam);
		westWall.transform.position = mainCam.ScreenToWorldPoint(new Vector3(-dims.x/2, Screen.height/2, 10));

		eastWall = new GameObject("eastWall");
		bCol = eastWall.AddComponent<BoxCollider2D>();
		bCol.sharedMaterial = Resources.Load("Materials/BouncyMat") as PhysicsMaterial2D;
		SetColliderHeight(ref bCol, Screen.height, mainCam);
		eastWall.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width + dims.x/2, Screen.height/2, 10));
	}
	
	// Update is called once per frame
	void Update () {
		BoxCollider2D bCol = northWall.GetComponent<BoxCollider2D>();
		Vector3 dims = GetColliderDims(bCol, mainCam);
		northWall.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height + dims.y/2, 10));
		southWall.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width/2, -dims.y/2, 10));

		bCol = westWall.GetComponent<BoxCollider2D>();
		dims = GetColliderDims(bCol, mainCam);
		westWall.transform.position = mainCam.ScreenToWorldPoint(new Vector3(-dims.x/2, Screen.height/2, 10));
		eastWall.transform.position = mainCam.ScreenToWorldPoint(new Vector3(Screen.width + dims.x/2, Screen.height/2, 10));
	}

	Vector3 GetColliderDims(BoxCollider2D bCol, Camera cam)
	{
		GameObject go = bCol.gameObject;
		float xLeft = go.transform.position.x - bCol.bounds.extents.x;
		float yBottom = go.transform.position.y - bCol.bounds.extents.y;
		float xRight = go.transform.position.x + bCol.bounds.extents.x;
		float yTop = go.transform.position.y + bCol.bounds.extents.y;

		Vector3 p1 = new Vector3(xLeft, yBottom, go.transform.position.z);
		Vector3 p2 = new Vector3(xRight, yTop, go.transform.position.z);
		p1 = cam.WorldToScreenPoint(p1);
		p2 = cam.WorldToScreenPoint(p2);

		return new Vector3(p2.x - p1.x, p2.y - p1.y, 0);
	}

	void SetColliderWidth(ref BoxCollider2D bCol, float tarWidth, Camera cam)
	{
		float width = 0;
		GameObject go = bCol.gameObject;
		while(width < tarWidth)
		{
			go.transform.localScale = new Vector3(go.transform.localScale.x * 1.01f, go.transform.localScale.y, go.transform.localScale.z);
			float xLeft = go.transform.position.x - bCol.bounds.extents.x;
			float yBottom = go.transform.position.y - bCol.bounds.extents.y;
			float xRight = go.transform.position.x + bCol.bounds.extents.x;
			float yTop = go.transform.position.y + bCol.bounds.extents.y;

			Vector3 p1 = new Vector3(xLeft, yBottom, go.transform.position.z);
			Vector3 p2 = new Vector3(xRight, yTop, go.transform.position.z);
			p1 = cam.WorldToScreenPoint(p1);
			p2 = cam.WorldToScreenPoint(p2);

			width = p2.x - p1.x;
		}
	}

	void SetColliderHeight(ref BoxCollider2D bCol, float tarHeight, Camera cam)
	{
		float height = 0;
		GameObject go = bCol.gameObject;
		while(height < tarHeight)
		{
			go.transform.localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.y * 1.01f, go.transform.localScale.z);
			float xLeft = go.transform.position.x - bCol.bounds.extents.x;
			float yBottom = go.transform.position.y - bCol.bounds.extents.y;
			float xRight = go.transform.position.x + bCol.bounds.extents.x;
			float yTop = go.transform.position.y + bCol.bounds.extents.y;

			Vector3 p1 = new Vector3(xLeft, yBottom, go.transform.position.z);
			Vector3 p2 = new Vector3(xRight, yTop, go.transform.position.z);
			p1 = cam.WorldToScreenPoint(p1);
			p2 = cam.WorldToScreenPoint(p2);

			height = p2.y - p1.y;
		}
	}
}
