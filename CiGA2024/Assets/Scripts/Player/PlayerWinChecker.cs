using OvO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerWinChecker
{
    public static int playerNum = 2;
    private static readonly bool[] playerStandingOnExit = new bool[playerNum];
    public static void Init()
    {
        for (int i = 0; i < playerNum; i++)
        {
            playerStandingOnExit[i] = false;
        }
    }
    public static void ReachExit(int playerNumer, Vector2Int position)
    {
        PlayWinEffect(position);
        playerStandingOnExit[playerNumer] = true;
        for (int i = 0; i < playerNum; i++)
        {
            if (!playerStandingOnExit[i])
            {
                return;
            }
        }
        Timer.CreateTimer("DelayEndLevel", true, (1f, EndLevel));
    }

    private static void EndLevel()
    {
        Init();
        Messenger.Broadcast(MsgType.reachExit);
        Timer.RemoveTimer(Timer.GetTimers("DelayEndLevel").First());
    }

    public static void PlayWinEffect(Vector2Int position)
    {
        GameObject effect = (GameObject)Object.Instantiate(Resources.Load("Prefabs/WinEffect"));
        effect.transform.position = new Vector3(position.x, position.y, 0);
    }
}
