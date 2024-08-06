using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAudioChange : MonoBehaviour
{
    public AudioSource openEyeAudio;
    public AudioSource closeEyeAudio;
    public AudioSource openEyeBGS;
    public AudioSource closeEyeBGS;
    public AudioSource hurtAudio;
    public AudioSource winAudio;
    public AudioSource playerEnterExitAudio;

    void Start()
    {
        Messenger.AddListener<PlayerState>(MsgType.changeOpenCloseEye, ChangeBlinkAudio);
        Messenger.AddListener(MsgType.playerHurt, HurtAudio);
        Messenger.AddListener(MsgType.reachExit, WinAudio);
        // 缺少一个调动的消息
    }

    void ChangeBlinkAudio(PlayerState state)
    {
        if (state == PlayerState.Close)
        {
            openEyeAudio.volume = 0;
            closeEyeAudio.volume = 1;
            closeEyeBGS.Play();
        }
        else
        {
            openEyeAudio.volume = 1;
            closeEyeAudio.volume = 0;
            openEyeBGS.Play();
        }
    }

    void HurtAudio()
    {
        hurtAudio.Play();
    }

    void WinAudio()
    {
        winAudio.Play();
    }

    void EnterExitAudio()
    {
        playerEnterExitAudio.Play();
    }
}
