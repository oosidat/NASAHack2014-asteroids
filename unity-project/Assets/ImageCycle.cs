using UnityEngine;
using System.Collections;

public class ImageCycle : MonoBehaviour {

	public Texture2D[] images;
	public AudioClip type_cycle;
	int image_num = 0;





	//All of the nonsense I put in the script should be consolodated into one function.





	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.RightArrow))
		    {
				image_num++;
						if (image_num >= images.Length) {
							image_num = 0;		
						}
				renderer.material.mainTexture = images[image_num];
				audio.PlayOneShot(type_cycle);
			}

		if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			image_num--;	
						if (image_num < 0) {
							image_num = 12;		
						}
			renderer.material.mainTexture = images[image_num];
			audio.PlayOneShot(type_cycle);	
		}
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo = new RaycastHit();
			//DESELECT
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Graphs")
			{
				print ("Next Image");
				image_num++;
				if (image_num >= images.Length) {
					image_num = 0;		
				}
				renderer.material.mainTexture = images[image_num];
				audio.PlayOneShot(type_cycle);
			}
		}



		if (Input.GetAxis("Mouse ScrollWheel") > 0){
			image_num++;
			if (image_num >= images.Length) {
				image_num = 0;		
			}
			renderer.material.mainTexture = images[image_num];
			audio.PlayOneShot(type_cycle);
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0){
			image_num--;	
			if (image_num < 0) {
				image_num = 12;		
			}
			renderer.material.mainTexture = images[image_num];
			audio.PlayOneShot(type_cycle);	
		}


	}


}
