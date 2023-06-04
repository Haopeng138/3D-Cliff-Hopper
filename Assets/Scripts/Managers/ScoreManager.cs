using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : Singleton<ScoreManager>
{
    public Text scoreText;
    public Text highScoreText;

    public int score = 0;
    public int highScore = 0;
    public int taps = 0;
    public int coins = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
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

    public void addTap(){
        taps++;
        this.addScore(1);
    }

    public void addCoin(){
        coins++;
        this.addScore(5);
    }


    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            highScoreText.text = "HighScore: " + highScore;
            PlayerPrefs.SetInt("highScore",highScore);
        }
    }
    public int GetHighScore()
    {
        return highScore;
    }


}
