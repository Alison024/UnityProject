using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        IsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        IsPaused = true;
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuiteGame()
    {
        Application.Quit();
    }
}
