using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkChecker : Singleton<BlinkChecker>
{
    [Header("еіблБебл")]
    public PlayerState isOpenEye = PlayerState.Open;
    public float blinkTime = 0.5f;
    public bool eyeOpening;
    public bool isBlinking = false;
    public bool canBlink;
    public void Start()
    {
        Init();
        Messenger.AddListener(MsgType.levelStart, Init);
    }

    public void Update()
    {
        if (canBlink)
        {
            EyeStateCheck();
        }
    }
    public void Init()
    {
        canBlink = true;
        isOpenEye = PlayerState.Open;
    }
    private void ChangeEyeState()
    {
        isOpenEye = isOpenEye == PlayerState.Open ? PlayerState.Close : PlayerState.Open;
        eyeOpening = isOpenEye == PlayerState.Open;
        Messenger.Broadcast<PlayerState>(MsgType.changeOpenCloseEye, isOpenEye);
    }
    IEnumerator Blinking()
    {
        yield return new WaitForSeconds(blinkTime);
        isBlinking = false;
    }
    void EyeStateCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBlinking)
        {
            isBlinking = true;
            ChangeEyeState();
            StartCoroutine(Blinking());
        }
    }
}
