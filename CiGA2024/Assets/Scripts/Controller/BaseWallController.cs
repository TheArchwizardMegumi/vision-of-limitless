using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWallController : MonoBehaviour
{
    private void Awake()
    {
        Messenger.AddListener<bool>(MsgType.changeOpenCloseEye, OnchangeOpenCloseEye);
    }

    protected virtual void OnchangeOpenCloseEye(bool arg1)
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
        Messenger.RemoveListener<bool>(MsgType.changeOpenCloseEye, OnchangeOpenCloseEye);
    }
}
