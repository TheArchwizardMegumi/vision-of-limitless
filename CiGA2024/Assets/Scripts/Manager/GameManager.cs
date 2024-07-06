using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public LevelContainer levelContainer;
    int currentLevelIndex;

    // void Awake()
    // {
    //     base.Awake();
    // }

    void Start()
    {
        // 需要加在这里，因为场景会销毁，所以需要重新注册
        Messenger.AddListener(MsgType.playerHurt, ReloadLevel);
        Messenger.AddListener(MsgType.playerWin, LoadNextLevel);
        currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex", 0);
    }

    void Update()
    {
        
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadSceneAsync(levelContainer.levels[index-1].name);
        currentLevelIndex = index-1;
        PlayerPrefs.SetInt("currentLevelIndex", currentLevelIndex);
    }

    private void LoadNextLevel()
    {
        Debug.Log(currentLevelIndex + " " + (levelContainer.levels.Length - 1));
        if (currentLevelIndex >= levelContainer.levels.Length - 1)
        {
            Debug.Log("No more levels");
            SceneManager.LoadSceneAsync("Win");
            return;
        }
        currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex", 0);
        currentLevelIndex += 1;
        PlayerPrefs.SetInt("currentLevelIndex", currentLevelIndex);
        SceneManager.LoadSceneAsync(levelContainer.levels[currentLevelIndex].name);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadSceneAsync(levelContainer.levels[currentLevelIndex].name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
