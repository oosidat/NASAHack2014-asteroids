using UnityEngine;
using System.Collections;
using System;

public class Controls : MonoBehaviour {

	//Color c1 = Color.red;
	//Color c2 = Color.red;
	Color laserColor1 = new Color(1, 0, 0, 0.5f);	
	Color laserColor2 = new Color(1, .17f, .17f, 0.4f);	
	LineRenderer lineRenderer;
	public Transform Laser;
	public Material laserMat;

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
	public AudioClip asteroidselect;
	public AudioClip noselect;
	public AudioClip select;
	public float click_range;

	public String lastMined;

	//Lasers


	// Use this for initialization
	void Start () {
		//Lasers
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		//lineRenderer.material = new Material (Shader.Find("Particles/Additive"));
		lineRenderer.material = laserMat;
		lineRenderer.SetColors(laserColor1, laserColor1);
		lineRenderer.SetWidth(.3f,.1f);
		lineRenderer.SetVertexCount(2);
		//Lasers

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
		
		if (Input.GetMouseButtonDown(0)){

			if (last_asteroid_go){
				last_asteroid_go.renderer.material.shader = shader1;
			}
	
			RaycastHit hitInfo = new RaycastHit();
	
			
			//DESELECT
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, click_range)){



				Debug.DrawRay(Camera.main.transform.position, hitInfo.transform.position, Color.red, 5F);
				Debug.Log (hitInfo.transform.tag);
				//Select ASTEROID
				if (hitInfo.transform.tag == "AsteroidChild"){
					audio.PlayOneShot(select);

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
					AsteroidCreator asteroidCreator = new AsteroidCreator();


					float sum = 0;
					for (int i = 0; i < (asteroid.composition).Length; i++) {
						sum += asteroidCreator.GetResourcePrice(asteroid.composition[i]);
					}

					//print (asteroid.asteroidType);
					//print (sum);
					lastAstVal = sum;

					lastMined = "Last mined: Type " + asteroid.asteroidType.ToUpper() + ", containing " + String.Join(", ", asteroid.composition);

				}
				
				
				
				//MINE BUTTON
				else if (hitInfo.transform.tag == "MineAsteroid"){

					//Lasers
					lineRenderer.enabled=true;
					//lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z));
					lineRenderer.SetPosition(0, new Vector3(Laser.position.x,Laser.position.y,Laser.position.z));
					lineRenderer.SetPosition(1, last_asteroid_go.transform.position);
					StartCoroutine(laser_die());

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

					GameObject.FindGameObjectWithTag ("AsteroidText").GetComponent<TextMesh>().text = lastMined;

					//Destroy(last_asteroid);
					last_asteroid_go.renderer.material.shader = shader3;
					last_asteroid.gameObject.tag = "OldAsteroid";
					last_asteroid_go.gameObject.tag = "OldAsteroid";
					last_asteroid_go.collider.enabled = false;
					last_asteroid_go.rigidbody.isKinematic = true;
					last_asteroid_go.renderer.material.SetFloat("_Strength", 3f);
					float strength_start=last_asteroid_go.renderer.material.GetFloat("_Strength");
					StartCoroutine(asteroid_fade(strength_start, last_asteroid_go));
					last_asteroid_go = null;
					last_asteroid = null;

					audio.PlayOneShot(mine);
					DeselectMine();
				}
				

				else if (hitInfo.transform.tag == "GameOverClick")	{
					GameObject.Find ("GameOverText").GetComponent<TextMesh>().text="Loading...";
					Application.LoadLevel(Application.loadedLevel);
				}
				else{

					DeselectMine();
				}

				//if ( hitInfo.transform && hitInfo.transform.tag != "AsteroidChild" || hitInfo.transform == null)
				if ( hitInfo.transform == null)	{
					DeselectMine();

				}
				
			}
			else{
				DeselectMine();
				audio.PlayOneShot(noselect);
				Debug.Log ("No Hit");
			}
		}

	}

	void DeselectMine(){
		GameObject.Find ("MineText").renderer.enabled = false;
		GameObject.Find ("MineText").collider.enabled = false;
		GameObject.Find ("MineButton").renderer.enabled = false;
	}

	IEnumerator laser_die(){
		yield return new WaitForSeconds(.3f);
		lineRenderer.enabled = false;

	}

	IEnumerator asteroid_fade(float strength_start, GameObject asteroidmined){
		for (int i = 0; i < 50; i++){
			strength_start=strength_start - .15f;
			asteroidmined.renderer.material.SetFloat("_Strength", strength_start);
			yield return null;
		}

	}

}
