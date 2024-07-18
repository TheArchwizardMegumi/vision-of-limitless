using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Restart()
    {
        int currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex", 0);
        Messenger.Broadcast(MsgType.levelStart);
        Time.timeScale = 1;
        string currentLevelName = "Level" + currentLevelIndex.ToString();
        SceneManager.UnloadSceneAsync(currentLevelName).completed += (AsyncOperation op) =>
        {
            SceneManager.LoadSceneAsync(currentLevelName, LoadSceneMode.Additive);
        };
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
