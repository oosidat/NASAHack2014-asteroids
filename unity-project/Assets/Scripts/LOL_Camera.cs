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
		if ( Input.GetMouseButtonUp(0) && MapCam.pixelRect.Contains(Input.mousePosition))
		{

			Debug.Log (Input.mousePosition.y + ":" + Input.mousePosition.x);
			int TerrainLayer = 1 << 10;
			//RaycastHit hitInfo;
			RaycastHit hitInfo = new RaycastHit();

			if (Physics.Raycast(MapCam.ScreenPointToRay(Input.mousePosition), out hitInfo))
			{
				print (hitInfo.transform.tag);
			


			Ray ray = MapCam.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(ray, out hitInfo, Mathf.Infinity, TerrainLayer);
			Debug.DrawLine(ray.origin, hitInfo.point);
			Debug.Log("HitInfo x: " + hitInfo.point.x + " y: " + hitInfo.point.y + " z: " + hitInfo.point.z);
			



			//StartCoroutine(	MovePlayer(new Vector3(hitInfo.point.x,1,hitInfo.point.z)));
			//Debug.Log(hitInfo.transform.tag);

				if (hitInfo.transform.tag != "MapDeadZone"){
					Camera.main.transform.position = new Vector3(hitInfo.point.x,1,hitInfo.point.z);
				
					//print(Mathf.Clamp(10, 1, 3));
				}
			}
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


	/*
        if (player.hud.MiniMap.IsInViewport(Input.mousePosition))
        {
            RaycastHit hit;
            Ray ray = player.hud.MiniMap.minimapCamera.ScreenPointToRay(Input.mousePosition);
 
            // Trace ray from minimap viewport, ignoring everything except the ground
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, CustomLayerMask.Ground))
            {
                Vector3 miniMapPosition = hit.point;
                Vector3 camViewCenter;
                RaycastHit cameraView;
                Vector3 camDestPos;
 
                // Project a ray from the center of the main camera to find current world position
                Ray cameraCenter = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
                Physics.Raycast(cameraCenter, out cameraView, Mathf.Infinity, CustomLayerMask.Ground);
                camViewCenter = cameraView.point;
 
                // Calculate change to move from current position to new minimap location                    
                camDestPos = miniMapPosition - camViewCenter;
                camDestPos.y = 0;           // maintain current height
 
                Debug.Log("Camera: " + Camera.main.transform.position);
                Debug.Log("Add to: " + camDestPos);
                Camera.main.transform.position += camDestPos;
            }                
        }
	 */


	
}