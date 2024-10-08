using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        GlobalData.playerController = this;
        Messenger.AddListener<float>(MsgType.playerHurt, OnPlayerHurt);
    }

    private void OnPlayerHurt(float arg1)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<float>(MsgType.playerHurt, OnPlayerHurt);
    }
}
