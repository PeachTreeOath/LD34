﻿using UnityEngine;
using System.Collections;

public class BaseTile : MonoBehaviour {

	public float scale = 0;
	public float vel = 5f;
	public float accel = 12f;

	private bool increasing = true;
	private bool isAnimating = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isAnimating) {
			transform.localScale = new Vector3 (scale, scale, 1);
			scale += vel * Time.deltaTime;	
			vel -= accel * Time.deltaTime;
			if (scale > 1.05) {
				if (increasing) {
					increasing = !increasing;
				}
			} else {
				if (scale < 1) {
					if (!increasing) {
						transform.localScale = new Vector3 (1, 1, 1);
						isAnimating = false;
					}
				}
			}
		}
	}

	public void SkipAnimation()
	{
		isAnimating = false;
	}
}
