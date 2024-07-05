using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary : MonoBehaviour
{
    public GameObject gameOverLabel;
    public GameObject gameWinLabel;
    void Start()
    {
        Messenger.AddListener(MsgType.GameOver, GameOver);
        Messenger.AddListener(MsgType.GameWin, GameWin);
    }

    void GameOver()
    {
        gameOverLabel.SetActive(true);
    }

    void GameWin()
    {
        gameWinLabel.SetActive(true);
    }
}
