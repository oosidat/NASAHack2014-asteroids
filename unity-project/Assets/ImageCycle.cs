using UnityEngine;
using System.Collections;

public class ImageCycle : MonoBehaviour {

	public Texture2D[] images;
	int image_num = 0;

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
			}

		if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			image_num--;	
						if (image_num < 0) {
							image_num = 12;		
						}
			renderer.material.mainTexture = images[image_num];
		}


	}
}
