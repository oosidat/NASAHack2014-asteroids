using UnityEngine;
using System.Collections;

public class filterButtons : MonoBehaviour {

	public Texture2D[] images;
	public AudioClip filter_apply;
	public int filtermask = 0;
	public float k;
	public bool changeColor = true;
	public bool changeShininess = true;

	private enum spectra {
		uv,
		vis,
		ir
	};

	void Start () {
	
	}

	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hitInfo = new RaycastHit();
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "uvbutton") {
				Debug.Log ("uvbutton");
				applyFilter((int) spectra.uv);
				renderer.material.mainTexture = images[0];
				audio.PlayOneShot(filter_apply);
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "visbutton") {
				Debug.Log ("visbutton");
				applyFilter((int) spectra.vis);
				renderer.material.mainTexture = images[1];
				audio.PlayOneShot(filter_apply);
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "infrabutton") {
				Debug.Log ("infrabutton");
				applyFilter ((int) spectra.ir);
				renderer.material.mainTexture = images[2];
				audio.PlayOneShot(filter_apply);
			}
		}
	}

	void applyFilter(int mask) {

		GameObject [] asteroids = GameObject.FindGameObjectsWithTag ("AsteroidGameObjects");
		int numAsteroids = (int)asteroids.Length;
		float color = 1.0f;
		float shininess = 1.0f;

		for (int i = 0; i < numAsteroids; i++) {

			Asteroid asteroidtmp = asteroids[i].GetComponent<Asteroid>();
			Transform asteroidChildTransform = asteroidtmp.transform.GetChild(0).transform.GetChild(0);


			// define the look of the asteroid based on filter mask
			if (mask == (int) spectra.uv) {

				color = (float)(asteroidtmp.uv * k);
				shininess = asteroidtmp.uv; //(float)(asteroidtmp.uv * 100.0);

			} else if (mask == (int) spectra.vis) {

				color = (float)(asteroidtmp.vis * k);
				shininess = asteroidtmp.ir;//(float)(asteroidtmp.ir * 100.0);

			} else if (mask == (int) spectra.ir) {

				color = (float)(asteroidtmp.ir * k);
				shininess = asteroidtmp.ir;//(float)(asteroidtmp.ir * 100.0);

			}

			// apply shininess
			if (changeShininess) {
				shininess = 1.0f - shininess;
			} else {
				shininess = 1.0f;
			}
			Debug.Log ("S:" + shininess);
			asteroidChildTransform.renderer.material.SetFloat("_Shininess", shininess);

			// apply colour
			if (!changeColor) {
				color = 1.0f;
			}
			Debug.Log ("C:" + color);
			asteroidChildTransform.renderer.material.SetColor("_Color", new Color(color, color, color));
		}
	}             
}