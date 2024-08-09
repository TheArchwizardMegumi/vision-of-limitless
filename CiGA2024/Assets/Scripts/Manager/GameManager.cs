using System.Collections;
using System.Collections.Generic;
using UnderCloud;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static string[] levelName = new string[25]
    {
        "Level0",
        "Level1",
        "Level2",
        "Level3",
        "Level4",
        "Level5",
        "Level6",
        "Level7",
        "Level8",
        "Level9",
        "Level10",
        "Level11",
        "Level12",
        "Level13",
        "Level14",
        "Level15",
        "Level16",
        "Level17",
        "Level18",
        "Level19",
        "Level20",
        "Level21",
        "Level22",
        "Level23",
        "Level24",
    };
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
        Messenger.AddListener<PlayerState>(MsgType.changeOpenCloseEye, MapManager.CountTimeLimitedWall);
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
        Instance.StartCoroutine(LoadSceneCor(levelName[index]));
        Instance.currentLevelIndex = index;
        PlayerPrefs.SetInt("currentLevelIndex", Instance.currentLevelIndex);
        if (SceneManager.GetSceneByName("MainMenu").isLoaded)
        {
            SceneManager.UnloadSceneAsync("MainMenu");
        }
    }
    private static IEnumerator LoadSceneCor(string name)
    {
        if (SceneManager.GetSceneByName(name).isLoaded)
        {
            SceneManager.UnloadSceneAsync(name);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        yield return operation;
        foreach (string sceneName in levelName)
        {
            if (sceneName == name)
            {
                Messenger.Broadcast(MsgType.levelStart);
                yield break;
            }
        }
    }
    public void UnloadScene(string name)
    {
        Debug.Log($"Unloading {name}");
        Instance.StartCoroutine(UnloadSceneCor(name));
    }

    //将除了通关之外的所有场景加载都改为了这个协程，用于一异步加载完成后触发事件

    private static IEnumerator UnloadSceneCor(string name)
    {
        yield return null;
        SceneManager.UnloadSceneAsync(name);
    }

    private void LoadNextLevel()
    {
        Debug.Log(currentLevelIndex + " " + (levelName.Length - 1));
        if (currentLevelIndex >= levelName.Length - 1)
        {
            Debug.Log("No more levels");
            SceneManager.LoadSceneAsync("Win", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(levelName[currentLevelIndex]);
            return;
        }
        currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex", 0);
        SceneManager.UnloadSceneAsync(levelName[currentLevelIndex]);
        currentLevelIndex += 1;
        PlayerPrefs.SetInt("currentLevelIndex", currentLevelIndex);
        if (!SceneManager.GetSceneByName("Player").isLoaded)
        {
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
        StartCoroutine(LoadSceneCor(levelName[currentLevelIndex]));
    }

    private void ReloadLevel()
    {
        if (!SceneManager.GetSceneByName("Player").isLoaded)
        {
            SceneManager.LoadScene("Player", LoadSceneMode.Additive);
        }
        StartCoroutine(LoadSceneCor(levelName[currentLevelIndex]));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}