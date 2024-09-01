using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public int score;
	public int highScore;
	public Text textScore;
	public Text textScoreFinal;
	public Text textHighScore;
	public Level level;
	public GameObject bestScore;
	public Text textHighScoreShop;
	public int rand;
	void Start()
	{
		highScore = PlayerPrefs.GetInt("HighScore");
		textScore.text = 0 + "";
		textHighScore.text = "" + highScore;
		textHighScoreShop.text = "HIGHSCORE : " + highScore;
	}

	public void resetScore()
	{
		if (score > highScore)
		{
			highScore = score;
			PlayerPrefs.SetInt("HighScore", highScore);
		}
		textScoreFinal.text = "" + score;
		textHighScore.text = "" + highScore;
		textHighScoreShop.text = "HIGHSCORE : " + highScore;
		if (score >= 10)
		{
			rand = 1;
		}
		else
		rand = 0;
		score = 0;
		textScore.text = "" + 0;
		bestScore.SetActive(true);
		GetComponent<Text>().color = new Vector4(1f, 1f, 1f, 0f);
	}
	public void addScore(int value)
	{
		score += value;
		textScore.text = "" + score;
	}
}
