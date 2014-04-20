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
			Controls controls = GameObject.Find ("Player").GetComponent<Controls>();
			float score = controls.currentMoney;
			Debug.Log("score: " + score);
			GameObject.Find ("GameOver").renderer.enabled=true;
			GameObject.Find ("GameOver").collider.enabled=true;
			GameObject.Find ("GameOverText").GetComponent<TextMesh>().text="Congratulations!\n\nYou mined $"+score.ToString("0.00");;
			GameObject.Find ("GameOverText").renderer.enabled=true;


		}
		renderer.material.mainTexture = images[value];


	}
}
