using UnityEngine;
using System.Collections;

public class filterButtons : MonoBehaviour {
	public Texture2D[] images;
	public AudioClip filter_apply;
	public int filtermask = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo = new RaycastHit();
			//DESELECT
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "uvbutton")
			{
				print ("uvbutton");
				int filtermask = 1;
				applyfilter(filtermask);
				renderer.material.mainTexture = images[0];
				audio.PlayOneShot(filter_apply);
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "visbutton")
			{
				print ("visbutton");
				int filtermask = 2;
				applyfilter(filtermask);
				renderer.material.mainTexture = images[1];
				audio.PlayOneShot(filter_apply);
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "infrabutton")
			{
				print ("infrabutton");
				int filtermask = 3;
				applyfilter (filtermask);
				renderer.material.mainTexture = images[2];
				audio.PlayOneShot(filter_apply);
			}
		}
	}

	void applyfilter(int mask){
		GameObject [] asteroids = GameObject.FindGameObjectsWithTag ("AsteroidGameObjects");
		for (int i=0; i<(int)asteroids.Length; i++) {

			Asteroid asteroidtmp = asteroids[i].GetComponent<Asteroid>();
			Transform asteroidChildTransform = asteroidtmp.transform.GetChild(0).transform.GetChild(0);

			float baseColor = 0.5f;
			float r = baseColor;
			float g = baseColor;
			float b = baseColor;
			float k = 0.2f;

			// update the shininess of the asteroid based on filter
			if(mask==1)
			{
				// reduce g to make it a purple tint
				//g = g - asteroidtmp.uv * k;

				asteroidChildTransform.renderer.material.SetFloat("_Shininess",(float)(asteroidtmp.uv*100.0));
				//asteroidChildTransform.renderer.material.SetColor("_Color", new Color(baseColor * k, asteroidtmp.uv, baseColor));
				//asteroidChildTransform.renderer.material.SetColor("_SpecColor", new Color(baseColor, asteroidtmp.uv, baseColor));
			}
			if(mask==2)
			{
				asteroidChildTransform.renderer.material.SetFloat("_Shininess",(float)(asteroidtmp.vis*100.0));
				//asteroidChildTransform.renderer.material.SetColor("_Color", new Color(baseColor, baseColor, baseColor));
				//asteroidChildTransform.renderer.material.SetColor("_SpecColor", new Color(baseColor, baseColor, baseColor));
			}
			if(mask==3)
			{
				// reduce g and b to make it a red tint
				//g = g - asteroidtmp.ir * k;
				//b = g;

				asteroidChildTransform.renderer.material.SetFloat("_Shininess",(float)(asteroidtmp.ir*100.0));
				//asteroidChildTransform.renderer.material.SetColor("_Color", new Color(baseColor * k, asteroidtmp.ir, asteroidtmp.ir));
				//asteroidChildTransform.renderer.material.SetColor("_SpecColor", new Color(baseColor, asteroidtmp.ir, asteroidtmp.ir));
			}
		}
	}             
}
