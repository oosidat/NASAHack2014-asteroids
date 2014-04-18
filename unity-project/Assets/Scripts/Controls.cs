using UnityEngine;
using System.Collections;
using System;

public class Controls : MonoBehaviour {
	public Transform target;
	public float currentFuel;
	public float fuelBurnRate;
	public float currentMoney;
	public float lastAstVal;
	public Shader shader1 = Shader.Find("Bumped Specular");
	public Shader shader2 = Shader.Find("Outlined/Gem");
	public Shader shader3 = Shader.Find("Transparent Effects/CheapForcefield");
	private GameObject last_asteroid;
	private GameObject last_asteroid_go;
	public AudioClip mine;
	// Use this for initialization
	void Start () {
		
		//currentFuel = 1.0f;
		currentMoney = 0.0f;
		lastAstVal = 0.0f;
		//transform.rotation = Quaternion.LookRotation(transform.position - target.position);
	}
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown("escape")) {//When a key is pressed down it see if it was the escape key if it was it will execute the code
			Application.Quit(); // Quits the game
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			if (last_asteroid_go){
				last_asteroid_go.renderer.material.shader = shader1;
			}
			RaycastHit hitInfo = new RaycastHit();
			
			
			//DESELECT
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Untagged")
			{
				//print (hitInfo.transform.tag);
			}
			//SELECT ASTEROID
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Asteroid")
			{
				//print (hitInfo.transform.tag);
				//transform.LookAt(hitInfo.transform);
			}
			//SELECT ASTEROID
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "AsteroidChild")
			{
				GameObject.Find ("MineText").renderer.enabled = true;
				GameObject.Find ("MineText").collider.enabled = true;
				GameObject.Find ("MineButton").renderer.enabled = true;



				//print (hitInfo.transform.tag);
				//transform.LookAt(hitInfo.transform);
				//hitInfo.transform.renderer.material.shader.


				hitInfo.transform.gameObject.renderer.material.shader = shader2;
				hitInfo.transform.gameObject.renderer.material.SetColor("_OutlineColor", Color.cyan);


				last_asteroid = hitInfo.transform.parent.parent.gameObject;
				last_asteroid_go = hitInfo.transform.gameObject;
				Asteroid asteroid = hitInfo.transform.parent.parent.GetComponent<Asteroid>();
				//hitInfo.transform.parent.
				AsteroidCreator asteroidCreator = new AsteroidCreator();
				//Asteroid asteroid = hitInfo.parent.parent.GetComponent<Asteroid>();
				float sum = 0;
				foreach (string resource in asteroid.composition){
					sum += asteroidCreator.GetResourcePrice(resource);
				}

				//print (asteroid.asteroidType);
				//print (sum);
				lastAstVal = sum;
			}
			
			
			
			//GUI BUTTONS
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "MineAsteroid")
			{

				currentMoney = currentMoney+lastAstVal;
				currentFuel -= 5f;
				print ("current fuel: "+currentFuel);

				float mulipliedByNumberOfBlocks = currentFuel/10.0f;
				double roundUp = Math.Ceiling (mulipliedByNumberOfBlocks);
				
				int fuelGaugeBlocks = (int)roundUp;
				fuelGage fuelGauge = GameObject.Find ("FuelCell").GetComponent<fuelGage>();
				fuelGauge.changeTexture(fuelGaugeBlocks);
				
				String fuelText = "Current Charge: "+ string.Format("{0:f1}", currentFuel);
				GameObject.FindGameObjectWithTag ("fuelLabel").GetComponent<TextMesh>().text = fuelText;
				//String currentMoneyString = "Cash: $"+currentMoney.ToString("0.00");
				GameObject.FindGameObjectWithTag ("CashText").GetComponent<TextMesh> ().text ="Cash: $"+currentMoney.ToString("0.00");// currentMoneyString;

				//Destroy(last_asteroid);
				last_asteroid_go.renderer.material.shader = shader3;
				last_asteroid.gameObject.tag = "Untagged";
				last_asteroid_go.gameObject.tag = "Untagged";
				last_asteroid_go = null;
				last_asteroid = null;



				audio.PlayOneShot(mine);
			}
			
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Scan_UV")
			{
				//print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Scan_IR")
			{
				//print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Scan_VISIBLE")
			{
				//print ("It's working");
			}
			
			
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Move")
			{
				//print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Destroy")
			{
				//print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Capture")
			{
				//print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "NextSpec")
			{
				//print ("It's working");
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "GameOverClick")
			{
				GameObject.Find ("GameOverText").GetComponent<TextMesh>().text="Loading...";
				Application.LoadLevel(Application.loadedLevel);
			}
			
			if ( hitInfo.transform & hitInfo.transform.tag != "AsteroidChild")
			{
				GameObject.Find ("MineText").renderer.enabled = false;
				GameObject.Find ("MineText").collider.enabled = false;
				GameObject.Find ("MineButton").renderer.enabled = false;
			}
			
		}
	}
	
}
