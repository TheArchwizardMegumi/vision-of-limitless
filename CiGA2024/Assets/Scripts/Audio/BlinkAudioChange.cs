using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAudioChange : MonoBehaviour
{
    public AudioSource openEyeAudio;
    public AudioSource closeEyeAudio;

    void Start()
    {
        Messenger.AddListener<PlayerState>(MsgType.changeOpenCloseEye, ChangeBlinkAudio);
    }

    void ChangeBlinkAudio(PlayerState state)
    {
        if (state == PlayerState.Close)
        {
            openEyeAudio.volume = 0;
            closeEyeAudio.volume = 1;
        }
        else
        {
            openEyeAudio.volume = 1;
            closeEyeAudio.volume = 0;
        }
    }
}
