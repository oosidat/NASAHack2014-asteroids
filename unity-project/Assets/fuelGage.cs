using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class fuelGage : MonoBehaviour {
	public Texture2D[] images;
	public string highscorePos;
	public string highscores;
	public int score;
	public int temp;
	int gameover;
	// Use this for initialization
	void Start () {
		gameover=0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeTexture(int value)
	{

		if (value <= 0) {
			value = 0;
			if (gameover == 0){
				Controls controls = GameObject.Find ("Player").GetComponent<Controls>();
				score = Mathf.CeilToInt(controls.currentMoney);
				HighScoreSet();
				Debug.Log("score: " + score);
				GameObject.Find ("GameOver").renderer.enabled=true;
				GameObject.Find ("GameOver").collider.enabled=true;

				var mostCommonValue = controls.minedElements.GroupBy(v => v)
					.OrderByDescending(g => g.Count())
						.Select(g => g.Key)
						.FirstOrDefault();

				GameObject.Find ("GameOverText").GetComponent<TextMesh>().text="Congratulations!\n\nYou earned $"+score;
				GameObject.Find ("GameOverText").renderer.enabled=true;

				controls.minedElements.Clear();

				for(int i=0; i<5; i++)
				{
					//GUI.Box(new Rect(100, 75*i, 150, 50), "Pos "+i+". "+PlayerPrefs.GetInt("highscorePos"+i));
					//GUI.TextArea(new Rect(100, 75*i, 150, 50), "Pos "+i+". "+PlayerPrefs.GetInt("highscorePos"+i));
					//Debug.Log ("Score" + i + " " + PlayerPrefs.GetInt("highscorePos"+i));
					highscores += i+1 + ":  $" + PlayerPrefs.GetInt("highscorePos"+i) + "\n";
				}
				string commonresource="";
				if (mostCommonValue != null){	
					commonresource="Most common resource mined was "+mostCommonValue;
				}

				GameObject.Find ("HighScoreText").GetComponent<TextMesh>().text=commonresource+"\n\nHigh Scores\n" + highscores;
				GameObject.Find ("HighScoreText").renderer.enabled=true;
				gameover = 1;	
			}
		}
		renderer.material.mainTexture = images[value];


	}

	void HighScoreSet ()
	{
		//Blank Scores
		//for (int i=0; i<5; i++) {
		//		PlayerPrefs.SetInt("highscorePos"+i,0);
		//}



		int newscore = score;
		for (int i=0; i<5; i++) {
			if (PlayerPrefs.HasKey("highscorePos"+i)) {
				if (PlayerPrefs.GetInt("highscorePos"+i) < newscore) {
					// new Score is higher than the stored score
					temp= PlayerPrefs.GetInt("highscorePos"+i);
					PlayerPrefs.SetInt("highscorePos"+i,newscore);
					newscore = temp;

				}
			}
			else {
				PlayerPrefs.SetInt("highscorePos"+i,score);
				newscore = 0;
				return;
				///newName = "";
			}
		}

	}
}
