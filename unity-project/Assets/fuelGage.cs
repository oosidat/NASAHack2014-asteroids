﻿using UnityEngine;
using System.Collections;

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
				GameObject.Find ("GameOverText").GetComponent<TextMesh>().text="Congratulations!\n\nYou mined $"+score.ToString("0.00");;
				GameObject.Find ("GameOverText").renderer.enabled=true;

				for(int i=0; i<=4; i++)
				{
					//GUI.Box(new Rect(100, 75*i, 150, 50), "Pos "+i+". "+PlayerPrefs.GetInt("highscorePos"+i));
					//GUI.TextArea(new Rect(100, 75*i, 150, 50), "Pos "+i+". "+PlayerPrefs.GetInt("highscorePos"+i));
					//Debug.Log ("Score" + i + " " + PlayerPrefs.GetInt("highscorePos"+i));
					highscores += i + ":  $" + PlayerPrefs.GetInt("highscorePos"+i) + "\n";
				}

				GameObject.Find ("HighScoreText").GetComponent<TextMesh>().text="High Scores:\n" + highscores;
				GameObject.Find ("HighScoreText").renderer.enabled=true;
				gameover = 1;	
			}
		}
		renderer.material.mainTexture = images[value];


	}

	void HighScoreSet ()
	{

		/*for(int i=1; i<=5; i++) //for top 5 highscores
		{
			if(PlayerPrefs.GetInt("highscorePos"+i)<score) //if cuurent score is in top 5
			{

				temp=PlayerPrefs.GetInt("highscorePos"+i); //store the old highscore in temp varible to shift it down
				PlayerPrefs.SetInt("highscorePos"+i,score); //store the currentscore to highscores
				if(i<5) //do this for shifting scores down
				{
					int j=i+1;
					PlayerPrefs.SetInt("highscorePos"+j,temp);
				}
			}
		}*/
	
	//Used to force blank high scores
	//	for (int i=0; i<4; i++) {
	//		PlayerPrefs.SetInt("highscorePos"+i,0);
	//	}


		int newscore = score;
		for (int i=0; i<4; i++) {
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
				///newName = "";
			}
		}

	}

	/*void OnGUI()
	{
		if(levelComplete)
		{
			for(int i=1; i<=5; i++)
			{
				//GUI.Box(new Rect(100, 75*i, 150, 50), "Pos "+i+". "+PlayerPrefs.GetInt("highscorePos"+i));
				GUI.TextArea(new Rect(100, 75*i, 150, 50), "Pos "+i+". "+PlayerPrefs.GetInt("highscorePos"+i));
			}
		}
	}*/



}
