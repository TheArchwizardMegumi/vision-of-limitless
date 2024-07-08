using Coffee.UIEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextLevelUIDissolve : MonoBehaviour
{
    [SerializeField]
    UIDissolve iDissolve;
    [SerializeField]
    float time;

    void Start()
    {
        Messenger.AddListener(MsgType.reachExit, LevelChange);
    }

    public void LevelChange()
    {
        StartCoroutine(LevelChanging());
    }

    IEnumerator LevelChanging()
    {
        DOTween.To(() => iDissolve.effectFactor, x => iDissolve.effectFactor = x, 0f, time);
        yield return new WaitForSeconds(time);
        Messenger.Broadcast(MsgType.playerWin);
        DOTween.To(() => iDissolve.effectFactor, x => iDissolve.effectFactor = x, 1f, time);
        yield return new WaitForSeconds(time*2);
        iDissolve.effectFactor = 1f;
        Debug.Log("Set effectFactor to 1f");
    }
}
