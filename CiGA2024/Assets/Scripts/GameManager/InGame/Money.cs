using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    void Start()
    {
        Messenger.AddListener<int>(MsgType.ChangeGold, ChangeGold);
    }

    void ChangeGold(int value)
    {
        Text text = GetComponent<Text>();
        text.text = (int.Parse(text.text) + value).ToString();
    }
}
