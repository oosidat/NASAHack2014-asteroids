using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public Transform target;
	public float currentFuel;
	public float currentMoney;
		

	// Use this for initialization
	void Start () {
		currentFuel = 1.0f;
		currentMoney = 0.0f;
		//transform.rotation = Quaternion.LookRotation(transform.position - target.position);
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown("escape")) {//When a key is pressed down it see if it was the escape key if it was it will execute the code
			Application.Quit(); // Quits the game
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hitInfo = new RaycastHit();


			//DESELECT
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Untagged")
			{
				print (hitInfo.transform.tag);
			}
			//SELECT ASTEROID
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Asteroid")
			{
				print (hitInfo.transform.tag);
				//transform.LookAt(hitInfo.transform);
			}
			//SELECT ASTEROID
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "AsteroidChild")
			{
				GameObject.Find ("MineText").renderer.enabled = true;
				GameObject.Find ("MineText").collider.enabled = true;

				print (hitInfo.transform.tag);
				//transform.LookAt(hitInfo.transform);

				Asteroid asteroid = hitInfo.transform.parent.parent.GetComponent<Asteroid>();
			 //hitInfo.transform.parent.
				AsteroidCreator asteroidCreator = new AsteroidCreator();
				//Asteroid asteroid = hitInfo.parent.parent.GetComponent<Asteroid>();
				float sum = 0;
				foreach (string resource in asteroid.composition){
					sum += asteroidCreator.GetResourcePrice(resource);
				}
				print (asteroid.asteroidType);
				print (sum);
			}



			//GUI BUTTONS
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "MineAsteroid")
			{
				print ("Mine Asteroid and Pay me");




			}

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Scan_UV")
			{
				print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Scan_IR")
			{
				print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Scan_VISIBLE")
			{
				print ("It's working");
			}


			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Move")
			{
				print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Destroy")
			{
				print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Capture")
			{
				print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "NextSpec")
			{
				print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "PrevSpec")
			{
				print ("It's working");
			}

			if ( hitInfo.transform.tag != "AsteroidChild")
			{
				GameObject.Find ("MineText").renderer.enabled = false;
				GameObject.Find ("MineText").collider.enabled = false;
			}

		}
	}

}
