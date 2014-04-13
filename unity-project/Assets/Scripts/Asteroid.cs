using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour {
	public string asteroidType;
	public string[] composition;
	public float uv;
	public float vis;
	public float ir;

	public Asteroid() {
		AsteroidCreator creator = new AsteroidCreator ();
		asteroidType = creator.GetAsteroidType ();
		composition = creator.GetAsteroidComposition(asteroidType);
		float [] reflectance = creator.GetAsteroidReflectance (asteroidType);
		uv = reflectance[0];
		vis = reflectance[1];
		ir = reflectance[2];
	}

	/* I think this constructor can and should be used when populating a belt, but I'm not sure... */

	public Asteroid(string type) {
		AsteroidCreator creator = new AsteroidCreator ();
		asteroidType = type;
		composition = creator.GetAsteroidComposition(asteroidType);
		float [] reflectance = creator.GetAsteroidReflectance (asteroidType);
		uv = reflectance[0];
		vis = reflectance[1];
		ir = reflectance[2];
	}

	void Start () {
		AsteroidCreator creator = new AsteroidCreator ();
		asteroidType = creator.GetAsteroidType ();
		composition = creator.GetAsteroidComposition(asteroidType);
		float [] reflectance = creator.GetAsteroidReflectance (asteroidType);
		uv = reflectance[0];
		vis = reflectance[1];
		ir = reflectance[2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}