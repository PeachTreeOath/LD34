﻿using UnityEngine;
using System.Collections;

public class Rise : MonoBehaviour {

	public float moveSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += Vector3.up * Time.deltaTime * moveSpeed;
	}
}
