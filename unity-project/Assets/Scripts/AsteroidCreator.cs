using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidCreator : MonoBehaviour {

	public enum AsteroidTypes {s, c, q, x, v, u, r, d, t, m, e, o, p, a};
	public static Dictionary<string, string[]> AsteroidCompositions = new Dictionary<string, string[]>();
	public static Dictionary<string, float[]> AsteroidReflectance = new Dictionary<string, float[]>();

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

	public string[] GetAsteroidComposition(string key) {
		return AsteroidCompositions[key];
	}

	public float[] GetAsteroidReflectance(string key) {
		return AsteroidReflectance[key];
	}

	public GameObject AsteroidTemplate; // a new asteroid sans the mesh
	public GameObject [] Asteroids; // all possible asteroid meshes

	void Awake() {
		AsteroidCompositions.Add("s", new string[] {"metal", "olivine", "pyroxene"});
		AsteroidCompositions.Add("c", new string[] {"silicates", "water", "carbon"});
		AsteroidCompositions.Add("q", new string[] {"olivine", "pyroxene", "metal"});
		AsteroidCompositions.Add("x", new string[] {"metal"});
		AsteroidCompositions.Add("v", new string[] {"pyroxene", "feldspar"});
		AsteroidCompositions.Add("u", new string[] {"unknown"});
		AsteroidCompositions.Add("r", new string[] {"pyroxene", "olivine"});
		AsteroidCompositions.Add("d", new string[] {"silicates", "carbon"});
		AsteroidCompositions.Add("t", new string[] {"silicates", "carbon"});
		AsteroidCompositions.Add("m", new string[] {"metal", "enstatite"});
		AsteroidCompositions.Add("e", new string[] {"enstatite"});
		AsteroidCompositions.Add("o", new string[] {"silicates", "water", "carbon"});
		AsteroidCompositions.Add("p", new string[] {"silicates", "carbon"});
		AsteroidCompositions.Add("a", new string[] {"olivine", "metal"});

		AsteroidReflectance.Add("s", new float[] {0.17f, 0.83f, 0.50f});
		AsteroidReflectance.Add("c", new float[] {0.33f, 0.50f, 0.50f});
		AsteroidReflectance.Add("q", new float[] {0.17f, 0.83f, 0.17f});
		AsteroidReflectance.Add("x", new float[] {0.17f, 0.67f, 0.67f});
		AsteroidReflectance.Add("v", new float[] {0.00f, 1.00f, 0.00f});
		AsteroidReflectance.Add("u", new float[] {0.00f, 0.00f, 0.00f});
		AsteroidReflectance.Add("r", new float[] {0.00f, 0.83f, 0.33f});
		AsteroidReflectance.Add("d", new float[] {0.33f, 0.67f, 0.83f});
		AsteroidReflectance.Add("t", new float[] {0.33f, 0.67f, 0.67f});
		AsteroidReflectance.Add("m", new float[] {0.33f, 0.67f, 0.67f});
		AsteroidReflectance.Add("e", new float[] {0.33f, 0.50f, 0.67f});
		AsteroidReflectance.Add("o", new float[] {0.33f, 0.50f, 0.17f});
		AsteroidReflectance.Add("p", new float[] {0.50f, 0.67f, 0.67f});
		AsteroidReflectance.Add("a", new float[] {0.17f, 1.00f, 0.83f});


	}

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


