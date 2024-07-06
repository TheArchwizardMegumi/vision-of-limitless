using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Sington<GameManager>
{
    void Awake()
    {
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
        // TODO: Can only reload once
        Debug.Log("Reload Level");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
