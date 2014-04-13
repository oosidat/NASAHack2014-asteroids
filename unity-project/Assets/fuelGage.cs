﻿using UnityEngine;
using System.Collections;

public class fuelGage : MonoBehaviour {
	public Texture2D[] images;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeTexture(int value)
	{
		if (value < 0) {
			value = 0;
				}
		renderer.material.mainTexture = images[value];
	}
}
