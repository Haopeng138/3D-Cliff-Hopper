using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI tapsText;
    [SerializeField] TextMeshProUGUI coinsText;

    private ScoreManager scoreManager;
    void Start(){
        scoreManager = ScoreManager.Instance;
        scoreText.text = "Score: " + scoreManager.score;
        highScoreText.text = "HighScore: " + scoreManager.highScore;
        tapsText.text = "Taps: " + scoreManager.taps;
        coinsText.text = "Coins: " + scoreManager.coins;
    }

    void Update(){
        scoreText.text = "Score: " + scoreManager.score;
        highScoreText.text = "HighScore: " + scoreManager.highScore;
        tapsText.text = "Taps: " + scoreManager.taps;
        coinsText.text = "Coins: " + scoreManager.coins;
    }
}
