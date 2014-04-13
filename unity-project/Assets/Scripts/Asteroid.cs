using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour {
	public string asteroidType;
	public string[] composition;
	public float uv;
	public float vis;
	public float ir;

	public void SetAsteroidAttributes(string asteroidType, 
	                                  string[] composition,
	                                  float uv,
	                                  float vis,
	                                  float ir
	                                  ){
		this.asteroidType = asteroidType;
		this.composition = composition;
		this.uv = uv;
		this.vis = vis;
		this.ir = ir;
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}