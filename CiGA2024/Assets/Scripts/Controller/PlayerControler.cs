using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    PlayerState isOpenEye;
    private void Awake()
    {
        GlobalData.playerControler = this;
        Messenger.AddListener<float>(MsgType.playerHert, OnPlayerHert);
    }

    private void OnPlayerHert(float arg1)
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
    
    public void ChangeEyeType()
    {
        Messenger.Broadcast<PlayerState>(MsgType.changeOpenCloseEye, isOpenEye);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener<float>(MsgType.playerHert, OnPlayerHert);
    }
}
