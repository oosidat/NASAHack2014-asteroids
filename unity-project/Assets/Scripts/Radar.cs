using UnityEngine;
using System.Collections;

public class Radar : MonoBehaviour 
{
	
	// radar! by oPless from the original javascript by PsychicParrot, 
	// who in turn adapted it from a Blitz3d script found in the
	// public domain online somewhere ....
	//
	
	public Texture blip  ;
	public Texture radarBG ;
	public GameObject radarObject ;
	public Transform centerObject ;

	public Transform playerObject;

	public float mapScale = 0.3f;
	public Vector2 mapCenter = new Vector2(50,50);
	public string tagFilter =  "Asteroid";
	public float maxDist = 200;
	
	/*void OnGUI() 
	{
		
		//	GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, Vector3(Screen.width / 600.0, Screen.height / 450.0, 1));
		
		// Draw player blip (centerObject)
		//		float bX=centerObject.transform.position.x * mapScale;
		//	    float bY=centerObject.transform.position.z * mapScale;
		
		
		GUI.DrawTexture(new Rect(mapCenter.x-32,mapCenter.y-32,64,64),radarBG);
		DrawBlipsFor(tagFilter);
		
	}*/


	void Update(){

		DrawBlipsFor(tagFilter);
	
	}

	
	private void DrawBlipsFor(string tagName)
	{
		
		// Find all game objects with tag 
		GameObject[] gos = GameObject.FindGameObjectsWithTag(tagName); 
		
		// Iterate through them
		foreach (GameObject go in gos)  
		{ 
			drawBlip(go,blip);
		}
	}
	
	private void drawBlip(GameObject go,Texture aTexture)
	{



		Vector3 centerPos=centerObject.position;
		Vector3 extPos=go.transform.position;
		
		// first we need to get the distance of the Asteroid from the Sun
		float dist=Vector3.Distance(centerPos,extPos);
		
		float dx=centerPos.x-extPos.x; // how far to the side of the Sun is the Asteroid?
		float dz=centerPos.z-extPos.z; // how far in front or behind the Sun is the Asteroid?
		
		// what's the angle to turn to face the enemy - compensating for the player's turning?
		float deltay=Mathf.Atan2(dx,dz)*Mathf.Rad2Deg - 270 - centerObject.eulerAngles.y;
		
		// just basic trigonometry to find the point x,y (Asteroid's location) given the angle deltay
		float bX=dist*Mathf.Cos(deltay * Mathf.Deg2Rad);
		float bY=dist*Mathf.Sin(deltay * Mathf.Deg2Rad);
		
		bX=bX*mapScale; // scales down the x-coordinate by half so that the plot stays within our radar
		bY=bY*mapScale; // scales down the y-coordinate by half so that the plot stays within our radar
		
		if(dist<= maxDist)
		{ 
			// this is the diameter of our largest radar circle
			GUI.DrawTexture(new Rect(mapCenter.x+bX,mapCenter.y+bY,2,2),aTexture);
		}
		
	}
	
	
}