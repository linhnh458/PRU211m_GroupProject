using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    public static int score;
    private int displayScore;
    [SerializeField] Text scoreTextDisplay;
    [SerializeField] Text scoreTextGO;
    [SerializeField] Text highestScoreDisplay;

    public static int highestScore;

    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        if (PlayerPrefs.HasKey("highestScore"))
        {
            highestScore = PlayerPrefs.GetInt("highestScore");
            Debug.Log("highest score: " + highestScore);
        }
        displayScore = 0;
        scoreTextDisplay.text = "Score: " + displayScore.ToString();
        scoreTextGO.text = "Score: " + displayScore.ToString();
    }

    private void Update()
    {
        if (score != displayScore)
        {
            displayScore = score;
            scoreTextDisplay.text = "Score: " + displayScore.ToString();
            scoreTextGO.text = "Score: " + displayScore.ToString();
        }
        if (score > highestScore)
        {
            highestScore = score;
        }
        highestScoreDisplay.text = "Highest score: " + highestScore.ToString();
    }
    public void AddScore(int scoreToAdd)
    {
        score+= scoreToAdd;
    }
}
