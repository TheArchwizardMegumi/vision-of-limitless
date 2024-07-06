using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public PlayerState isOpenEye = PlayerState.Open;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeEyeState();
        }
    }

    private void ChangeEyeState()
    {
        isOpenEye = isOpenEye == PlayerState.Open ? PlayerState.Close : PlayerState.Open;
        Messenger.Broadcast<PlayerState>(MsgType.changeOpenCloseEye, isOpenEye);
    }
}
