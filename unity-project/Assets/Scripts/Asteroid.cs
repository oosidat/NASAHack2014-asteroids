using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour {
	public string myType;


	void Start () {
		AsteroidCreator creator = new AsteroidCreator ();
		myType = creator.GetAsteroidType ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}