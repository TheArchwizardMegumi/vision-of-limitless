using System.Collections;
using System.Collections.Generic;
using UnderCloud;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public LevelContainer levelContainer;
    public int currentLevelIndex;

    // void Awake()
    // {
    //     base.Awake();
    // }

    void Start()
    {
        Debug.Log($"Current level: {currentLevelIndex}");
        // 需要加在这里，因为场景会销毁，所以需要重新注册
        Messenger.AddListener(MsgType.playerHurt, ReloadLevel);
        Messenger.AddListener(MsgType.playerWin, LoadNextLevel);

        Messenger.AddListener<PlayerState>(MsgType.changeOpenCloseEye, MapManager.SwitchTransformWallState);
        Messenger.AddListener(MsgType.levelStart, MapManager.InitWhenLevelStart);
        currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex", 0);
    }

    void Update()
    {
        
    }

    public static void LoadLevel(int index)
    {
        if (!SceneManager.GetSceneByName("Player").isLoaded)
        {
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
        Instance.StartCoroutine(LoadSceneCor(Instance.levelContainer.levels[index - 1].name));
        Instance.currentLevelIndex = index-1;
        PlayerPrefs.SetInt("currentLevelIndex", Instance.currentLevelIndex);
    }

    public void UnloadScene(string name)
    {
        Debug.Log($"Unloading {name}");
        Instance.StartCoroutine(UnloadSceneCor(name));
    }

    //将除了通关之外的所有场景加载都改为了这个协程，用于一异步加载完成后触发事件
    private static IEnumerator LoadSceneCor(string name)
    {
        if (SceneManager.GetSceneByName(name).isLoaded)
        {
            SceneManager.UnloadSceneAsync(name);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        yield return operation;
        foreach (SceneAsset scene in Instance.levelContainer.levels)
        {
            if (scene.name == name)
            {
                Messenger.Broadcast(MsgType.levelStart);
                yield break;
            }
        }
    }
    private static IEnumerator UnloadSceneCor(string name)
    {
        yield return null;
        SceneManager.UnloadSceneAsync(name);
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
        SceneManager.UnloadSceneAsync(levelContainer.levels[currentLevelIndex].name);
        currentLevelIndex += 1;
        PlayerPrefs.SetInt("currentLevelIndex", currentLevelIndex);
        if (!SceneManager.GetSceneByName("Player").isLoaded)
        {
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
        StartCoroutine(LoadSceneCor(levelContainer.levels[currentLevelIndex].name));
    }

    private void ReloadLevel()
    {
        if (!SceneManager.GetSceneByName("Player").isLoaded)
        {
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
        StartCoroutine(LoadSceneCor(levelContainer.levels[currentLevelIndex].name));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
