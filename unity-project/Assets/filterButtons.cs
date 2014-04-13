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

			if(mask==1)
			{
				asteroids[i].renderer.material.SetFloat("_Shinyness",(float)(asteroidtmp.uv*100.0));
			}
			if(mask==2)
			{
				asteroids[i].renderer.material.SetFloat("_Shinyness",(float)(asteroidtmp.vis*100.0));
			}
			if(mask==3)
			{
				asteroids[i].renderer.material.SetFloat("_Shinyness",(float)(asteroidtmp.ir*100.0));
			}
				}
		}
	             
}
