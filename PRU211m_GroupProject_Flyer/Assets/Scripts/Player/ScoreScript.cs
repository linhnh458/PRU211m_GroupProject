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

    private void Update()
    {
        if (score != displayScore)
        {
            displayScore = score;
            scoreText.text = "Score: " + displayScore.ToString();
        }
    }
    public void AddScore(int scoreToAdd)
    {
        score+= scoreToAdd;
    }
}
