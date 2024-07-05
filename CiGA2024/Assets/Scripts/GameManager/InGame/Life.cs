using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    void Start()
    {
        Messenger.AddListener<int>(MsgType.ChangeHealth, ChangeHealth);
    }

    void ChangeHealth(int value)
    {
        Text text = GetComponent<Text>();
        int newValue = int.Parse(text.text) + value;
        if (newValue >= 0)
        {
            text.text = (newValue).ToString();
        }
        else
        {
            Messenger.Broadcast(MsgType.GameOver);
        }
    }
}
