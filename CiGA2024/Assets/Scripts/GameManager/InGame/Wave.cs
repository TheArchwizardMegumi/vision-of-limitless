using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wave : MonoBehaviour
{
    void Start()
    {
        Messenger.AddListener<int>(MsgType.ChangeWaves, ChangeWaves);
    }

    void ChangeWaves(int value)
    {
        Text text = GetComponent<Text>();
        

        if (int.Parse(text.text) >= 5) // 5 is the number of waves (temp)
        {
            Messenger.Broadcast(MsgType.GameWin);
        }
        else
        {
            text.text = (int.Parse(text.text) + value).ToString();
            // Spawn enemies
        }
    }
}
