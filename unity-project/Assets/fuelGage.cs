using UnityEngine;
using System.Collections;

public class fuelGage : MonoBehaviour {
	public Texture2D[] images;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeTexture(int value)
	{

		if (value <= 0) {
			value = 0;
			GameObject.Find ("GameOver").renderer.enabled=true;
			GameObject.Find ("GameOver").collider.enabled=true;
			GameObject.Find ("GameOverText").renderer.enabled=true;

				}
		renderer.material.mainTexture = images[value];


	}
}
