using Coffee.UIEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EyeUIDissolve : MonoBehaviour
{
    [SerializeField]
    UIDissolve iDissolve;
    [SerializeField]
    float time;
    
    void Start()
    {
        Messenger.AddListener<PlayerState>(MsgType.changeOpenCloseEye, ChangeEye);
        Messenger.AddListener(MsgType.playerHurt, OpenEye);
        Messenger.AddListener(MsgType.playerWin, OpenEye);
    }

    void ChangeEye(PlayerState state)
    {
        if (state == PlayerState.Close)
        {
            ClosepenEye();
        }
        else
        {
            OpenEye();
        }
    }

    public void OpenEye()
    {
        DOTween.To(() => iDissolve.effectFactor, x => iDissolve.effectFactor = x, 1f, time);
    }
    public void ClosepenEye()
    {
        DOTween.To(() => iDissolve.effectFactor, x => iDissolve.effectFactor = x, 0f, time);
    }
}
