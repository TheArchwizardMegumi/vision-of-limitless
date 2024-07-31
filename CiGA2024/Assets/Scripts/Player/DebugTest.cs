using System.Collections;
using System.Collections.Generic;
using UnderCloud;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            Messenger.Broadcast(MsgType.playerHurt);
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            Messenger.Broadcast(MsgType.reachExit);
        }
    }
}
