using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	Color c1= Color.yellow;
	Color c2 = Color.red;
	int lengthOfLineRenderer = 200;
	// Use this for initialization
	void Start () {
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material (Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.2f,0.2f);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
		lineRenderer.SetPosition(0, transform.forward);

	}
	
	// Update is called once per frame
	void Update () {
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		for(int i = 1; i < lengthOfLineRenderer; i++) {
			Vector3 pos = new Vector3(i * 0.5f, 0, Mathf.Sin(i - Time.time * 10));
			lineRenderer.SetPosition(i, pos);
		}
	
	}


}
