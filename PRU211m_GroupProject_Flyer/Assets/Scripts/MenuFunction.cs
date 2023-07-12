using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFunction : MonoBehaviour
{
    [SerializeField] bool isPaused = false;
    private PipeMove[] pipes;
    [SerializeField] GameObject pauseMenu;
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
        //PlayerPrefs.SetFloat("PlayerPositionX", GunScript.playerPosition.x);
        //PlayerPrefs.SetFloat("PlayerPositionY", GunScript.playerPosition.y);
        //PlayerPrefs.SetFloat("PlayerPositionZ", GunScript.playerPosition.z);

        Debug.Log("Saved score: " + ScoreScript.score);
    }
}

