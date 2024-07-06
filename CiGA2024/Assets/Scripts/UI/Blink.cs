using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public PlayerState isOpenEye = PlayerState.Open;
    public float blinkTime = 0.5f;

    bool isBlinking = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blinking());
            ChangeEyeState();
        }
    }

    private void ChangeEyeState()
    {
        isOpenEye = isOpenEye == PlayerState.Open ? PlayerState.Close : PlayerState.Open;
        Messenger.Broadcast<PlayerState>(MsgType.changeOpenCloseEye, isOpenEye);
    }

    IEnumerator Blinking()
    {
        yield return new WaitForSeconds(blinkTime);
        isBlinking = false;
    }
}
