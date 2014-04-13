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
			int TerrainLayer = 1 << 8;
			//RaycastHit hitInfo;
			RaycastHit hitInfo = new RaycastHit();
			Ray ray = MapCam.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(ray, out hitInfo, Mathf.Infinity, TerrainLayer);
			Debug.DrawLine(ray.origin, hitInfo.point);
			Debug.Log("HitInfo x: " + hitInfo.point.x + " y: " + hitInfo.point.y + " z: " + hitInfo.point.z);
			//Debug.Log(hitInfo.transform.tag);
		}
		
		
	}
	
	
}