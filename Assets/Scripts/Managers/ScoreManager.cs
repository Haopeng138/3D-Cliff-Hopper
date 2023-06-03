using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : Singleton<ScoreManager>
{
    public Text scoreText;
    public Text highScoreText;

    int score = 0;
    int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("highScore",0);
        Debug.Log("HighScore: " + highScore);
        scoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + highScore;
    }

    
    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateHighScore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
            highScoreText.text = "HighScore: " + highScore;
            PlayerPrefs.SetInt("highScore",highScore);
        }
    }
    public int GetHighScore()
    {
        return highScore;
    }


}
