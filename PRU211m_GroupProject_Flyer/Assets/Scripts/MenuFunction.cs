using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MenuFunction : MonoBehaviour
{
    [SerializeField] bool isPaused = false;
    private PipeMove[] pipes;
    [SerializeField] GameObject pauseMenu;

    public Slider volumeSlider;
    public AudioMixer audioMixer;

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
        }
    }
    public void SetVolume()
    {
        audioMixer.SetFloat("volume", volumeSlider.value);
    }

    public void StartNewGame()
    {
        Time.timeScale = 1f;
        if (pipes != null)
        {
            foreach (var pipe in pipes)
            {
                pipe.Resume();
                pauseMenu.SetActive(false);
            }
        }
        AmmoText.ammoAmount = 0;
        PlayerScript.isGameOver = false;
        ResetGame();
        SceneManager.LoadScene(1);
    }

    // delete playerref data, reset all data
    void ResetGame()
    {
        PlayerPrefs.DeleteKey("Score");
        HealthManager.currentHeart = 2; // reset initial hearts
    }

    public void ExitMain()
    {
        Application.Quit();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Tạm dừng thời gian trong game
            // Hiển thị giao diện hoặc thực hiện các hành động liên quan khi game tạm dừng

            // Tạm dừng hoạt động của tất cả các đối tượng PipeMove
            pipes = FindObjectsOfType<PipeMove>();
            foreach (var pipe in pipes)
            {
                pipe.Pause();
                pauseMenu.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1f; // Tiếp tục thời gian trong game
            // Ẩn giao diện hoặc thực hiện các hành động liên quan khi game tiếp tục

            // Tiếp tục hoạt động của tất cả các đối tượng PipeMove
            if (pipes != null)
            {
                foreach (var pipe in pipes)
                {
                    pipe.Resume();
                    pauseMenu.SetActive(false);
                }
            }
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("Score", ScoreScript.score);
        PlayerPrefs.SetInt("Health", HealthManager.currentHeart);
    }

    public void ResumeButtonClicked()
    {
        Time.timeScale = 1f;
        LoadDataFromPlayerPrefs();
        SceneManager.LoadScene(1); // resume main game scene
    }

    // get data from playerpref
    public void LoadDataFromPlayerPrefs()
    {
        ScoreScript.score = PlayerPrefs.GetInt("Score");
    }
}

