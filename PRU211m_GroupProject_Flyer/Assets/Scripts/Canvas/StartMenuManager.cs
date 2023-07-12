using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    private Button resumeButton;

    private void Awake()
    {
        // get the resume button, disable when game first starts
        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton").GetComponent<Button>();
        resumeButton.enabled = false;
    }
    void Start()
    {
        // enable resume button only when playerrefs data is available
        if (PlayerPrefs.HasKey("Score"))
        {
            resumeButton.enabled = true;
            resumeButton.onClick.AddListener(ResumeButtonClicked);
        }
    }

    void ResumeButtonClicked()
    {
        LoadDataFromPlayerPrefs();
        SceneManager.LoadScene(1); // resume main game scene
    }

    // get data from playerpref
    public void LoadDataFromPlayerPrefs()
    {
        ScoreScript.score = PlayerPrefs.GetInt("Score");
    }
}
