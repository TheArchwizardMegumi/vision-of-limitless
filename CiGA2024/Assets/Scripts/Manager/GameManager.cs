using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public LevelContainer levelContainer;
    int currentLevelIndex = 0;

    // void Awake()
    // {
    //     base.Awake();
    // }

    void Start()
    {
        // 需要加在这里，因为场景会销毁，所以需要重新注册
        Messenger.AddListener(MsgType.playerHurt, ReloadLevel);
        Messenger.AddListener(MsgType.playerWin, LoadNextLevel); 
    }

    void Update()
    {
        
    }

    public void LoadLevel()
    {
        // int index;
        // currentLevelIndex = index; 
    }

    private void LoadNextLevel()
    {
        if (currentLevelIndex >= levelContainer.levels.Length - 1)
        {
            Debug.Log("No more levels");
            return;
        }
        currentLevelIndex += 1;
        SceneManager.LoadSceneAsync(levelContainer.levels[currentLevelIndex].name);
    }

    private void ReloadLevel()
    {
        Debug.Log(levelContainer.levels[currentLevelIndex]);
        SceneManager.LoadSceneAsync(levelContainer.levels[currentLevelIndex].name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
