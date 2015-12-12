﻿using UnityEngine;
using System.Collections;

public class Uitlity : MonoBehaviour {

	public static RaycastHit hitInfo;
	public static bool hitSomething;

	public static RaycastHit hitInfoHover;
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
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hitInfo))
			{
				Debug.Log(Time.time + " hit something");
				hitSomething = true;
			}
		}

		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hitInfoHover))
			{
				if(!hitHovering)
				{
					hitHoverEnter = true;
					hitHovering = true;
				}
			}else
			{
				if(hitHovering)
				{
					hitHoverExit = true;
				}
				hitHovering = false;
			}
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
		Shader zTextShader = Shader.Find("Resources/Shaders/zTextShader");
		if(font.name.Equals("origami"))
		{
			zTextShader = Shader.Find("Resources/Shaders/Textured Text Shader");
		}
		tm.gameObject.GetComponent<Renderer>().material.shader = zTextShader;
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
}