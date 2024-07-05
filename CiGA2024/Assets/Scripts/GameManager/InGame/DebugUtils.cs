using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtils : MonoBehaviour
{
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ChangeHealth(int value)
    {
        Messenger.Broadcast<int>(MsgType.ChangeHealth, value);
    }

    public void ChangeGold(int value)
    {
        Messenger.Broadcast<int>(MsgType.ChangeGold, value);
    }

    public void ChangeWaves(int value)
    {
        Messenger.Broadcast<int>(MsgType.ChangeWaves, value);
    }

    public void GameOver()
    {
        Messenger.Broadcast(MsgType.GameOver);
    }

    public void GameWin()
    {
        Messenger.Broadcast(MsgType.GameWin);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}
