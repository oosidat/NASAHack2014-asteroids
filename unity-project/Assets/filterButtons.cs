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
				renderer.material.mainTexture = images[1];
				audio.PlayOneShot(filter_apply);
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "visbutton")
			{
				print ("visbutton");
				int filtermask = 2;
				renderer.material.mainTexture = images[2];
				audio.PlayOneShot(filter_apply);
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "infrabutton")
			{
				print ("infrabutton");
				int filtermask = 3;
				renderer.material.mainTexture = images[3];
				audio.PlayOneShot(filter_apply);
			}
		}
	}
}
