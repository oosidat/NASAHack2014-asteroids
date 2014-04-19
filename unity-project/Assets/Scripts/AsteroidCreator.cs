using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidCreator : MonoBehaviour {
	
	public enum AsteroidTypes {s, c, q, x, v, u, r, d, t, m, e, o, p, a};
	
	public static Dictionary<int, int[]> AsteroidsByRadius = new Dictionary<int, int[]>();

	public GameObject UiFilterButtons;
	public int numAsteroidsToSpawn = 75;
	public void InitializeAsteroidsByRadius() {

		AsteroidsByRadius.Clear ();

		AsteroidsByRadius[8] = new int[] {2842, 447, 895, 132, 368, 0, 79, 26, 26, 13, 0, 26, 13, 3};
		AsteroidsByRadius[9] = new int[] {2368, 522, 746, 175, 307, 0, 66, 31, 31, 18, 0, 31, 15, 4};
		AsteroidsByRadius[10] = new int[] {24, 12, 24, 4, 24, 0, 24, 12, 12, 4, 0, 12, 12, 4};
		AsteroidsByRadius[11] = new int[] {18, 15, 18, 4, 18, 0, 18, 15, 15, 4, 0, 15, 15, 4};
		AsteroidsByRadius[12] = new int[] {13, 18, 13, 6, 13, 0, 13, 18, 18, 6, 1, 18, 18, 6};
		AsteroidsByRadius[13] = new int[] {14, 20, 14, 5, 14, 0, 14, 20, 20, 5, 1, 20, 20, 5};
		AsteroidsByRadius[14] = new int[] {3, 29, 3, 5, 3, 0, 3, 29, 29, 5, 2, 29, 29, 5};
	}
	
	public static Dictionary<string, string[]> AsteroidCompositions = new Dictionary<string, string[]>();
	
	public void InitializeAsteroidCompositions() {

		AsteroidCompositions.Clear ();

		AsteroidCompositions["s"] = new string[] {"Metal", "Olivine", "Pyroxene"};
		AsteroidCompositions["c"] = new string[] {"Silicates", "Water", "Carbon"};
		AsteroidCompositions["q"] = new string[] {"Olivine", "Pyroxene", "Metal"};
		AsteroidCompositions["x"] = new string[] {"Metal"};
		AsteroidCompositions["v"] = new string[] {"Pyroxene", "Feldspar"};
		AsteroidCompositions["u"] = new string[] {"Unknown"};
		AsteroidCompositions["r"] = new string[] {"Pyroxene", "Olivine"};
		AsteroidCompositions["d"] = new string[] {"Silicates", "Carbon"};
		AsteroidCompositions["t"] = new string[] {"Silicates", "Carbon"};
		AsteroidCompositions["m"] = new string[] {"Metal", "Enstatite"};
		AsteroidCompositions["e"] = new string[] {"Enstatite"};
		AsteroidCompositions["o"] = new string[] {"Silicates", "Water", "Carbon"};
		AsteroidCompositions["p"] = new string[] {"Silicates", "Carbon"};
		AsteroidCompositions["a"] = new string[] {"Olivine", "Metal"};
	}

	public static Dictionary<string,float> minPrice = new Dictionary<string,float >();
	
	public void InitPrices(){

		minPrice.Clear ();

		minPrice["Feldspar"] = 60.9f;
		minPrice["Olivine"] = 314.84f;
		minPrice["Pyroxene"] = 314.84f;
		minPrice["Enstatite"] =314.84f;
		minPrice["Carbon"] = 0.0f;
		minPrice["Silicates"] = 34.81f;
		minPrice["Water"] = 0.0011f;
		minPrice["Metal"] = 2231.25f;
		minPrice ["Unknown"] = 0.0f;
		
	}

	public float GetResourcePrice(string key) {
		return minPrice [key];
	}
	
	public string[] GetAsteroidComposition(string key) {
		string[] value;
		if (AsteroidCompositions.TryGetValue(key, out value)) {
			return value;
		}
		else {
			//Debug.Log ("Not Found: " + key);
			return new string[] {};
		}
	}
	
	public static Dictionary<string, float[]> AsteroidReflectance = new Dictionary<string, float[]>();
	
	public void InitializeAsteroidReflectance() {

		AsteroidReflectance.Clear ();

		AsteroidReflectance["s"] = new float[] {0.17f, 0.83f, 0.50f};
		AsteroidReflectance["c"] = new float[] {0.33f, 0.50f, 0.50f};
		AsteroidReflectance["q"] = new float[] {0.17f, 0.83f, 0.17f};
		AsteroidReflectance["x"] = new float[] {0.17f, 0.67f, 0.67f};
		AsteroidReflectance["v"] = new float[] {0.00f, 1.00f, 0.00f};
		AsteroidReflectance["u"] = new float[] {0.00f, 0.00f, 0.00f};
		AsteroidReflectance["r"] = new float[] {0.00f, 0.83f, 0.33f};
		AsteroidReflectance["d"] = new float[] {0.33f, 0.67f, 0.83f};
		AsteroidReflectance["t"] = new float[] {0.33f, 0.67f, 0.67f};
		AsteroidReflectance["m"] = new float[] {0.33f, 0.67f, 0.67f};
		AsteroidReflectance["e"] = new float[] {0.33f, 0.50f, 0.67f};
		AsteroidReflectance["o"] = new float[] {0.33f, 0.50f, 0.17f};
		AsteroidReflectance["p"] = new float[] {0.50f, 0.67f, 0.67f};
		AsteroidReflectance["a"] = new float[] {0.17f, 1.00f, 0.83f};
	}
	
	public float[] GetAsteroidReflectance(string key) {
		float[] value;
		if (AsteroidReflectance.TryGetValue(key, out value)) {
			return value;
		} else {
			//Debug.Log ("Not Found: " + key);
			return new float[] {0, 0, 0};
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
			float scale = Random.Range(0.2f, 0.5f);
			newAsteroid.transform.localScale = new Vector3 (scale, scale, scale);
			
			Vector2 belt = Random.insideUnitCircle.normalized * radius;
			float variance = 20.0f;
			newAsteroid.transform.localPosition = new Vector3 (Random.Range(belt.x-variance,belt.x+variance),
			                                                   Random.Range(-10.0f, 10.0f),
			                                                   Random.Range(belt.y-variance, belt.y+variance)
			                                                   );
			newAsteroid.transform.rotation = Random.rotation;

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
		InitPrices ();
	}
	
	// Use this for initialization
	void Start () {



	/*	SpawnGameObjects(numAsteroidsToSpawn, 200, 8);
		SpawnGameObjects(numAsteroidsToSpawn, 300, 10);
		SpawnGameObjects(numAsteroidsToSpawn, 400, 11);
		SpawnGameObjects(numAsteroidsToSpawn, 500, 12);
		SpawnGameObjects (numAsteroidsToSpawn, 600, 13);
		SpawnGameObjects (numAsteroidsToSpawn, 700, 14);*/
		/*
		SpawnGameObjects(numAsteroidsToSpawn, 200, 8);
		SpawnGameObjects(numAsteroidsToSpawn, 225, 10);
		SpawnGameObjects(numAsteroidsToSpawn, 340, 11);
		SpawnGameObjects(numAsteroidsToSpawn, 400, 12);
		SpawnGameObjects (numAsteroidsToSpawn, 500, 13);
		SpawnGameObjects (numAsteroidsToSpawn, 600, 14);
		*/
		SpawnGameObjects( 10,200, 8);
		SpawnGameObjects( 300,250, 9);
		SpawnGameObjects(50,300, 10);
		SpawnGameObjects(340,350, 11);
		SpawnGameObjects(50, 400,12);
		SpawnGameObjects (150,450, 13);
		SpawnGameObjects (10,500, 14);


		// apply default (vis) filter
		UiFilterButtons.GetComponent<filterButtons> ().applyFilter (1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


