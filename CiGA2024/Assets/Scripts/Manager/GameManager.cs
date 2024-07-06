using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Sington<GameManager>
{
    void Awake()
    {
        
    }

    void Start()
    {
        // 需要加在这里，因为场景会销毁，所以需要重新注册
        Messenger.AddListener(MsgType.playerHurt, ReloadLevel);
        Messenger.AddListener(MsgType.playerWin, LoadNextLevel); 
    }

    void Update()
    {
        
    }

    private void LoadNextLevel()
    {
        int level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
