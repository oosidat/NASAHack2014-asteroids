using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	public Transform[] helpPosition;
	public string[] helpText;
	public bool[] displayArrow;
	public GameObject arrow;
	public GameObject helptext;
	public float padding;
	int i = 0;

	// Use this for initialization
	void Start () {
		arrow.renderer.enabled = true;
		helptext.renderer.enabled = true;
		//renderer.enabled = true;
		GameObject.Find("TutorialBG").renderer.enabled= true;

		arrow.transform.position = new Vector3(helpPosition[i].position.x, arrow.transform.position.y , arrow.transform.position.z);
		helptext.GetComponent<TextMesh>().text = helpText[i];
		
		if (displayArrow[i] == true){
			arrow.renderer.enabled = true;
		}
		else {
			arrow.renderer.enabled = false;
		}
		i++;
	}
		
	void Update(){	

		if (Input.GetMouseButtonDown(0))
		{
			if ( i == helpPosition.Length ){
				Destroy(gameObject);  
				return;
			}
			Debug.Log ("Clicking through help screen");
			//helpPosition[i].position.x
			arrow.transform.position = new Vector3(helpPosition[i].position.x, arrow.transform.position.y , arrow.transform.position.z);
			helptext.GetComponent<TextMesh>().text = helpText[i];
				
				if (displayArrow[i] == true){
					arrow.renderer.enabled = true;
				}
				else {
					arrow.renderer.enabled = false;
				}
				i++;
			}
			

	}
}
