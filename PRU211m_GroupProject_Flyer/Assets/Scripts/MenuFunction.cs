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
    [SerializeField] GameObject soundSettingsMenu;
    [SerializeField] GameObject ammoText;

    [SerializeField] GameObject resumeButton;

    void Start()
    {
        resumeButton.SetActive(false);
        // enable resume button only when playerrefs data is available
        if (PlayerPrefs.HasKey("Score"))
        {
            resumeButton.SetActive(true);
        }
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
        PlayerScript.isGameOver = false;
        ResetGame();
        SceneManager.LoadScene(1);
    }

    // delete playerref data, reset all data
    void ResetGame()
    {
        PlayerPrefs.DeleteKey("Score");
        HealthManager.currentHeart = 2;
        AmmoText.ammoAmount = 5;
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

    public void SoundSettings()
    {
        Time.timeScale = 0f; // Tạm dừng thời gian trong game
                             // Hiển thị giao diện hoặc thực hiện các hành động liên quan khi game tạm dừng

        // Tạm dừng hoạt động của tất cả các đối tượng PipeMove
        pipes = FindObjectsOfType<PipeMove>();
        foreach (var pipe in pipes)
        {
            pipe.Pause();
            pauseMenu.SetActive(false);
        }
        soundSettingsMenu.SetActive(true);
        ammoText.SetActive(false);
    }

    public void ExitSoundSettings()
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
        soundSettingsMenu.SetActive(false);
    }

    public void GoToMenu()
    {
        AudioManager.Instance.sfxSource.Stop();
        SceneManager.LoadScene(0);
        if(isPaused == true)
        {
            PlayerPrefs.SetInt("Score", ScoreScript.score);
            PlayerPrefs.SetInt("Health", HealthManager.currentHeart);
            PlayerPrefs.SetInt("Ammo", AmmoText.ammoAmount);
        }
        else if(isPaused == false)
        {
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("Health");
            PlayerPrefs.DeleteKey("Ammo");
        }
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
        AmmoText.ammoAmount = PlayerPrefs.GetInt("Ammo");
    }
}

