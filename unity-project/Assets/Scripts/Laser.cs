using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	Color c1 = Color.yellow;
	Color c2 = Color.red;
	LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material (Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(.2f,.2f);
		lineRenderer.SetVertexCount(2);

	}
	
	// Update is called once per frame
	void Update () {
		lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z));
		lineRenderer.SetPosition(1, hitInfo.point);
		lineRenderer.SetPosition(2, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z));
	}
}
