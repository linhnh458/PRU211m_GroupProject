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

    void Start()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            score = 0;
        }
        displayScore = 0;
    }

    private void Update()
    {
        if (score != displayScore)
        {
            displayScore = score;
            scoreTextDisplay.text = "Score: " + displayScore.ToString();
            scoreTextGO.text = "Score: " + displayScore.ToString();
        }
        else
        {
            scoreTextDisplay.text = "Score: 0";
            scoreTextGO.text = "Score: 0";
        }
    }
    public void AddScore(int scoreToAdd)
    {
        score+= scoreToAdd;
    }
}
