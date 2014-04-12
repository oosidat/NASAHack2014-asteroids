using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidCreator : MonoBehaviour {

	public enum AsteroidTypes {s, c, q, x, v, u, r, d, t, m, e, o, p, a};

	/* Attempt to get an Asteroid Type, based on their distribution */
	public string GetAsteroidType() {

		int[] weights = new int[] { 54, 17, 17, 10, 7, 2, 2, 1, 1, 1, 1, 1, 1, 1 };

		int sum = 0;
		int i;
		for (i = 0; i < weights.Length; i++)
				sum += weights [i];
		int selection = Random.Range (0, sum);
		int count = 0;
		for (i = 0; i < weights.Length - 1; i++) {
				count += weights [i];
				if (selection < count)
						return ((AsteroidTypes)i).ToString ();
		}
		return ((AsteroidTypes)(weights.Length - 1)).ToString ();
	}

	public GameObject AsteroidTemplate; // a new asteroid sans the mesh
	public GameObject [] Asteroids; // all possible asteroid meshes

	// Use this for initialization
	void Start () {

		GameObject asteroidGameObj = GameObject.Instantiate(Asteroids [Random.Range (0, 19)]) as GameObject;

		// create a new asteroid from the template
		GameObject newAsteroid = GameObject.Instantiate (AsteroidTemplate) as GameObject;

		// attach asteroid game object to the newAsteroid
		asteroidGameObj.transform.parent = newAsteroid.transform;

		newAsteroid.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


