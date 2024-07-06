using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Sington<GameManager>
{
    void Awake()
    {
        DontDestroyOnLoad(this);
        Messenger.AddListener(MsgType.playerHurt, ReloadLevel);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ReloadLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
