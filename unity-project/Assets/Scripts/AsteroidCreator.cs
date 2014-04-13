using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidCreator : MonoBehaviour {

	public enum AsteroidTypes {s, c, q, x, v, u, r, d, t, m, e, o, p, a};

	public static Dictionary<int, int[]> AsteroidsByRadius = new Dictionary<int, int[]>();


	public void InitializeAsteroidsByRadius() {
		AsteroidsByRadius.Add(8, new int[] {2842, 447, 895, 132, 368, 0, 79, 26, 26, 13, 0, 26, 13, 3});
		AsteroidsByRadius.Add(9, new int[] {2368, 522, 746, 175, 307, 0, 66, 31, 31, 18, 0, 31, 15, 4});
		AsteroidsByRadius.Add(10, new int[] {24, 12, 24, 4, 24, 0, 24, 12, 12, 4, 0, 12, 12, 4});
		AsteroidsByRadius.Add(11, new int[] {18, 15, 18, 4, 18, 0, 18, 15, 15, 4, 0, 15, 15, 4});
		AsteroidsByRadius.Add(12, new int[] {13, 18, 13, 6, 13, 0, 13, 18, 18, 6, 1, 18, 18, 6});
		AsteroidsByRadius.Add(13, new int[] {14, 20, 14, 5, 14, 0, 14, 20, 20, 5, 1, 20, 20, 5});
		AsteroidsByRadius.Add(14, new int[] {3, 29, 3, 5, 3, 0, 3, 29, 29, 5, 2, 29, 29, 5});
	}

	public static Dictionary<string, string[]> AsteroidCompositions = new Dictionary<string, string[]>();

	public void InitializeAsteroidCompositions() {
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
	}

	public string[] GetAsteroidComposition(string key) {
		string[] value;
		if (AsteroidCompositions.TryGetValue(key, out value)) {
			return value;
		}
		else {
			Debug.Log ("Not Found: " + key);
			return new string[] {};
		}
	}

	public static Dictionary<string, float[]> AsteroidReflectance = new Dictionary<string, float[]>();

	public void InitializeAsteroidReflectance() {
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

	public float[] GetAsteroidReflectance(string key) {
		float[] value;
		if (AsteroidReflectance.TryGetValue(key, out value)) {
			return value;
		}
		else {
			Debug.Log ("Not Found: " + key);
			return new float[] {0,0,0};
		}
	}

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

	public string GetAsteroidTypeByRadius(int radius) {
		int[] weights = AsteroidsByRadius[radius];
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

	public void SpawnGameObjects(int number, int radius, int au) {
		for (int i = 0; i < number; i++) {
			GameObject asteroidGameObj = GameObject.Instantiate(Asteroids [Random.Range (0, 19)]) as GameObject;
			
			// create a new asteroid from the template
			GameObject newAsteroid = GameObject.Instantiate (AsteroidTemplate) as GameObject;
			
			// attach asteroid game object to the newAsteroid
			asteroidGameObj.transform.parent = newAsteroid.transform;
			newAsteroid.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);

			Vector2 belt = Random.insideUnitCircle.normalized * radius;
			float variance = 20.0f;
			newAsteroid.transform.localPosition = new Vector3 (Random.Range(belt.x-variance,belt.x+variance),
			                                                   Random.Range(4.0f, 6.0f),
			                                                   Random.Range(belt.y-variance, belt.y+variance)
			                                                   );
			// set asteoriod properties 
			string asteroidType = GetAsteroidTypeByRadius(au);
			float [] asteroidRefl = GetAsteroidReflectance(asteroidType);

			Asteroid asteroid = newAsteroid.GetComponent<Asteroid>();
			asteroid.SetAsteroidAttributes(asteroidType, 
			                               GetAsteroidComposition(asteroidType),
			                               asteroidRefl[0], 
			                               asteroidRefl[1], 
			                               asteroidRefl[2]);

		}
	}
	public GameObject AsteroidTemplate; // a new asteroid sans the mesh
	public GameObject [] Asteroids; // all possible asteroid meshes

	void Awake() {
		InitializeAsteroidCompositions ();
		InitializeAsteroidReflectance ();
		InitializeAsteroidsByRadius ();
	}

	// Use this for initialization
	void Start () {
		SpawnGameObjects(150,200,8);
		SpawnGameObjects(150,300,10);
		SpawnGameObjects(150,400,11);
		SpawnGameObjects(150,500,12);
		SpawnGameObjects (150, 600, 13);
		SpawnGameObjects (150, 700, 14);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


