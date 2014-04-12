using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidCreator : MonoBehaviour {

//	public string ChooseAsteroidClass() {
//
//		List<KeyValuePair<string, double>> types = new List<KeyValuePair<string, double>>();
//		types.Add(new KeyValuePair<string, double>("p", 0.43859649122807));
//		types.Add(new KeyValuePair<string, double>("d", 0.87719298245614));
//		types.Add(new KeyValuePair<string, double>("c", 14.9122807017544));
//		types.Add(new KeyValuePair<string, double>("t", 0.87719298245614));
//		types.Add(new KeyValuePair<string, double>("s", 47.3684210526316));
//		types.Add(new KeyValuePair<string, double>("q", 14.9122807017544));
//		types.Add(new KeyValuePair<string, double>("v", 6.14035087719298));
//		types.Add(new KeyValuePair<string, double>("r", 1.31578947368421));
//		types.Add(new KeyValuePair<string, double>("m", 0.87719298245614));
//		types.Add(new KeyValuePair<string, double>("a", 0.219298245614035));
//		types.Add(new KeyValuePair<string, double>("e", 0.87719298245614));
//		types.Add(new KeyValuePair<string, double>("x", 8.7719298245614));
//		types.Add(new KeyValuePair<string, double>("o", 0.87719298245614));
//		types.Add(new KeyValuePair<string, double>("u", 1.75438596491228));
//
//
//		Random r = new Random();
//		double diceRoll = r.NextDouble();
//		
//		double cumulative = 0.0;
//		for (int i = 0; i < elements.Count; i++)
//		{
//			cumulative += elements[i].Value;
//			if (diceRoll < cumulative)
//			{
//				return elements[i].Key;
//			}
//		}
//	}


	public GameObject [] Asteroids;

	// Use this for initialization
	void Start () {
		GameObject Asteroid = GameObject.Instantiate (Asteroids [Random.Range (0, 19)]) as GameObject;
		Asteroid.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


