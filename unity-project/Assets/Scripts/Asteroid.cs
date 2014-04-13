using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour {
	public string asteroidType;
	public string[] composition;

	void Start () {
		AsteroidCreator creator = new AsteroidCreator ();
		asteroidType = creator.GetAsteroidType ();
		composition = creator.GetAsteroidComposition(asteroidType);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}