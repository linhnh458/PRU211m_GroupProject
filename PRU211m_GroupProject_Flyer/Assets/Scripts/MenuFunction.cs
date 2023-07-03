﻿using System.Collections;
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
        SceneManager.LoadScene(1);
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
    }
}

