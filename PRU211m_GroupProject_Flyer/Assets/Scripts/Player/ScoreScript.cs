using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour
{
    public static int score;
    private int displayScore;
    [SerializeField] Text scoreText;
    [SerializeField] Text scoreTextGO;

    private void Update()
    {
        if (score != displayScore)
        {
            displayScore = score;
            scoreText.text = "Score: " + displayScore.ToString();
            scoreTextGO.text = "Score: " + displayScore.ToString();
        }
        else
        {
            scoreText.text = "Score: 0";
            scoreTextGO.text = "Score: 0";
        }
    }
    public void AddScore(int scoreToAdd)
    {
        score+= scoreToAdd;
    }
}
