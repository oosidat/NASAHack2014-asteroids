using UnityEngine;
using System.Collections;

public class Laser_anim : MonoBehaviour {
	Color c1 = Color.yellow;
	Color c2 = Color.red;
	LineRenderer lineRenderer;

	public Transform origin;
	public Transform destination;

	private float counter;
	private float dist;

	public float lineDrawSpeed = 6f;

	// Use this for initialization
	void Start () {

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material (Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(.2f,.2f);
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, origin.position);
		dist = Vector3.Distance(origin.position, destination.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (counter < dist){
			counter += .1f / lineDrawSpeed;
			float x = Mathf.Lerp(0, dist, counter);

			Vector3 pointA = origin.position;
			Vector3 pointB = destination.position;

			//Get the unit vector in the desired direction, multiply by desired length and add the starting point.
			Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

			lineRenderer.SetPosition(1, pointAlongLine);

		}
	
	}
}
