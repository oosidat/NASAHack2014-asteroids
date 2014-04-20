using UnityEngine;
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
				GameObject.Find ("GameOverText").GetComponent<TextMesh>().text="Congratulations!\n\nYou mined $"+score;
				GameObject.Find ("GameOverText").renderer.enabled=true;

				for(int i=0; i<=4; i++)
				{
					//GUI.Box(new Rect(100, 75*i, 150, 50), "Pos "+i+". "+PlayerPrefs.GetInt("highscorePos"+i));
					//GUI.TextArea(new Rect(100, 75*i, 150, 50), "Pos "+i+". "+PlayerPrefs.GetInt("highscorePos"+i));
					//Debug.Log ("Score" + i + " " + PlayerPrefs.GetInt("highscorePos"+i));
					highscores += i+1 + ":  $" + PlayerPrefs.GetInt("highscorePos"+i) + "\n";
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
