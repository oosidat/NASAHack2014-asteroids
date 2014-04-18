using UnityEngine;
using System.Collections;
using System;

public class Movement : MonoBehaviour {
	
	public static Movement Instance;
	public Camera MapCam;
	public float prevx;
	public float prevz;
	private bool isCurrentlyMoving = false;
	private Vector3 moveToPos;
	private float fuelTankCapacity;
	private Controls control;
	private float fuelBurnRate;
	
	void Awake () {
		Instance = this;
		prevx = 0;
		prevz = 0;
		// set the initial position of the camera to match the moveToPos
		moveToPos = Camera.main.transform.position;
		control = GameObject.Find ("Player").GetComponent<Controls>();
		fuelTankCapacity = control.currentFuel;
		fuelBurnRate = control.fuelBurnRate;
		if (fuelBurnRate <= 0 || fuelBurnRate == null) {
			fuelBurnRate = 0.005f;
		}
	}
	
	void LateUpdate() {
		
		//if (Input.mousePosition.x > 230 && Input.mousePosition.y < 230 && Input.GetMouseButton(0))
		if (Input.GetMouseButton (0) && MapCam.pixelRect.Contains (Input.mousePosition)) {
			
			Debug.Log (Input.mousePosition.y + ":" + Input.mousePosition.x);
			int TerrainLayer = 1 << 10;
			
			//RaycastHit hitInfo;
			RaycastHit hitInfo = new RaycastHit ();
			
			if (Physics.Raycast (MapCam.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
				
				print (hitInfo.transform.tag);
				
				Ray ray = MapCam.ScreenPointToRay (Input.mousePosition);
				Physics.Raycast (ray, out hitInfo, Mathf.Infinity, TerrainLayer);
				
				Debug.DrawLine (ray.origin, hitInfo.point);
				Debug.Log ("HitInfo x: " + hitInfo.point.x + " y: " + hitInfo.point.y + " z: " + hitInfo.point.z);
				
				if (hitInfo.transform.tag != "MapDeadZone") {
					
					float currentZ = hitInfo.point.z;
					float currentX = hitInfo.point.x;//+27.0;
					
					double radius = Math.Sqrt (Math.Pow (currentX, 2) + Math.Pow (currentZ, 2));
					double auRad = 2 + (radius - 45) * (3.5 - 2.0) / 145.0;
					String auText = "Distance: " + auRad.ToString ("0.00") + " AU";

					double magDifference = Math.Sqrt(Math.Pow (currentX-prevx,2)+Math.Pow (currentZ-prevz,2));

					GameObject.FindGameObjectWithTag ("distanceLabel").GetComponent<TextMesh> ().text = auText;
					print("mag difference: "+magDifference);

					// set our new position as the end goal for moving the camera
					moveToPos = new Vector3 (hitInfo.point.x, 0.0f, hitInfo.point.z);

					fuelReduction(fuelBurnRate, magDifference, fuelTankCapacity);

					prevx = currentX;
					prevz = currentZ;
				}
			}
		}
		
		if (Camera.main.transform.position != moveToPos) {
			MoveCamera(moveToPos);
			CameraRotation();

		}
	}
	
	void MoveCamera(Vector3 targetPosition) {
		float step = 3.0f * Time.deltaTime;
		Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, targetPosition, step);		
	}

	void fuelReduction(float burnRate, double distance, float startingFuel) {
		float fuelUsed = (float)distance * burnRate;
		control.currentFuel = control.currentFuel - fuelUsed;

		float fuelRatio = control.currentFuel / startingFuel;
		float mulipliedByNumberOfBlocks = 10 * fuelRatio;
		double roundUp = Math.Ceiling (mulipliedByNumberOfBlocks);

		int fuelGaugeBlocks = (int)roundUp;

		print ("StartingFuel: " + startingFuel);
		print ("CurrentFuel: " + control.currentFuel);
		print ("fuelUsed: " + fuelUsed);
		print ("fuelGaugeBlocks: " + fuelGaugeBlocks);

		fuelGage fuelGauge = GameObject.Find ("FuelCell").GetComponent<fuelGage>();
		fuelGauge.changeTexture(fuelGaugeBlocks);

		String fuelText = "Current Charge: "+ string.Format("{0:f1}", control.currentFuel);
		GameObject.FindGameObjectWithTag ("fuelLabel").GetComponent<TextMesh>().text = fuelText;
	}

	void CameraRotation() {
		/* Look at center, then reverse */
		Camera.main.transform.LookAt(new Vector3(0,0,0));
		Camera.main.transform.rotation *= Quaternion.Euler(0, 180, 0);
	}
}
