using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	public Transform[] helpPosition;
	public string[] helpText;
	public bool[] displayArrow;
	public float padding;
	int i = 0;

	// Use this for initialization
	void Start () {
	
	}
		
	void update(){
		
		if (Input.GetMouseButtonDown(0)) {
			
				transform.position = new Vector3(helpPosition[i].position.x, helpPosition[i].position.y + padding, helpPosition[i].position.z);
				GetComponent<TextMesh>().text = helpText[i];
				
				if (displayArrow[i] = true){
					GameObject.Find("DisplayArrow").renderer.enabled = true;
				}
				else {
					GameObject.Find("DisplayArrow").renderer.enabled = false;
				}
				i++;
			}
			
			if ( i >= helpPosition.Length ){
				Destroy(transform.parent.gameObject);     
			}
	}
}
