using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Utility : MonoBehaviour {

	public static RaycastHit2D hitInfo;
	public static bool hitSomething;

	public static RaycastHit2D hitInfoHover;
	public static bool hitHoverEnter;
	public static bool hitHoverExit;
	public static bool hitHovering;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		hitHoverEnter = false;
		hitHoverExit = false;

		hitSomething = false;
		if(Input.GetMouseButtonDown(0))
		{
			hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if(hitInfo.collider != null)
			{
				//Debug.Log(Time.time + " hit something");
				hitSomething = true;
			}
		}
			
		hitInfoHover = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if(hitInfoHover.collider != null)
		{
			if(!hitHovering)
			{
				//Debug.Log(Time.time + " hitHoverEnter");
				hitHoverEnter = true;
				hitHovering = true;
			}
		}else
		{
			if(hitHovering)
			{
				//Debug.Log(Time.time + " hitHoverExit");
				hitHoverExit = true;
			}
			hitHovering = false;
		}
	}

	void LateUpdate()
	{
		
	}

	public static void PlaySFX(AudioSource sound, float mod, bool loop)
	{
		sound.loop = loop;
		sound.volume = (Globals.sfxVolume/100.0f)*.3f*mod;
		sound.Play();
	}

	public static void PlayMusic(AudioSource sound)
	{
		sound.volume = (Globals.musicVolume/100.0f)*.1f;
		sound.Play();
	}

	public static GameObject CreateTextObject(Font font, Material mat)
	{
		GameObject fontObj = new GameObject();
		fontObj.layer = LayerMask.NameToLayer("GUI");
		TextMesh tm = fontObj.AddComponent<TextMesh>();
		MeshRenderer mRenderer = fontObj.GetComponent<MeshRenderer>();
		mRenderer.material = mat;//font.material;
		tm.font = font;
		/*Shader zTextShader = Shader.Find("Resources/Shaders/zTextShader");
		if(font.name.Equals("origami"))
		{
			zTextShader = Shader.Find("Resources/Shaders/Textured Text Shader");
		}*/
		//tm.gameObject.GetComponent<Renderer>().material.shader = zTextShader;
		tm.gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", mat.mainTexture);

		fontObj.transform.localScale = new Vector3(1,1,1);

		return fontObj;
	}

	public static GameObject CreateBillboard()
	{
		GameObject planeObj = Resources.Load("Meshes/plane") as GameObject;

		GameObject tmp = Instantiate(planeObj, Vector3.zero, Quaternion.Euler(90, 180, 0)) as GameObject;
		tmp.transform.localScale = new Vector3(1,1,1);
		Transform child = tmp.transform.GetChild(0);
		child.parent = null;
		Destroy(tmp);
		Renderer rend = child.gameObject.GetComponent<Renderer>();
		rend.material.shader = Shader.Find("Standard");
		rend.material.DisableKeyword("_ALPHATEST_ON");
		rend.material.EnableKeyword("_ALPHABLEND_ON");
		rend.material.EnableKeyword("_EMISSION");
		rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		rend.material.SetColor("_EmissionColor", Color.white);
		rend.material.SetFloat("_Mode", 2);
		rend.material.SetFloat("_Glossiness", 0);
		rend.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
		rend.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
		rend.material.SetInt("_ZWrite", 0);
		rend.material.renderQueue = 3000;
		rend.material.shader = Shader.Find("Transparent/Diffuse");
		rend.material.color = Color.white;
		return child.gameObject;
	}

	public static Transform GetChildTransform(Transform t, string tname)
	{
		Transform res = null;
		if(t == null)
		{
			return res;
		}

		if(t.gameObject.name.Equals(tname))
		{
			return t;
		}
		int ccount = t.childCount;
		for(int i = 0; i < ccount; i++)
		{
			res = GetChildTransform(t.GetChild(i), tname);
			if(res != null)
			{
				return res;
			}
		}
		return res;
	}

	public static Vector2 GetScreenDims(GameObject go, Camera cam)
	{
		Renderer rend = go.GetComponent<Renderer>();
		float xLeft = go.transform.position.x - rend.bounds.extents.x;
		float yBottom = go.transform.position.y - rend.bounds.extents.y;
		float xRight = go.transform.position.x + rend.bounds.extents.x;
		float yTop = go.transform.position.y + rend.bounds.extents.y;

		Vector3 p1 = new Vector3(xLeft, yBottom, go.transform.position.z);
		Vector3 p2 = new Vector3(xRight, yTop, go.transform.position.z);
		p1 = cam.WorldToScreenPoint(p1);
		p2 = cam.WorldToScreenPoint(p2);

		Vector2 ret = new Vector2(p2.x - p1.x, p2.y - p1.y);

		return ret;
	}

	public static void SetObjectWidth(ref GameObject go, float tarWidth, Camera cam)
	{
		float width = 0;
		Renderer rend = go.GetComponent<Renderer>();
		while(width < tarWidth)
		{
			go.transform.localScale = new Vector3(go.transform.localScale.x * 1.01f, go.transform.localScale.y, go.transform.localScale.z);
			float xLeft = go.transform.position.x - rend.bounds.extents.x;
			float yBottom = go.transform.position.y - rend.bounds.extents.y;
			float xRight = go.transform.position.x + rend.bounds.extents.x;
			float yTop = go.transform.position.y + rend.bounds.extents.y;

			Vector3 p1 = new Vector3(xLeft, yBottom, go.transform.position.z);
			Vector3 p2 = new Vector3(xRight, yTop, go.transform.position.z);
			p1 = cam.WorldToScreenPoint(p1);
			p2 = cam.WorldToScreenPoint(p2);

			width = p2.x - p1.x;
		}
	}

	public static void SetObjectHeight(ref GameObject go, float tarHeight, Camera cam)
	{
		float height = 0;
		Renderer rend = go.GetComponent<Renderer>();
		while(height < tarHeight)
		{
			go.transform.localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.y, go.transform.localScale.z * 1.01f);
			float xLeft = go.transform.position.x - rend.bounds.extents.x;
			float yBottom = go.transform.position.y - rend.bounds.extents.y;
			float xRight = go.transform.position.x + rend.bounds.extents.x;
			float yTop = go.transform.position.y + rend.bounds.extents.y;

			Vector3 p1 = new Vector3(xLeft, yBottom, go.transform.position.z);
			Vector3 p2 = new Vector3(xRight, yTop, go.transform.position.z);
			p1 = cam.WorldToScreenPoint(p1);
			p2 = cam.WorldToScreenPoint(p2);

			height = p2.y - p1.y;
			if(go.transform.localScale.y == Mathf.Infinity)
			{
				height  = float.MaxValue;
			}
		}
	}
}
