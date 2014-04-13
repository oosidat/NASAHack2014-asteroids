using UnityEngine;
using System.Collections;

public class LOL_Camera : MonoBehaviour {
	
	public static LOL_Camera Instance;
	public Camera MapCam;
	void Awake () 
	{
		Instance = this;
	}

	void LateUpdate()
	{
		
		//if (Input.mousePosition.x > 230 && Input.mousePosition.y < 230 && Input.GetMouseButton(0))
		if ( Input.GetMouseButton(0))
		{
			//int TerrainLayer = 1 << 8;
			//RaycastHit hitInfo;
			RaycastHit hitInfo = new RaycastHit();

			if (Physics.Raycast(MapCam.ScreenPointToRay(Input.mousePosition), out hitInfo))
			{
				print (hitInfo.transform.tag);
			}


			Ray ray = MapCam.ScreenPointToRay(Input.mousePosition);
			//Physics.Raycast(ray, out hitInfo, Mathf.Infinity, TerrainLayer);
			Debug.DrawLine(ray.origin, hitInfo.point);
			Debug.Log("HitInfo x: " + hitInfo.point.x + " y: " + hitInfo.point.y + " z: " + hitInfo.point.z);
			StartCoroutine(	MovePlayer(new Vector3(hitInfo.point.x,1,hitInfo.point.z)));
			//Debug.Log(hitInfo.transform.tag);

		}
		
		
	}

	IEnumerator MovePlayer(Vector3 targ){
		Debug.Log ("moving");
		float t = 3;
		while (t > 0) {
						float step = .1f * Time.deltaTime;
						Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, targ, step);
						t = t - step;
						yield return null;
				}
	}



	
}