using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunction : MonoBehaviour
{
   public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitMain()
    {
        Application.Quit();
    }
}
